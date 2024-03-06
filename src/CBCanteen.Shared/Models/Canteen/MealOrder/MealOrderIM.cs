// <copyright file="MealOrderIM.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBCanteen.Shared.Models.Canteen.MealOrder;

/// <summary>
/// Input model for the meal order.
/// </summary>
public class MealOrderIM
{
    /// <summary>
    /// Gets or sets the ID of the meal that was ordered.
    /// </summary>
    [Required]
    public string MealId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of the meal that was ordered.
    /// </summary>
    [Required]
    [Range(1, 10, ErrorMessage = "Количеството трябва да е в размер между 0 и 10.")]
    public int Quantity { get; set; } = 0;

    /// <summary>
    /// Gets or sets the date and time when the order was made, in UTC time.
    /// </summary>
    [Required]
    [FutureDate]
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
