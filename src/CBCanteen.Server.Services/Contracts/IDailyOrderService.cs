// <copyright file="IDailyOrderService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Data.Models.Canteen;
using CBCanteen.Shared.Models.Canteen.DailyOrder;

namespace CBCanteen.Server.Services.Contracts;

/// <summary>
/// Interface for a service that handles daily orders.
/// </summary>
public interface IDailyOrderService
{
    /// <summary>
    /// Saves a daily order.
    /// </summary>
    /// <param name="dailyOrder">The daily order to save.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task SaveDailyOrderAsync(DailyOrder dailyOrder);

    /// <summary>
    /// Generates and saves daily order information.
    /// </summary>
    /// <param name="dailyOrder">The daily order to generate and save information for.</param>
    /// <returns>The generated daily order view model with information.</returns>
    Task<DailyOrderVM> GenerateAndSaveDailyOrderInfoAsync(DailyOrderIM dailyOrder);

    /// <summary>
    /// Edits a daily order with the provided information.
    /// </summary>
    /// <param name="dailyOrderId">The id of the daily order to edit.</param>
    /// <param name="newDailyOrderInfo">The new daily order information to replace old information.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task EditDailyOrderAsync(string dailyOrderId, DailyOrderIM newDailyOrderInfo);

    /// <summary>
    /// Deletes a daily order with the provided id.
    /// </summary>
    /// <param name="id">The id of the daily order to be deleted.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteDailyOrderAsync(string id);

    /// <summary>
    /// Gets a daily order by its id.
    /// </summary>
    /// <param name="dailyOrderId">The id of the daily order to get.</param>
    /// <returns>The daily order with the provided id, or null if the daily order is not found.</returns>
    Task<DailyOrderVM?> GetDailyOrderByIdAsync(string dailyOrderId);

    /// <summary>
    /// Gets all daily orders stored in the system.
    /// </summary>
    /// <returns>A list of all daily orders.</returns>
    Task<List<DailyOrderVM>> GetAllDailyOrdersAsync();

    /// <summary>
    /// Gets all daily orders between two dates.
    /// </summary>
    /// <param name="startDateTime">The starting date time.</param>
    /// <param name="endDateTime">The ending date time.</param>
    /// <returns>A list of daily orders between the provided dates.</returns>
    Task<List<DailyOrderVM>> GetDailyOrdersBetweenDates(DateTime? startDateTime, DateTime? endDateTime);
}
