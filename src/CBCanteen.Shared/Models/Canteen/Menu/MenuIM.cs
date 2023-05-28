// <copyright file="MenuIM.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CBCanteen.Shared.Models.Canteen.Menu;

/// <summary>
/// A class representing an input model of a menu in the Canteen System.
/// </summary>
public class MenuIM
{
    /// <summary>
    /// Gets or sets the unique identifier of the appetizer on the menu.
    /// </summary>
    [Required(ErrorMessage = "Задължително поле. Моля, въведете идентификатор на предястие.")]
    public string AppetizerId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the unique identifier of the main dish on the menu.
    /// </summary>
    [Required(ErrorMessage = "Задължително поле. Моля, въведете идентификатор на основното ястие.")]
    public string MainDishId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the unique identifier of the dessert on the menu.
    /// </summary>
    [Required(ErrorMessage = "Задължително поле. Моля, въведете идентификатор на десерта.")]
    public string DessertId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the price of the menu.
    /// </summary>
    [Required(ErrorMessage = "Задължително поле. Моля, въведе цена за менюто.")]
    [Range(0.1, double.MaxValue, ErrorMessage = "Цената трябва да е по-голяма от 0.1 лв.")]
    public double Price { get; set; } = 5.5;
}
