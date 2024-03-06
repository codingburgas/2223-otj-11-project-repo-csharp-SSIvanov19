// <copyright file="DailyOrder.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBCanteen.Server.Data.Models.Canteen;

/// <summary>
/// Represents a daily order in the canteen.
/// </summary>
public class DailyOrder
{
    /// <summary>
    /// Gets or sets the unique identifier for the daily order.
    /// </summary>
    [Required]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier for the first menu option for the daily order.
    /// </summary>
    [Required]
    public string MenuOneId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier for the second menu option for the daily order.
    /// </summary>
    [Required]
    public string MenuTwoId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the first menu option for the daily order.
    /// </summary>
    [ForeignKey(nameof(MenuOneId))]
    public Menu MenuOne { get; set; }

    /// <summary>
    /// Gets or sets the second menu option for the daily order.
    /// </summary>
    [ForeignKey(nameof(MenuTwoId))]
    public Menu MenuTwo { get; set; }

    /// <summary>
    /// Gets or sets the list of free consumption options for the daily order.
    /// </summary>
    public List<Meal> FreeConsumption { get; set; } = new ();

    /// <summary>
    /// Gets or sets the date of the daily order.
    /// </summary>
    public DateTime DateOfConsumption { get; set; } = DateTime.Now;
}
