// <copyright file="UserPreference.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CBCanteen.Server.Data.Models.User;

/// <summary>
/// Represents the user preferences that can be set in the CBCanteen application.
/// </summary>
public class UserPreference
{
    /// <summary>
    /// Gets or sets the user's ID.
    /// </summary>
    [Key]
    [Required]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether meetings are shown for the user.
    /// </summary>
    [Required]
    public bool ShowMeetings { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the user receives emails.
    /// </summary>
    [Required]
    public bool SendEmail { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the user wants to have reminders.
    /// </summary>
    [Required]
    public bool CreateReminder { get; set; } = true;
}
