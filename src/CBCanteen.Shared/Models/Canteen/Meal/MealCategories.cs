// <copyright file="MealCategories.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel;

namespace CBCanteen.Shared.Models.Canteen.Meal;

/// <summary>
/// Represents the categories of meals available in the canteen.
/// </summary>
public enum MealCategories
{
    /// <summary>
    /// Represents an appetizer.
    /// </summary>
    [Description("Пред.")]
    Appetizer,

    /// <summary>
    /// Represents a main dish.
    /// </summary>
    [Description("Осн.")]
    MainDish,

    /// <summary>
    /// Represents a dessert.
    /// </summary>
    [Description("Десерт")]
    Dessert,
}
