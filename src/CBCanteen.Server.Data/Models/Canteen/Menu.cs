// <copyright file="Menu.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBCanteen.Server.Data.Models.Canteen;

/// <summary>
/// Represents a menu in the Canteen System.
/// </summary>
public class Menu
{
    /// <summary>
    /// Gets or sets the unique identifier of the menu.
    /// </summary>
    [Required]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the unique identifier of the appetizer on the menu.
    /// </summary>
    [Required]
    public string AppetizerId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the unique identifier of the main dish on the menu.
    /// </summary>
    [Required]
    public string MainDishId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the unique identifier of the dessert on the menu.
    /// </summary>
    [Required]
    public string DessertId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the appetizer on the menu.
    /// </summary>
    [ForeignKey(nameof(AppetizerId))]
    public Meal Appetizer { get; set; }

    /// <summary>
    /// Gets or sets the main dish on the menu.
    /// </summary>
    [ForeignKey(nameof(MainDishId))]
    public Meal MainDish { get; set; }

    /// <summary>
    /// Gets or sets the dessert on the menu.
    /// </summary>
    [ForeignKey(nameof(DessertId))]
    public Meal Dessert { get; set; }

    /// <summary>
    /// Gets or sets the price of the menu.
    /// </summary>
    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "The minimum price of the menu is 0.1.")]
    public double Price { get; set; } = 0.1;
}
