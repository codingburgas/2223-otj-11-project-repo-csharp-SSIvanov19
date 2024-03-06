// <copyright file="ILunchHoursService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.Models.User;

namespace CBCanteen.Server.Services.Contracts;

/// <summary>
/// Contract for managing user lunch hours.
/// </summary>
public interface ILunchHoursService
{
    /// <summary>
    /// Sets user lunch hours.
    /// </summary>
    /// <param name="userLunchHoursIM">The user lunch hours input model.</param>
    /// <param name="userId">The id of the user.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task SetUserLunchHoursAsync(UserLunchHoursIM userLunchHoursIM, string userId);

    /// <summary>
    /// Gets user lunch hours.
    /// </summary>
    /// <param name="userId">The id of the user.</param>
    /// <returns>The user lunch hours view model.</returns>
    Task<UserLunchHoursVM?> GetUserLunchHoursAsync(string userId);
}
