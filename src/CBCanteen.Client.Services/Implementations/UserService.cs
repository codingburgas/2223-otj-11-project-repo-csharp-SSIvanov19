﻿using CBCanteen.Client.Services.Contracts;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace CBCanteen.Client.Services.Implementations;

internal class UserService : IUserService
{
    private readonly GraphServiceClient graphClient;

    public UserService(GraphServiceClient graphClient)
    {
        this.graphClient = graphClient;    
    }
    
    public async Task<User?> GetCurrentUserInfoAsync()
    {
        return await graphClient.Me.GetAsync((requestConfiguration) =>
        {
            requestConfiguration.QueryParameters.Select = new string[] { "displayName", "mail", "jobTitle", "department" };
        });
    }

    public async Task<Stream?> GetCurrentUserProfilePhotoAsync()
    {
        return await this.graphClient.Me.Photo.Content.GetAsync();
    }
}