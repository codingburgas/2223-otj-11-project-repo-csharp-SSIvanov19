// <copyright file="MenuOrderVM.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using CBCanteen.Shared.Models.Canteen.Menu;

namespace CBCanteen.Shared.Models.Canteen.MenuOrder;

/// <summary>
/// View model for the menu order.
/// </summary>
public class MenuOrderVM
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
    public MenuVM Menu { get; set; }
}
