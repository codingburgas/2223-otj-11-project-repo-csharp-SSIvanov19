// <copyright file="CalendarEvent.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

namespace CBCanteen.Shared.Models;

/// <summary>
/// Represents a calendar event.
/// </summary>
public class CalendarEvent
{
    /// <summary>
    /// Gets or sets the ID of the calendar event.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the subject of the calendar event.
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// Gets or sets the start time of the calendar event.
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// Gets or sets the end time of the calendar event.
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Gets or sets the time zone of the start time.
    /// </summary>
    public string? StartTimezone { get; set; }

    /// <summary>
    /// Gets or sets the time zone of the end time.
    /// </summary>
    public string? EndTimezone { get; set; }

    /// <summary>
    /// Gets or sets the location of the calendar event.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the description of the calendar event.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the calendar event is an all-day event.
    /// </summary>
    public bool? IsAllDay { get; set; }

    /// <summary>
    /// Gets or sets the ID of the recurrence pattern.
    /// </summary>
    public string? RecurrenceID { get; set; }

    /// <summary>
    /// Gets or sets the recurrence pattern of the calendar event.
    /// </summary>
    public string? RecurrenceRule { get; set; }

    /// <summary>
    /// Gets or sets the exceptions to the recurrence pattern.
    /// </summary>
    public string? RecurrenceException { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the calendar event is readonly.
    /// </summary>
    public bool? IsReadonly { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the calendar event is a block event.
    /// </summary>
    public bool? IsBlock { get; set; }

    /// <summary>
    /// Gets or sets the CSS class of the calendar event.
    /// </summary>
    public string? CssClass { get; set; }
}
