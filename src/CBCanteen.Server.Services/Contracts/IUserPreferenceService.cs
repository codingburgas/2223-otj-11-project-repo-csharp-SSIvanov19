// <copyright file="IUserPreferenceService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.Models.User;

namespace CBCanteen.Server.Services.Contracts;

/// <summary>
/// Interface for the user preference service.
/// </summary>
public interface IUserPreferenceService
{
    /// <summary>
    /// Gets the user preference asynchronous.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>The user preferences.</returns>
    Task<UserPreferenceVM> GetUserPreferenceAsync(string userId);

    /// <summary>
    /// Sets the user preference asynchronous.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="userPreferenceIM">The user preference.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task SetUserPreferenceAsync(string userId, UserPreferenceIM userPreferenceIM);
}
