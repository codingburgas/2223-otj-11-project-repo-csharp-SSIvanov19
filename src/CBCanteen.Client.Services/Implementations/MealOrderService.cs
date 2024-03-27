// <copyright file="MealOrderService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CBCanteen.Client.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.MealOrder;

namespace CBCanteen.Client.Services.Implementations;

/// <summary>
/// Service that manages meal orders.
/// </summary>
internal class MealOrderService : IMealOrderService
{
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="MealOrderService"/> class.
    /// </summary>
    /// <param name="httpClient">Http client.</param>
    public MealOrderService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task CreateMealOrderAsync(MealOrderIM mealOrderIM)
    {
        using StringContent jsonContent = new (
            JsonSerializer.Serialize(new
            {
                mealId = mealOrderIM.MealId,
                quantity = mealOrderIM.Quantity,
                date = mealOrderIM.Date,
            }),
            Encoding.UTF8,
            "application/json");

        await this.httpClient.PostAsync("/api/MealOrders", jsonContent);
    }

    /// <inheritdoc/>
    public async Task DeleteMealOrderAsync(string mealOrderId)
    {
        await this.httpClient.DeleteAsync($"/api/MealOrders/{mealOrderId}");
    }

    /// <inheritdoc/>
    public async Task EditMealOrderAsync(string dailyOrderId, MealOrderIM mealOrderIM)
    {
        using StringContent jsonContent = new (
            JsonSerializer.Serialize(new
            {
                mealId = mealOrderIM.MealId,
                quantity = mealOrderIM.Quantity,
                date = mealOrderIM.Date,
            }),
            Encoding.UTF8,
            "application/json");

        await this.httpClient.PutAsync($"/api/MealOrders/{dailyOrderId}", jsonContent);
    }

    /// <inheritdoc/>
    public async Task<List<MealOrderVM>> GetMealOrderAsync(DateTime? startDateTime, DateTime? endDateTime)
    {
        var apiUrl = "/api/MealOrders";

        if (startDateTime.HasValue)
        {
            apiUrl += $"?startDateTime={startDateTime.Value:yyyy-MM-ddTHH:mm:ss}";
        }

        if (endDateTime.HasValue)
        {
            apiUrl += startDateTime.HasValue ? "&" : "?";
            apiUrl += $"endDateTime={endDateTime.Value:yyyy-MM-ddTHH:mm:ss}";
        }

        return await this.httpClient.GetFromJsonAsync<List<MealOrderVM>>(apiUrl);
    }
}
