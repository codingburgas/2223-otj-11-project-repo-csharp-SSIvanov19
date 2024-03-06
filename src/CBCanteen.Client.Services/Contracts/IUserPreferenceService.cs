// <copyright file="IUserPreferenceService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.Models.User;

namespace CBCanteen.Client.Services.Contracts;

/// <summary>
/// This interface defines methods for getting and setting user preferences.
/// </summary>
public interface IUserPreferenceService
{
    /// <summary>
    /// This method asynchronously gets user preferences.
    /// </summary>
    /// <returns>The user preferences.</returns>
    Task<UserPreferenceVM?> GetUserPreferenceAsync();

    /// <summary>
    /// This method asynchronously sets user preferences.
    /// </summary>
    /// <param name="userPreferenceIM">User preferences to be set.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task SetUserPreferenceAsync(UserPreferenceIM userPreferenceIM);
}
