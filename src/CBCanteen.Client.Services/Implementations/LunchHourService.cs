// <copyright file="LunchHourService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CBCanteen.Client.Services.Contracts;
using CBCanteen.Shared.Models.User;

namespace CBCanteen.Client.Services.Implementations;

/// <summary>
/// Represents a service for managing lunch hour preferences for a user.
/// </summary>
internal class LunchHourService : ILunchHourService
{
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="LunchHourService"/> class.
    /// </summary>
    /// <param name="httpClient">Http client.</param>
    public LunchHourService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    /// <summary>
    /// Returns an object representing the user's lunch hour preferences.
    /// </summary>
    /// <returns>The user's lunch hour preferences, or null if the user has not set any preferences.</returns>
    public async Task<UserLunchHoursVM?> GetUserLunchHours()
    {
        try
        {
            return await this.httpClient.GetFromJsonAsync<UserLunchHoursVM?>("/api/LunchHour");
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Sets the user's lunch hour preferences.
    /// </summary>
    /// <param name="userLunchHoursIM">An object containing the user's new lunch hour preferences.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task SetUserLunchHours(UserLunchHoursIM userLunchHoursIM)
    {
        using StringContent jsonContent = new (
            JsonSerializer.Serialize(new
            {
                mondayLunchTimeStart = userLunchHoursIM.MondayLunchTimeStart,
                mondayLunchTimeEnd = userLunchHoursIM.MondayLunchTimeEnd,
                tuesdayLunchTimeStart = userLunchHoursIM.ThursdayLunchTimeStart,
                tuesdayLunchTimeEnd = userLunchHoursIM.ThursdayLunchTimeEnd,
                wednesdayLunchTimeStart = userLunchHoursIM.WednesdayLunchTimeStart,
                wednesdayLunchTimeEnd = userLunchHoursIM.WednesdayLunchTimeEnd,
                thursdayLunchTimeStart = userLunchHoursIM.ThursdayLunchTimeStart,
                thursdayLunchTimeEnd = userLunchHoursIM.ThursdayLunchTimeEnd,
                fridayLunchTimeStart = userLunchHoursIM.FridayLunchTimeStart,
                fridayLunchTimeEnd = userLunchHoursIM.FridayLunchTimeEnd,
                hasSameLunchHours = userLunchHoursIM.HasSameLunchHours,
            }),
            Encoding.UTF8,
            "application/json");

        await this.httpClient.PostAsync("/api/LunchHour", jsonContent);
    }
}
