// <copyright file="IDailyOrderService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.Models.Canteen.DailyOrder;

namespace CBCanteen.Client.Services.Contracts;

/// <summary>
/// Service that manages daily orders.
/// </summary>
public interface IDailyOrderService
{
    /// <summary>
    /// Creates a daily order for the specified daily order input model.
    /// </summary>
    /// <param name="dailyOrderIM">The daily order input model.</param>
    /// <returns>A task that represents the asynchronous create operation.</returns>
    Task CreateDailyOrderAsync(DailyOrderIM dailyOrderIM);

    /// <summary>
    /// Gets daily orders between the specified start and end dates.
    /// </summary>
    /// <param name="startDateTime">The start date of the period.</param>
    /// <param name="endDateTime">The end date of the period.</param>
    /// <returns>A task that represents the asynchronous get operation. The task result contains a list of daily orders.</returns>
    Task<List<DailyOrderVM>> GetDailyOrderAsync(DateTime? startDateTime, DateTime? endDateTime);

    /// <summary>
    /// Edits a daily order with the specified identifier using the daily order input model.
    /// </summary>
    /// <param name="dailyOrderId">The daily order identifier.</param>
    /// <param name="dailyOrderIM">The daily order input model.</param>
    /// <returns>A task that represents the asynchronous edit operation.</returns>
    Task EditDailyOrderAsync(string dailyOrderId, DailyOrderIM dailyOrderIM);

    /// <summary>
    /// Deletes a daily order with the specified identifier.
    /// </summary>
    /// <param name="dailyOrderId">The daily order identifier.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    Task DeleteDailyOrderAsync(string dailyOrderId);
}
