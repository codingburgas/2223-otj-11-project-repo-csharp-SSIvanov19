// <copyright file="ICalendarService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using Microsoft.Graph.Models;

namespace CBCanteen.Client.Services.Contracts;

/// <summary>
/// This interface defines methods used to interact with Microsoft Graph API and retrieve calendar events information.
/// </summary>
public interface ICalendarService
{
    /// <summary>
    /// Retrieves events between two dates from Microsoft Graph API.
    /// </summary>
    /// <param name="startDate">The starting date.</param>
    /// <param name="endDate">The ending date.</param>
    /// <returns>The events.</returns>
    Task<EventCollectionResponse?> GetEventsBetweenTwoDatesAsync(DateTime startDate, DateTime endDate);
}
