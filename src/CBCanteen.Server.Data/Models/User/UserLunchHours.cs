// <copyright file="UserLunchHours.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBCanteen.Server.Data.Models.User;

/// <summary>
/// Represents a UserLunchHours model.
/// </summary>
public class UserLunchHours
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    [Key]
    [Required]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether a user has the same lunch hours throughout the week.
    /// </summary>
    [Required]
    public bool HasSameLunchHours { get; set; } = true;

    /// <summary>
    /// Gets or sets the ID of the Monday Lunch Hours.
    /// </summary>
    [Required]
    public string MondayLunchHoursId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Monday Lunch Hours.
    /// </summary>
    [Required]
    [ForeignKey(nameof(MondayLunchHoursId))]
    public LunchHours MondayLunchHours { get; set; }

    /// <summary>
    /// Gets or sets the ID of the Tuesday Lunch Hours.
    /// </summary>
    [Required]
    public string TuesdayLunchTimeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Tuesday Lunch Hours.
    /// </summary>
    [Required]
    [ForeignKey(nameof(TuesdayLunchTimeId))]
    public LunchHours TuesdayLunchTime { get; set; }

    /// <summary>
    /// Gets or sets the ID of the Wednesday Lunch Hours.
    /// </summary>
    [Required]
    public string WednesdayLunchTimeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Wednesday Lunch Hours.
    /// </summary>
    [Required]
    [ForeignKey(nameof(WednesdayLunchTimeId))]
    public LunchHours WednesdayLunchTime { get; set; }

    /// <summary>
    /// Gets or sets the ID of the Thursday Lunch Hours.
    /// </summary>
    [Required]
    public string ThursdayLunchTimeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Thursday Lunch Hours.
    /// </summary>
    [Required]
    [ForeignKey(nameof(ThursdayLunchTimeId))]
    public LunchHours ThursdayLunchTime { get; set; }

    /// <summary>
    /// Gets or sets the ID of the Friday Lunch Hours.
    /// </summary>
    [Required]
    public string FridayLunchTimeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Friday LunchHours.
    /// </summary>
    [Required]
    [ForeignKey(nameof(FridayLunchTimeId))]
    public LunchHours FridayLunchTime { get; set; }
}
