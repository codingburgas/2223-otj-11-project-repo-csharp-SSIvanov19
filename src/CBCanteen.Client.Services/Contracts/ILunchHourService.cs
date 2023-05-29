// <copyright file="ILunchHourService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.Models.User;

namespace CBCanteen.Client.Services.Contracts;

/// <summary>
/// Interface for managing user's lunch hours.
/// </summary>
public interface ILunchHourService
{
    /// <summary>
    /// Sets user's lunch hours.
    /// </summary>
    /// <param name="userLunchHoursIM">Model object that holds user's lunch hours.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task SetUserLunchHours(UserLunchHoursIM userLunchHoursIM);

    /// <summary>
    /// Gets user's lunch hours.
    /// </summary>
    /// <returns>A model object that holds user's lunch hours or null if there are none in the database.</returns>
    Task<UserLunchHoursVM?> GetUserLunchHours();
}
