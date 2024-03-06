// <copyright file="MealOrderService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using AutoMapper;
using AutoMapper.QueryableExtensions;
using CBCanteen.Server.Data.Data;
using CBCanteen.Server.Data.Models.Canteen;
using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.MealOrder;
using Microsoft.EntityFrameworkCore;

namespace CBCanteen.Server.Services.Implementations;

/// <summary>
/// Class implementing the <see cref="IMealOrderService"/> interface.
/// </summary>
internal class MealOrderService : IMealOrderService
{
    private readonly ApplicationDBContext context;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="MealOrderService"/> class.
    /// </summary>
    /// <param name="context">The DB Context.</param>
    /// <param name="mapper">Mapper.</param>
    public MealOrderService(ApplicationDBContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task DeleteMealOrderAsync(string id)
    {
        var mealOrder = await this.context.MealOrders.FirstOrDefaultAsync(o => o.Id == id);

        if (mealOrder is not null)
        {
            this.context.Remove(mealOrder);
            await this.context.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task EditMealOrderAsync(string mealOrderId, MealOrderIM newMealOrderInfo)
    {
        var mealOrder = await this.context.MealOrders.FindAsync(mealOrderId);

        mealOrder!.Quantity = newMealOrderInfo.Quantity;
        mealOrder.MealId = newMealOrderInfo.MealId;
        mealOrder.Date = newMealOrderInfo.Date;

        await this.context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<MealOrderVM> GenerateAndSaveMealOrderInfoAsync(MealOrderIM mealOrderIM, string userId)
    {
        var mealOrder = this.mapper.Map<MealOrder>(mealOrderIM);

        mealOrder.Id = Guid.NewGuid().ToString();
        mealOrder.UserId = userId;

        await this.SaveMealOrderAsync(mealOrder);

        return this.mapper.Map<MealOrderVM>(mealOrder);
    }

    /// <inheritdoc/>
    public async Task<List<MealOrderVM>> GetAllMealOrdersAsync()
    {
        return await this.context.MealOrders
            .ProjectTo<MealOrderVM>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<MealOrderVM?> GetMealOrderByIdAsync(string mealOrderId)
    {
        return this.mapper.Map<MealOrderVM>(await this.context.MealOrders
            .FirstOrDefaultAsync(o => o.Id == mealOrderId));
    }

    /// <inheritdoc/>
    public async Task<List<MealOrderVM>> GetMealOrdersBetweenDates(DateTime? startDateTime, DateTime? endDateTime)
    {
        var query = this.context.MealOrders.AsQueryable();

        if (startDateTime is not null)
        {
            query = query.Where(o => o.Date >= startDateTime);
        }

        if (endDateTime is not null)
        {
            query = query.Where(o => o.Date <= endDateTime);
        }

        return await query.ProjectTo<MealOrderVM>(this.mapper.ConfigurationProvider).ToListAsync();
    }

    /// <inheritdoc/>
    public async Task SaveMealOrderAsync(MealOrder mealOrder)
    {
        if (this.context.MealOrders!.Any(o => o.Id == mealOrder.Id))
        {
            this.context.Update(mealOrder);
        }
        else
        {
            this.context.Add(mealOrder);
        }

        await this.context.SaveChangesAsync();
    }
}
