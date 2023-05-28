// <copyright file="IMealService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Data.Models.Canteen;
using CBCanteen.Shared.Models.Canteen.Meal;

namespace CBCanteen.Server.Services.Contracts;

/// <summary>
/// Interface for meal service.
/// </summary>
public interface IMealService
{
    /// <summary>
    /// Saves a meal.
    /// </summary>
    /// <param name="meal">The meal to save.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task SaveMealAsync(Meal meal);

    /// <summary>
    /// Generates and saves meal information.
    /// </summary>
    /// <param name="meal">The meal to generate and save information for.</param>
    /// <returns>The generated meal view model with information.</returns>
    Task<MealVM> GenerateAndSaveMealInfoAsync(MealIM meal);

    /// <summary>
    /// Edits a meal with the provided information.
    /// </summary>
    /// <param name="mealId">The id of the meal to edit.</param>
    /// <param name="newMealInfo">The new meal information to replace old information.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task EditMealAsync(string mealId, MealIM newMealInfo);

    /// <summary>
    /// Gets a meal by its id.
    /// </summary>
    /// <param name="mealId">The id of the meal to get.</param>
    /// <returns>The meal with the provided id, or null if the meal is not found.</returns>
    Task<MealVM?> GetMealByIdAsync(string mealId);

    /// <summary>
    /// Gets all meals stored in the system.
    /// </summary>
    /// <returns>A list of all meals.</returns>
    Task<List<MealVM>> GetAllMealsAsync();

    /// <summary>
    /// Searches for meals in the database by keyword.
    /// </summary>
    /// <param name="keyword">Keyword to search with.</param>
    /// <returns>A list of meals that match the keyword.</returns>
    Task<List<MealVM>> SearchForMealsAsync(string keyword);
}
