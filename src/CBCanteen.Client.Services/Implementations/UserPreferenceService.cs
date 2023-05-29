// <copyright file="UserPreferenceService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CBCanteen.Client.Services.Contracts;
using CBCanteen.Shared.Models.User;

namespace CBCanteen.Client.Services.Implementations;

/// <summary>
/// Class implementing <see cref="IUserPreferenceService"/>.
/// </summary>
internal class UserPreferenceService : IUserPreferenceService
{
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserPreferenceService"/> class.
    /// </summary>
    /// <param name="httpClient">Http client.</param>
    public UserPreferenceService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task<UserPreferenceVM?> GetUserPreferenceAsync()
    {
        try
        {
            return await this.httpClient.GetFromJsonAsync<UserPreferenceVM>("/api/UserPreferences");
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <inheritdoc/>
    public async Task SetUserPreferenceAsync(UserPreferenceIM userPreferenceIM)
    {
        using StringContent jsonContent = new (
            JsonSerializer.Serialize(new
            {
                showMeetings = userPreferenceIM.ShowMeetings,
                sendEmail = userPreferenceIM.SendEmail,
                createReminder = userPreferenceIM.CreateReminder,
            }),
            Encoding.UTF8,
            "application/json");

        await this.httpClient.PostAsync("/api/UserPreferences", jsonContent);
    }
}
