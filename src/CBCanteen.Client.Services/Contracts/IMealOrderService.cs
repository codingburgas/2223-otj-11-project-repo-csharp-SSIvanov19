// <copyright file="IMealOrderService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.Models.Canteen.MealOrder;

namespace CBCanteen.Client.Services.Contracts;

/// <summary>
/// Service that manages meal orders.
/// </summary>
public interface IMealOrderService
{
    /// <summary>
    /// Creates a meal order.
    /// </summary>
    /// <param name="mealOrderIM">The meal order to create.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CreateMealOrderAsync(MealOrderIM mealOrderIM);

    /// <summary>
    /// Retrieves meal orders between the provided start and end date.
    /// </summary>
    /// <param name="startDateTime">The earliest date to retrieve meal orders from.</param>
    /// <param name="endDateTime">The latest date to retrieve meal orders from.</param>
    /// <returns>A task that represents the asynchronous operation, returns a list of the queried meal orders.</returns>
    Task<List<MealOrderVM>> GetMealOrderAsync(DateTime? startDateTime, DateTime? endDateTime);

    /// <summary>
    /// Edits an existing meal order.
    /// </summary>
    /// <param name="mealOrderId">The id of the meal order to edit.</param>
    /// <param name="mealOrderIM">The updated meal order data.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task EditMealOrderAsync(string mealOrderId, MealOrderIM mealOrderIM);

    /// <summary>
    /// Deletes an existing meal order.
    /// </summary>
    /// <param name="mealOrderId">The id of the meal order to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteMealOrderAsync(string mealOrderId);
}
