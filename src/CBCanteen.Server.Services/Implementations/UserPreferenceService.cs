// <copyright file="UserPreferenceService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using AutoMapper;
using CBCanteen.Server.Data.Data;
using CBCanteen.Server.Data.Models.User;
using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.User;
using Microsoft.EntityFrameworkCore;

namespace CBCanteen.Server.Services.Implementations;

/// <summary>
/// Class representing the user preference service.
/// </summary>
internal class UserPreferenceService : IUserPreferenceService
{
    private readonly ApplicationDBContext context;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserPreferenceService"/> class.
    /// </summary>
    /// <param name="context">The DB context.</param>
    /// <param name="mapper">The mapper.</param>
    public UserPreferenceService(ApplicationDBContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<UserPreferenceVM> GetUserPreferenceAsync(string userId)
    {
        return this.mapper.Map<UserPreferenceVM>(
            await this.context.UserPreferences.Where(up => up.UserId == userId).FirstOrDefaultAsync());
    }

    /// <inheritdoc/>
    public async Task SetUserPreferenceAsync(string userId, UserPreferenceIM userPreferenceIM)
    {
        var userPreference = this.mapper.Map<UserPreference>(userPreferenceIM);

        userPreference.UserId = userId;

        if (this.context.UserPreferences.Any(context => context.UserId == userId))
        {
            this.context.UserPreferences.Update(userPreference);
        }
        else
        {
            this.context.UserPreferences.Add(userPreference);
        }

        await this.context.SaveChangesAsync();
    }
}
