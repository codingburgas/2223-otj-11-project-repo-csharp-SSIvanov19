// <copyright file="DailyOrderVM.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using CBCanteen.Shared.Models.Canteen.Meal;
using CBCanteen.Shared.Models.Canteen.Menu;

namespace CBCanteen.Shared.Models.Canteen.DailyOrder;

/// <summary>
/// Class representing a view model of a daily order in the Canteen System.
/// </summary>
public class DailyOrderVM
{
    /// <summary>
    /// Gets or sets the unique identifier for the daily order.
    /// </summary>
    [Required]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the first menu option for the daily order.
    /// </summary>
    public MenuVM MenuOne { get; set; } = new ();

    /// <summary>
    /// Gets or sets the second menu option for the daily order.
    /// </summary>
    public MenuVM MenuTwo { get; set; } = new ();

    /// <summary>
    /// Gets or sets the list of free consumption options for the daily order.
    /// </summary>
    public List<MealVM> FreeConsumption { get; set; } = new ();

    /// <summary>
    /// Gets or sets the date of the daily order.
    /// </summary>
    public DateTime DateOfConsumption { get; set; } = DateTime.Now;
}
