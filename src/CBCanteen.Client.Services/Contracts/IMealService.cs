// <copyright file="IMealService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.Models.Canteen.Meal;

namespace CBCanteen.Client.Services.Contracts;

/// <summary>
/// Meal service interface.
/// </summary>
public interface IMealService
{
    /// <summary>
    /// Gets all meals stored in the system.
    /// </summary>
    /// <returns>A list of all meals.</returns>
    Task<List<MealVM>> GetAllMealsAsync();

    /// <summary>
    /// Creates a new meal and stores it in the system.
    /// </summary>
    /// <param name="mealIm">The model of the meal to be created.</param>
    /// <returns>A task representing the operation.</returns>
    Task CreateMealAsync(MealIM mealIm);

    /// <summary>
    /// Edits an existing meal in the system.
    /// </summary>
    /// <param name="mealId">The ID of the meal to be edited.</param>
    /// <param name="mealIm">The updated model of the meal.</param>
    /// <returns>A task representing the operation.</returns>
    Task EditMealAsync(string mealId, MealIM mealIm);
}
