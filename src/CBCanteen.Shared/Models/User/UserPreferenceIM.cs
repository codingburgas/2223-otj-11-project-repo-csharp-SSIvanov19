// <copyright file="UserPreferenceIM.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

namespace CBCanteen.Shared.Models.User;

/// <summary>
/// A class that represents the user preferences that can be set in the CBCanteen application.
/// </summary>
public class UserPreferenceIM
{
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
