// <copyright file="MenuOrderIM.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace CBCanteen.Shared.Models.Canteen.MenuOrder;

/// <summary>
/// Input model for the menu order.
/// </summary>
public class MenuOrderIM
{
    /// <summary>
    /// Gets or sets the menu identifier associated with the menu order.
    /// </summary>
    [Required]
    public string MenuId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets quantity of the menu order.
    /// </summary>
    [Required]
    [Range(1, 10, ErrorMessage = "Количеството трябва да е в размер между 0 и 10.")]
    public int Quantity { get; set; } = 0;

    /// <summary>
    /// Gets or sets date of the consumption.
    /// </summary>
    [Required]
    [FutureDate]
    public DateTime Date { get; set; } = DateTime.Now;
}
