// <copyright file="UserService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Client.Services.Contracts;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace CBCanteen.Client.Services.Implementations;

/// <summary>
/// This class implements methods used to interact with Microsoft Graph API and retrieve calendar events information.
/// </summary>
internal class UserService : IUserService
{
    /// <summary>
    /// The GraphServiceClient instance.
    /// </summary>
    private readonly GraphServiceClient graphClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="graphClient">The GraphServiceClient instance.</param>
    public UserService(GraphServiceClient graphClient)
    {
        this.graphClient = graphClient;
    }

    /// <inheritdoc/>
    public async Task<User?> GetCurrentUserInfoAsync()
    {
        return await this.graphClient.Me.GetAsync((requestConfiguration) =>
        {
            requestConfiguration.QueryParameters.Select = new string[] { "displayName", "mail", "jobTitle", "department" };
        });
    }

    /// <inheritdoc/>
    public async Task<Stream?> GetCurrentUserProfilePhotoAsync()
    {
        return await this.graphClient.Me.Photo.Content.GetAsync();
    }

    /// <inheritdoc/>
    public async Task<string> GetUserNameById(string id)
    {
        var user = await this.graphClient.Users[id].GetAsync((requestConfiguration) =>
        {
            requestConfiguration.QueryParameters.Select = new string[] { "mail" };
        });

        return user!.Mail!;
    }
}
