// <copyright file="MenuOrderService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CBCanteen.Client.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.MenuOrder;

namespace CBCanteen.Client.Services.Implementations;

/// <summary>
/// Service that manages menu orders.
/// </summary>
internal class MenuOrderService : IMenuOrderService
{
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuOrderService"/> class.
    /// </summary>
    /// <param name="httpClient">Http client.</param>
    public MenuOrderService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task CreateMenuOrderAsync(MenuOrderIM menuOrderIM)
    {
        using StringContent jsonContent = new (
            JsonSerializer.Serialize(new
            {
                menuId = menuOrderIM.MenuId,
                quantity = menuOrderIM.Quantity,
                date = menuOrderIM.Date,
            }),
            Encoding.UTF8,
            "application/json");

        await this.httpClient.PostAsync("/api/MenuOrders", jsonContent);
    }

    /// <inheritdoc/>
    public async Task DeleteMenuOrderAsync(string menuOrderId)
    {
        await this.httpClient.DeleteAsync($"/api/MenuOrders/{menuOrderId}");
    }

    /// <inheritdoc/>
    public async Task EditMenuOrderAsync(string dailyOrderId, MenuOrderIM menuOrderIM)
    {
        using StringContent jsonContent = new (
            JsonSerializer.Serialize(new
            {
                menuId = menuOrderIM.MenuId,
                quantity = menuOrderIM.Quantity,
                date = menuOrderIM.Date,
            }),
            Encoding.UTF8,
            "application/json");

        await this.httpClient.PutAsync("/api/MenuOrders", jsonContent);
    }

    /// <inheritdoc/>
    public async Task<List<MenuOrderVM>> GetMenuOrderAsync(DateTime? startDateTime, DateTime? endDateTime)
    {
        var apiUrl = "/api/MenuOrders";

        if (startDateTime.HasValue)
        {
            apiUrl += $"?startDateTime={startDateTime.Value:yyyy-MM-ddTHH:mm:ss}";
        }

        if (endDateTime.HasValue)
        {
            apiUrl += startDateTime.HasValue ? "&" : "?";
            apiUrl += $"endDateTime={endDateTime.Value:yyyy-MM-ddTHH:mm:ss}";
        }

        return await this.httpClient.GetFromJsonAsync<List<MenuOrderVM>>(apiUrl);
    }
}
