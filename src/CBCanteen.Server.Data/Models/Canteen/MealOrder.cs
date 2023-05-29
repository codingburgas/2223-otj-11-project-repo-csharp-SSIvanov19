// <copyright file="MealOrder.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBCanteen.Server.Data.Models.Canteen;

/// <summary>
/// Represents a single order of a meal made by a user.
/// </summary>
public class MealOrder
{
    /// <summary>
    /// Gets or sets the ID of the order.
    /// </summary>
    [Required]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user ID who made the order.
    /// </summary>
    [Required]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the meal that was ordered.
    /// </summary>
    [Required]
    public string MealId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of the meal that was ordered.
    /// </summary>
    [Required]
    public int Quantity { get; set; } = 0;

    /// <summary>
    /// Gets or sets the date and time when the order was made, in UTC time.
    /// </summary>
    [Required]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the meal that was ordered.
    /// </summary>
    [ForeignKey(nameof(MealId))]
    public Meal Meal { get; set; }
}
