// <copyright file="MenuVM.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.Models.Canteen.Meal;

namespace CBCanteen.Shared.Models.Canteen.Menu;

/// <summary>
/// A class representing a view model of a menu in the Canteen System.
/// </summary>
public class MenuVM
{
    /// <summary>
    /// Gets or sets the unique identifier of the menu.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the appetizer on the menu.
    /// </summary>
    public MealVM Appetizer { get; set; } = new ();

    /// <summary>
    /// Gets or sets the main dish on the menu.
    /// </summary>
    public MealVM MainDish { get; set; } = new ();

    /// <summary>
    /// Gets or sets the dessert on the menu.
    /// </summary>
    public MealVM Dessert { get; set; } = new ();

    /// <summary>
    /// Gets or sets the price of the menu.
    /// </summary>
    public double Price { get; set; } = 5.5;
}
