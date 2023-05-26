// <copyright file="MealIM.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CBCanteen.Shared.Models.Canteen;

/// <summary>
/// Represents an order-able meal in the canteen.
/// </summary>
public class MealIM
{
    /// <summary>
    /// Gets or sets the name of the meal.
    /// </summary>
    [Required(ErrorMessage = "Моля, въведете име на ястието.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the meal.
    /// </summary>
    [Range(0.1, double.MaxValue, ErrorMessage = "Полето за цена трябва да бъде по-голямо от {1}.")]
    [Required(ErrorMessage = "Моля, въведете единична цена на ястието.")]
    public double UnitPrice { get; set; } = 0.1d;

    /// <summary>
    /// Gets or sets the weight of the meal.
    /// </summary>
    [Required(ErrorMessage = "Моля, въведете тегло на ястието.")]
    [Range(1, int.MaxValue, ErrorMessage = "Полето за грамаж трябва да бъде по-голямо от {1}.")]
    public int Weight { get; set; } = 1;

    /// <summary>
    /// Gets or sets the category of the meal.
    /// </summary>
    [Required(ErrorMessage = "Моля, изберете категория на ястието.")]
    public MealCategories Category { get; set; } = MealCategories.Appetizer;
}

