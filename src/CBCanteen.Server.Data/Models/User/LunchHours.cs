// <copyright file="LunchHours.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CBCanteen.Server.Data.Models.User;

/// <summary>
/// Represents a user's lunch hours for a specific day of the week.
/// </summary>
public class LunchHours
{
    /// <summary>
    /// Gets or sets the ID of the lunch hours entry.
    /// </summary>
    [Required]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the day of the week for the lunch hours entry.
    /// </summary>
    [Required]
    public DayOfWeek DayOfWeek { get; set; }

    /// <summary>
    /// Gets or sets the starting time for the lunch period.
    /// </summary>
    [Required]
    public TimeOnly StartTime { get; set; } = TimeOnly.MinValue;

    /// <summary>
    /// Gets or sets the ending time for the lunch period.
    /// </summary>
    [Required]
    public TimeOnly EndTime { get; set; } = TimeOnly.MinValue;
}
