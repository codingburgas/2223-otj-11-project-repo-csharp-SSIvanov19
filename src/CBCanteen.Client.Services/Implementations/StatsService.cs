// <copyright file="StatsService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.Net.Http.Json;
using CBCanteen.Client.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.Stats;

namespace CBCanteen.Client.Services.Implementations;

/// <summary>
/// Class representing a service that manages stats.
/// </summary>
internal class StatsService : IStatsService
{
    private readonly HttpClient httpClient;
    private readonly IUserService userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatsService"/> class.
    /// </summary>
    /// <param name="httpClient">Http client.</param>
    /// <param name="userService">User service.</param>
    public StatsService(HttpClient httpClient, IUserService userService)
    {
        this.httpClient = httpClient;
        this.userService = userService;
    }

    /// <inheritdoc/>
    public async Task<List<MealStats>> GetMealInRangeAsync(DateOnly startDate, DateOnly endDate)
    {
        return (await this.httpClient.GetFromJsonAsync<List<MealStats>>($"api/OrderStats/meals?startTime={startDate.ToString("o")}&endTime={endDate.ToString("o")}")) !;
    }

    /// <inheritdoc/>
    public async Task<List<OrderStats>> GetOrdersInRangeAsync(DateOnly startDate, DateOnly endDate)
    {
        var orders = await this.httpClient.GetFromJsonAsync<List<OrderStats>>($"/api/OrderStats/orders?startTime={startDate.ToString("o")}&endTime={endDate.ToString("o")}");

        foreach (var order in orders!)
        {
            foreach (var userOrder in order.UserOrders)
            {
                userOrder.UserName = await this.userService.GetUserNameById(userOrder.UserId);
            }
        }

        return orders;
    }
}
