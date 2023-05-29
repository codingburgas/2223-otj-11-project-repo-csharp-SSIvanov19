// <copyright file="IMenuOrderService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Data.Models.Canteen;
using CBCanteen.Shared.Models.Canteen.MenuOrder;

namespace CBCanteen.Server.Services.Contracts;

/// <summary>
/// Interface for the menu order service.
/// </summary>
public interface IMenuOrderService
{
    /// <summary>
    /// Saves a menu order asynchronously.
    /// </summary>
    /// <param name="menuOrder">The menu order to be saved.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveMenuOrderAsync(MenuOrder menuOrder);

    /// <summary>
    /// Generates and saves menu order information asynchronously.
    /// </summary>
    /// <param name="menuOrder">The menu order for which to generate and save information.</param>
    /// <param name="userId">The ID of the user who made the order.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<MenuOrderVM> GenerateAndSaveMenuOrderInfoAsync(MenuOrderIM menuOrder, string userId);

    /// <summary>
    /// Edits a menu order asynchronously.
    /// </summary>
    /// <param name="menuOrderId">The ID of the menu order to be edited.</param>
    /// <param name="newMenuOrderInfo">The updated information of the menu order.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task EditMenuOrderAsync(string menuOrderId, MenuOrderIM newMenuOrderInfo);

    /// <summary>
    /// Deletes a menu order asynchronously.
    /// </summary>
    /// <param name="id">The ID of the menu to be deleted.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteMenuOrderAsync(string id);

    /// <summary>
    /// Retrieves the menu order with the specified ID asynchronously.
    /// </summary>
    /// <param name="menuOrderId">he ID of the menu order to retrieve.</param>
    /// <returns>A task representing the asynchronous operation that returns the menu order with the specified ID.</returns>
    Task<MenuOrderVM?> GetMenuOrderByIdAsync(string menuOrderId);

    /// <summary>
    /// Retrieves all the menu orders asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that returns a list of menu orders.</returns>
    Task<List<MenuOrderVM>> GetAllMenuOrdersAsync();

    /// <summary>
    /// Gets a list of menu orders placed between two dates.
    /// </summary>
    /// <param name="startDateTime">The start date and time of the range to search for menu orders.</param>
    /// <param name="endDateTime">The end date and time of the range to search for menu orders.</param>
    /// <returns>A list of <see cref="MenuOrderVM"/> representing the menu orders placed between the specified range.</returns>
    Task<List<MenuOrderVM>> GetMenuOrdersBetweenDates(DateTime? startDateTime, DateTime? endDateTime);
}
