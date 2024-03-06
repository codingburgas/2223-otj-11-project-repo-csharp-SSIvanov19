// <copyright file="IStatsService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.Models.Canteen.Stats;

namespace CBCanteen.Client.Services.Contracts;

/// <summary>
/// 
/// </summary>
public interface IStatsService
{
    /// <summary>
    /// Gets the order statistics for the given time period.
    /// </summary>
    /// <param name="startDate">Start date.</param>
    /// <param name="endDate">End date.</param>
    /// <returns>List with orders.</returns>
    Task<List<OrderStats>> GetOrdersInRangeAsync(DateOnly startDate, DateOnly endDate);

    /// <summary>
    /// Gets the meal statistics for the given time period.
    /// </summary>
    /// <param name="startDate">Start date.</param>
    /// <param name="endDate">End date.</param>
    /// <returns>List with meals.</returns>
    Task<List<MealStats>> GetMealInRangeAsync(DateOnly startDate, DateOnly endDate);
}
