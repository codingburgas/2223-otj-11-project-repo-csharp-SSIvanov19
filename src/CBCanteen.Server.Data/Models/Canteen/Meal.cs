// <copyright file="Meal.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using CBCanteen.Shared.Models.Canteen;

namespace CBCanteen.Server.Data.Models.Canteen;

/// <summary>
/// A class representing a meal item on the canteen menu.
/// </summary>
public class Meal
{
    /// <summary>
    /// Gets or sets the unique identifier of the meal item.
    /// </summary>
    [Required]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the meal.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the meal.
    /// </summary>
    [Required]
    public double UnitPrice { get; set; } = 0d;

    /// <summary>
    /// Gets or sets the weight of the meal.
    /// </summary>
    [Required]
    public int Weight { get; set; } = 0;

    /// <summary>
    /// Gets or sets the category of the meal.
    /// </summary>
    [Required]
    public MealCategories Category { get; set; }
}
