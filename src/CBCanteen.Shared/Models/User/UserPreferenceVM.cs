// <copyright file="UserPreferenceVM.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

namespace CBCanteen.Shared.Models.User;

/// <summary>
/// Represents the user preferences that can be set in the CBCanteen application.
/// </summary>
public class UserPreferenceVM
{
    /// <summary>
    /// Gets or sets the user's ID.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether meetings are shown for the user.
    /// </summary>
    public bool ShowMeetings { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the user receives emails.
    /// </summary>
    public bool SendEmail { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the user wants to have reminders.
    /// </summary>
    public bool CreateReminder { get; set; } = true;
}
