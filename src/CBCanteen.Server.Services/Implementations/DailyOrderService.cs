// <copyright file="DailyOrderService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using AutoMapper;
using AutoMapper.QueryableExtensions;
using CBCanteen.Server.Data.Data;
using CBCanteen.Server.Data.Models.Canteen;
using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.DailyOrder;
using Microsoft.EntityFrameworkCore;

namespace CBCanteen.Server.Services.Implementations;

/// <summary>
/// Class for Daily Order Service.
/// </summary>
internal class DailyOrderService : IDailyOrderService
{
    private readonly ApplicationDBContext context;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="DailyOrderService"/> class.
    /// </summary>
    /// <param name="context">DB Context.</param>
    /// <param name="mapper">Mapper.</param>
    public DailyOrderService(ApplicationDBContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task DeleteDailyOrderAsync(string id)
    {
        var dailyOrder = await this.context.DailyOrders.FirstOrDefaultAsync(o => o.Id == id);

        if (dailyOrder is not null)
        {
            this.context.Remove(dailyOrder);
            await this.context.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task EditDailyOrderAsync(string dailyOrderId, DailyOrderIM newDailyOrderInfo)
    {
        var dailyOrder = await this.context.DailyOrders.FindAsync(dailyOrderId);

        dailyOrder!.MenuOneId = newDailyOrderInfo.MenuOneId;
        dailyOrder.MenuTwoId = newDailyOrderInfo.MenuTwoId;

        this.context.DailyOrderMeals.Where(o => o.DailyOrderId == dailyOrder.Id).ToList().ForEach(o => this.context.DailyOrderMeals.Remove(o));
        await this.context.SaveChangesAsync();

        var freeConsumptionList = new List<Meal>();

        foreach (var mealId in newDailyOrderInfo.FreeConsumptionIds)
        {
            var meal = await this.context.Meals.FindAsync(mealId);
            freeConsumptionList.Add(meal!);
        }

        dailyOrder.FreeConsumption = freeConsumptionList;

        this.context.Entry(dailyOrder).State = EntityState.Modified;

        await this.SaveDailyOrderAsync(dailyOrder);
    }

    /// <inheritdoc/>
    public async Task<DailyOrderVM> GenerateAndSaveDailyOrderInfoAsync(DailyOrderIM dailyOrderIm)
    {
        var dailyOrder = this.mapper.Map<DailyOrder>(dailyOrderIm);

        dailyOrder.Id = Guid.NewGuid().ToString();

        var freeConsumptionList = new List<Meal>();

        foreach (var mealId in dailyOrderIm.FreeConsumptionIds)
        {
            var meal = await this.context.Meals.FindAsync(mealId);
            freeConsumptionList.Add(meal!);
        }

        dailyOrder.FreeConsumption = freeConsumptionList;

        await this.SaveDailyOrderAsync(dailyOrder);

        return this.mapper.Map<DailyOrderVM>(dailyOrder);
    }

    /// <inheritdoc/>
    public async Task<List<DailyOrderVM>> GetAllDailyOrdersAsync()
    {
        return await this.context.DailyOrders
            .ProjectTo<DailyOrderVM>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<DailyOrderVM?> GetDailyOrderByIdAsync(string dailyOrderId)
    {
        return this.mapper.Map<DailyOrderVM>(await this.context.DailyOrders
            .FirstOrDefaultAsync(o => o.Id == dailyOrderId));
    }

    /// <inheritdoc/>
    public async Task<List<DailyOrderVM>> GetDailyOrdersBetweenDates(DateTime? startDateTime = null, DateTime? endDateTime = null)
    {
        var query = this.context.DailyOrders.AsQueryable();

        if (startDateTime is not null)
        {
            query = query.Where(o => o.DateOfConsumption >= startDateTime);
        }

        if (endDateTime is not null)
        {
            query = query.Where(o => o.DateOfConsumption <= endDateTime);
        }

        return await query.ProjectTo<DailyOrderVM>(this.mapper.ConfigurationProvider).ToListAsync();
    }

    /// <inheritdoc/>
    public async Task SaveDailyOrderAsync(DailyOrder dailyOrder)
    {
        if (this.context.DailyOrders.Any(o => o.Id == dailyOrder.Id))
        {
            this.context.Update(dailyOrder);
        }
        else
        {
            this.context.Add(dailyOrder);
        }

        await this.context.SaveChangesAsync();
    }
}
