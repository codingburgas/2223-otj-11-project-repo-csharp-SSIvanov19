// <copyright file="IMenuOrderService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.Models.Canteen.MenuOrder;

namespace CBCanteen.Client.Services.Contracts;

/// <summary>
/// Service that manages menu orders.
/// </summary>
public interface IMenuOrderService
{
    /// <summary>
    /// Creates a menu order.
    /// </summary>
    /// <param name="menuOrderIM">The menu order to create.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CreateMenuOrderAsync(MenuOrderIM menuOrderIM);

    /// <summary>
    /// Retrieves menu orders between the provided start and end date.
    /// </summary>
    /// <param name="startDateTime">The earliest date to retrieve menu orders from.</param>
    /// <param name="endDateTime">The latest date to retrieve menu orders from.</param>
    /// <returns>A task that represents the asynchronous operation, returns a list of the queried menu orders.</returns>
    Task<List<MenuOrderVM>> GetMenuOrderAsync(DateTime? startDateTime, DateTime? endDateTime);

    /// <summary>
    /// Edits an existing menu order.
    /// </summary>
    /// <param name="menuOrderId">The id of the menu order to edit.</param>
    /// <param name="menuOrderIM">The updated menu order data.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task EditMenuOrderAsync(string menuOrderId, MenuOrderIM menuOrderIM);

    /// <summary>
    /// Deletes an existing menu order.
    /// </summary>
    /// <param name="menuOrderId">The id of the menu order to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteMenuOrderAsync(string menuOrderId);
}
