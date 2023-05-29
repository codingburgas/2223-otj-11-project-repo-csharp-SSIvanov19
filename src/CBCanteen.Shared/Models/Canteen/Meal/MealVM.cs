﻿// <copyright file="MealVM.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

namespace CBCanteen.Shared.Models.Canteen.Meal;

/// <summary>
/// Represents the view model for a meal in the canteen.
/// </summary>
public class MealVM
{
    /// <summary>
    /// Gets or sets the id of the meal.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the meal.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the meal.
    /// </summary>
    public double UnitPrice { get; set; } = 0d;

    /// <summary>
    /// Gets or sets the weight of the meal.
    /// </summary>
    public int Weight { get; set; } = 0;

    /// <summary>
    /// Gets or sets the category of the meal.
    /// </summary>
    public string Category { get; set; } = string.Empty;
}