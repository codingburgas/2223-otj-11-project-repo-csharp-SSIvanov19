// <copyright file="MenuOrderService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using AutoMapper;
using AutoMapper.QueryableExtensions;
using CBCanteen.Server.Data.Data;
using CBCanteen.Server.Data.Models.Canteen;
using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.MenuOrder;
using Microsoft.EntityFrameworkCore;

namespace CBCanteen.Server.Services.Implementations;

/// <summary>
/// Class implementing the <see cref="IMenuOrderService"/> interface.
/// </summary>
internal class MenuOrderService : IMenuOrderService
{
    private readonly ApplicationDBContext context;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuOrderService"/> class.
    /// </summary>
    /// <param name="context">The DB Context.</param>
    /// <param name="mapper">Mapper.</param>
    public MenuOrderService(ApplicationDBContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task DeleteMenuOrderAsync(string id)
    {
        var menuOrder = await this.context.MenuOrders.FirstOrDefaultAsync(o => o.Id == id);

        if (menuOrder is not null)
        {
            this.context.Remove(menuOrder);
            await this.context.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task EditMenuOrderAsync(string menuOrderId, MenuOrderIM newMenuOrderInfo)
    {
        var menuOrder = await this.context.MenuOrders.FindAsync(menuOrderId);

        menuOrder!.Quantity = newMenuOrderInfo.Quantity;
        menuOrder.MenuId = newMenuOrderInfo.MenuId;
        menuOrder.Date = newMenuOrderInfo.Date;

        await this.context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<MenuOrderVM> GenerateAndSaveMenuOrderInfoAsync(MenuOrderIM menuOrderIM, string userId)
    {
        var menuOrder = this.mapper.Map<MenuOrder>(menuOrderIM);

        menuOrder.Id = Guid.NewGuid().ToString();
        menuOrder.UserId = userId;

        await this.SaveMenuOrderAsync(menuOrder);

        return this.mapper.Map<MenuOrderVM>(menuOrder);
    }

    /// <inheritdoc/>
    public async Task<List<MenuOrderVM>> GetAllMenuOrdersAsync()
    {
        return await this.context.MenuOrders
            .ProjectTo<MenuOrderVM>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<MenuOrderVM?> GetMenuOrderByIdAsync(string menuOrderId)
    {
        return this.mapper.Map<MenuOrderVM>(await this.context.MenuOrders
            .FirstOrDefaultAsync(o => o.Id == menuOrderId));
    }

    /// <inheritdoc/>
    public async Task<List<MenuOrderVM>> GetMenuOrdersBetweenDates(DateTime? startDateTime, DateTime? endDateTime)
    {
        var query = this.context.MenuOrders.AsQueryable();

        if (startDateTime is not null)
        {
            query = query.Where(o => o.Date >= startDateTime);
        }

        if (endDateTime is not null)
        {
            query = query.Where(o => o.Date <= endDateTime);
        }

        return await query.ProjectTo<MenuOrderVM>(this.mapper.ConfigurationProvider).ToListAsync();
    }

    /// <inheritdoc/>
    public async Task SaveMenuOrderAsync(MenuOrder menuOrder)
    {
        if (this.context.MenuOrders!.Any(o => o.Id == menuOrder.Id))
        {
            this.context.Update(menuOrder);
        }
        else
        {
            this.context.Add(menuOrder);
        }

        await this.context.SaveChangesAsync();
    }
}
