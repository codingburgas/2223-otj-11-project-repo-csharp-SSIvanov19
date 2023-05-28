// <copyright file="DailyOrderIM.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using CBCanteen.Shared.DataAnnotations;

namespace CBCanteen.Shared.Models.Canteen.DailyOrder;

/// <summary>
/// Class representing an input model of a daily order in the Canteen System.
/// </summary>
public class DailyOrderIM
{
    /// <summary>
    /// Gets or sets the unique identifier for the first menu option for the daily order.
    /// </summary>
    [Required(ErrorMessage = "MenuOneId is required. Please fill it in.")]
    public string MenuOneId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier for the second menu option for the daily order.
    /// </summary>
    [Required(ErrorMessage = "MenuTwoId is required. Please fill it in.")]
    public string MenuTwoId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of free consumption options for the daily order.
    /// </summary>
    [Required(ErrorMessage = "FreeConsumptionIds are required. Please fill them in.")]
    public List<string> FreeConsumptionIds { get; set; } = new ();

    /// <summary>
    /// Gets or sets the date of the daily order.
    /// </summary>
    [Required(ErrorMessage = "DateOfConsumption is required. Please fill it in.")]
    [FutureDate(ErrorMessage = "The date of consumption must be today or in the future.")]
    public DateTime DateOfConsumption { get; set; } = DateTime.Now;
}
