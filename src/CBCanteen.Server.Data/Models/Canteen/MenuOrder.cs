// <copyright file="MenuOrder.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBCanteen.Server.Data.Models.Canteen;

/// <summary>
/// Represents a single menu order with the user ID, the menu item ID, the quantity and the date.
/// </summary>
public class MenuOrder
{
    /// <summary>
    /// Gets or sets the unique identifier of the menu order.
    /// </summary>
    [Required]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user identifier associated with the menu order.
    /// </summary>
    [Required]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the menu identifier associated with the menu order.
    /// </summary>
    [Required]
    public string MenuId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets quantity of the menu order.
    /// </summary>
    [Required]
    public int Quantity { get; set; } = 0;

    /// <summary>
    /// Gets or sets date of the consumption.
    /// </summary>
    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets menu associated with the menu order.
    /// </summary>
    [ForeignKey(nameof(MenuId))]
    public Menu Menu { get; set; }
}
