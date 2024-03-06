// <copyright file="UserLunchHoursVM.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

namespace CBCanteen.Shared.Models.User;

/// <summary>
/// Represents the lunchtime hours of a user for each weekday.
/// </summary>
public class UserLunchHoursVM
{
    /// <summary>
    /// Gets or sets a value indicating whether a user has the same lunch hours throughout the week.
    /// </summary>
    public bool HasSameLunchHours { get; set; }

    /// <summary>
    /// Gets or sets the start time of Monday lunch.
    /// </summary>
    public TimeOnly MondayLunchTimeStart { get; set; } = new ();

    /// <summary>
    /// Gets or sets the end time of Monday lunch.
    /// </summary>
    public TimeOnly MondayLunchTimeEnd { get; set; } = new ();

    /// <summary>
    /// Gets or sets the start time of Tuesday lunch.
    /// </summary>
    public TimeOnly TuesdayLunchTimeStart { get; set; } = new ();

    /// <summary>
    /// Gets or sets the end time of Tuesday lunch.
    /// </summary>
    public TimeOnly TuesdayLunchTimeEnd { get; set; } = new ();

    /// <summary>
    /// Gets or sets the start time of Wednesday lunch.
    /// </summary>
    public TimeOnly WednesdayLunchTimeStart { get; set; } = new ();

    /// <summary>
    /// Gets or sets the end time of Wednesday lunch.
    /// </summary>
    public TimeOnly WednesdayLunchTimeEnd { get; set; } = new ();

    /// <summary>
    /// Gets or sets the start time of Thursday lunch.
    /// </summary>
    public TimeOnly ThursdayLunchTimeStart { get; set; } = new ();

    /// <summary>
    /// Gets or sets the end time of Thursday lunch.
    /// </summary>
    public TimeOnly ThursdayLunchTimeEnd { get; set; } = new ();

    /// <summary>
    /// Gets or sets the start time of Friday lunch.
    /// </summary>
    public TimeOnly FridayLunchTimeStart { get; set; } = new ();

    /// <summary>
    /// Gets or sets the end time of Friday lunch.
    /// </summary>
    public TimeOnly FridayLunchTimeEnd { get; set; } = new ();
}