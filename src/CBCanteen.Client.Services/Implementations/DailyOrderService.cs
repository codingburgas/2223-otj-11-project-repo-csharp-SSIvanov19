// <copyright file="DailyOrderService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CBCanteen.Client.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.DailyOrder;

namespace CBCanteen.Client.Services.Implementations;

/// <summary>
/// This class implements methods used to interact with the daily order.
/// </summary>
internal class DailyOrderService : IDailyOrderService
{
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="DailyOrderService"/> class.
    /// </summary>
    /// <param name="httpClient">Http client.</param>
    public DailyOrderService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task CreateDailyOrderAsync(DailyOrderIM dailyOrderIM)
    {
        using StringContent jsonContent = new (
            JsonSerializer.Serialize(new
            {
                menuOneId = dailyOrderIM.MenuOneId,
                menuTwoId = dailyOrderIM.MenuTwoId,
                freeConsumptionIds = dailyOrderIM.FreeConsumptionIds,
                dateOfConsumption = dailyOrderIM.DateOfConsumption,
            }),
            Encoding.UTF8,
            "application/json");

        await this.httpClient.PostAsync("/api/DailyOrders", jsonContent);
    }

    /// <inheritdoc/>
    public async Task DeleteDailyOrderAsync(string dailyOrderId)
    {
        await this.httpClient.DeleteAsync($"/api/DailyOrders/{dailyOrderId}");
    }

    /// <inheritdoc/>
    public async Task EditDailyOrderAsync(string dailyOrderId, DailyOrderIM dailyOrderIM)
    {
        using StringContent jsonContent = new (
            JsonSerializer.Serialize(new
            {
                menuOneId = dailyOrderIM.MenuOneId,
                menuTwoId = dailyOrderIM.MenuTwoId,
                freeConsumptionIds = dailyOrderIM.FreeConsumptionIds,
                dateOfConsumption = dailyOrderIM.DateOfConsumption,
            }),
            Encoding.UTF8,
            "application/json");

        await this.httpClient.PutAsync($"api/DailyOrders/{dailyOrderId}", jsonContent);
    }

    /// <inheritdoc/>
    public async Task<List<DailyOrderVM>> GetDailyOrderAsync(DateTime? startDateTime, DateTime? endDateTime)
    {
        var apiUrl = "/api/DailyOrders";

        if (startDateTime.HasValue)
        {
            apiUrl += $"?startDateTime={startDateTime.Value:yyyy-MM-ddTHH:mm:ss}";
        }

        if (endDateTime.HasValue)
        {
            apiUrl += startDateTime.HasValue ? "&" : "?";
            apiUrl += $"endDateTime={endDateTime.Value:yyyy-MM-ddTHH:mm:ss}";
        }

        return await this.httpClient.GetFromJsonAsync<List<DailyOrderVM>>(apiUrl);
    }

}
