// <copyright file="IUserService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using Microsoft.Graph.Models;

namespace CBCanteen.Client.Services.Contracts;

/// <summary>
/// This interface defines methods used to interact with the current user information on Microsoft Graph API.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Retrieves the current user information from Microsoft Graph API.
    /// </summary>
    /// <returns>The user information.</returns>
    Task<User?> GetCurrentUserInfoAsync();

    /// <summary>
    /// Retrieves the current user profile photo from Microsoft Graph API.
    /// </summary>
    /// <returns>The profile photo stream.</returns>
    Task<Stream?> GetCurrentUserProfilePhotoAsync();
}
