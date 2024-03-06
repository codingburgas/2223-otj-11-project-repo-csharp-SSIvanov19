// <copyright file="IMealOrderService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Data.Models.Canteen;
using CBCanteen.Shared.Models.Canteen.MealOrder;

namespace CBCanteen.Server.Services.Contracts;

/// <summary>
/// Interface for a service that handles meal orders.
/// </summary>
public interface IMealOrderService
{
    /// <summary>
    /// Saves a meal order asynchronously.
    /// </summary>
    /// <param name="mealOrder">The meal order to be saved.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveMealOrderAsync(MealOrder mealOrder);

    /// <summary>
    /// Generates and saves meal order information asynchronously.
    /// </summary>
    /// <param name="mealOrder">The meal order for which to generate and save information.</param>
    /// <param name="userId">The ID of the user creating the meal.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<MealOrderVM> GenerateAndSaveMealOrderInfoAsync(MealOrderIM mealOrder, string userId);

    /// <summary>
    /// Edits a meal order asynchronously.
    /// </summary>
    /// <param name="mealOrderId">The ID of the meal order to be edited.</param>
    /// <param name="newMealOrderInfo">The updated information of the meal order.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task EditMealOrderAsync(string mealOrderId, MealOrderIM newMealOrderInfo);

    /// <summary>
    /// Deletes a meal order asynchronously.
    /// </summary>
    /// <param name="id">The ID of the meal to be deleted.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteMealOrderAsync(string id);

    /// <summary>
    /// Retrieves the meal order with the specified ID asynchronously.
    /// </summary>
    /// <param name="mealOrderId">he ID of the meal order to retrieve.</param>
    /// <returns>A task representing the asynchronous operation that returns the meal order with the specified ID.</returns>
    Task<MealOrderVM?> GetMealOrderByIdAsync(string mealOrderId);

    /// <summary>
    /// Retrieves all the meal orders asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that returns a list of meal orders.</returns>
    Task<List<MealOrderVM>> GetAllMealOrdersAsync();

    /// <summary>
    /// Gets a list of meal orders placed between two dates.
    /// </summary>
    /// <param name="startDateTime">The start date and time of the range to search for meal orders.</param>
    /// <param name="endDateTime">The end date and time of the range to search for meal orders.</param>
    /// <returns>A list of <see cref="MealOrderVM"/> representing the meal orders placed between the specified range.</returns>
    Task<List<MealOrderVM>> GetMealOrdersBetweenDates(DateTime? startDateTime, DateTime? endDateTime);
}
