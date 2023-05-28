// <copyright file="MenuService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CBCanteen.Client.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.Meal;
using CBCanteen.Shared.Models.Canteen.Menu;

namespace CBCanteen.Client.Services.Implementations;

/// <summary>
/// Class representing the menu service.
/// </summary>
internal class MenuService : IMenuService
{
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuService"/> class.
    /// </summary>
    /// <param name="httpClient">Http client.</param>
    public MenuService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task<MenuVM> CreateMenuAsync(MenuIM menuIm)
    {
        using StringContent jsonContent = new (
            JsonSerializer.Serialize(new
            {
                appetizerId = menuIm.AppetizerId,
                mainDishId = menuIm.MainDishId,
                dessertId = menuIm.DessertId,
                price = menuIm.Price,
            }),
            Encoding.UTF8,
            "application/json");

        var response = await this.httpClient.PostAsync("api/Menus", jsonContent);

        return await response.Content.ReadFromJsonAsync<MenuVM>();
    }

    /// <inheritdoc/>
    public async Task EditMenuAsync(string menuId, MenuIM menuIm)
    {
        using StringContent jsonContent = new (
            JsonSerializer.Serialize(new
            {
                appetizerId = menuIm.AppetizerId,
                mainDishId = menuIm.MainDishId,
                dessertId = menuIm.DessertId,
                price = menuIm.Price,
            }),
            Encoding.UTF8,
            "application/json");

        await this.httpClient.PutAsync($"api/Menus/{menuId}", jsonContent);
    }

    /// <inheritdoc/>
    public async Task<double> GetDefaultPriceForMenuAsync()
    {
        var resp = await this.httpClient.GetAsync("api/Menus/price");
        return await resp.Content.ReadFromJsonAsync<double>();
    }

    /// <inheritdoc/>
    public List<MealVM> GetMealsFromMenuAsync(MenuVM menuVM)
    {
        return new List<MealVM> { menuVM.Appetizer, menuVM.MainDish, menuVM.Dessert };
    }

    /// <inheritdoc/>
    public async Task SetDefaultPriceForMenuAsync(double defaultPrice)
    {
        using StringContent jsonContent = new (
           JsonSerializer.Serialize(defaultPrice),
           Encoding.UTF8,
           "application/json");

        await this.httpClient.PostAsync("api/Menus/Price", jsonContent);
    }
}
