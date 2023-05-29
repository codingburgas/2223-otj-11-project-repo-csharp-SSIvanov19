// <copyright file="MenusController.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CBCanteen.Server.WebHost.Controllers;

/// <summary>
/// API Controller For Menus.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class MenusController : ControllerBase
{
    private readonly IMenuService menuService;
    private readonly IMealService mealService;
    private readonly IMenuPriceService menuPriceService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MenusController"/> class.
    /// </summary>
    /// <param name="menuService">Menu service.</param>
    /// <param name="mealService">Meal service.</param>
    /// <param name="menuPriceService">Menu price service.</param>
    public MenusController(IMenuService menuService, IMealService mealService, IMenuPriceService menuPriceService)
    {
        this.menuService = menuService;
        this.mealService = mealService;
        this.menuPriceService = menuPriceService;
    }

    /// <summary>
    /// Generate And Save New Menu Information.
    /// </summary>
    /// <param name="menuIm">Menu Input Model.</param>
    /// <returns>Newly Created Menu View Model.</returns>
    [HttpPost]
    public async Task<ActionResult<MenuVM>> CreateMenuAsync([FromBody] MenuIM menuIm)
    {
        var appetizer = await this.mealService.GetMealByIdAsync(menuIm.AppetizerId);

        if (appetizer is null)
        {
            return this.BadRequest(
                new
                {
                    Message = $"Appetizer with id {menuIm.AppetizerId} does not exist.",
                });
        }

        var mainDish = await this.mealService.GetMealByIdAsync(menuIm.MainDishId);

        if (mainDish is null)
        {
            return this.BadRequest(
                new
                {
                    Message = $"Main dish with id {menuIm.MainDishId} does not exists.",
                });
        }

        var dessert = await this.mealService.GetMealByIdAsync(menuIm.DessertId);

        if (dessert is null)
        {
            return this.BadRequest(
                new
                {
                    Message = $"Dessert with id {menuIm.DessertId} does not exists.",
                });
        }

        var menu = await this.menuService.GenerateAndSaveMenuInfoAsync(menuIm);

        return this.Ok(menu);
    }

    /// <summary>
    /// Edit Menu Information.
    /// </summary>
    /// <param name="menuId">Existing Menu Id.</param>
    /// <param name="menuIm">Menu Input Model.</param>
    /// <returns>Empty Result or NotFound Result.</returns>
    [HttpPut("{menuId}")]
    public async Task<IActionResult> EditMenuAsync(string menuId, [FromBody] MenuIM menuIm)
    {
        var menu = await this.menuService.GetMenuByIdAsync(menuId);

        if (menu is null)
        {
            return this.NotFound();
        }

        var appetizer = await this.mealService.GetMealByIdAsync(menuIm.AppetizerId);

        if (appetizer is null)
        {
            return this.BadRequest(
                new
                {
                    Message = $"Appetizer with id {menuIm.AppetizerId} does not exist.",
                });
        }

        var mainDish = await this.mealService.GetMealByIdAsync(menuIm.MainDishId);

        if (mainDish is null)
        {
            return this.BadRequest(
                new
                {
                    Message = $"Main dish with id {menuIm.MainDishId} does not exists.",
                });
        }

        var dessert = await this.mealService.GetMealByIdAsync(menuIm.DessertId);

        if (dessert is null)
        {
            return this.BadRequest(
                new
                {
                    Message = $"Dessert with id {menuIm.DessertId} does not exists.",
                });
        }

        await this.menuService.EditMenuAsync(menuId, menuIm);

        return this.Ok();
    }

    /// <summary>
    /// Get the current price for menu.
    /// </summary>
    /// <returns>Price for the menu.</returns>
    [HttpGet("price")]
    public async Task<ActionResult<double>> GetDefaultPriceForMenuAsync()
    {
        return await this.menuPriceService.GetMenuPriceAsync();
    }

    /// <summary>
    /// Set default price for the Menu.
    /// </summary>
    /// <param name="price">The new price to set.</param>
    /// <returns>The HTTP result.</returns>
    [HttpPost("price")]
    public async Task<IActionResult> SetDefaultPriceForMenuAsync([Required] [FromBody] double price)
    {
        await this.menuPriceService.SetDefaultPriceForMenusAsync(price);

        return this.Ok();
    }
}
