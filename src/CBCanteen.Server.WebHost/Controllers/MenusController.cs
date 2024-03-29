﻿// <copyright file="MenusController.cs" company="CBCanteen">
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
    private readonly ICurrentUser currentUser;
    private readonly ILogger<MenusController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MenusController"/> class.
    /// </summary>
    /// <param name="menuService">Menu service.</param>
    /// <param name="mealService">Meal service.</param>
    /// <param name="menuPriceService">Menu price service.</param>
    /// <param name="currentUser">Current user.</param>
    /// <param name="logger">Logger.</param>
    public MenusController(
        IMenuService menuService,
        IMealService mealService,
        IMenuPriceService menuPriceService,
        ICurrentUser currentUser,
        ILogger<MenusController> logger)
    {
        this.menuService = menuService;
        this.mealService = mealService;
        this.menuPriceService = menuPriceService;
        this.currentUser = currentUser;
        this.logger = logger;
    }

    /// <summary>
    /// Generate And Save New Menu Information.
    /// </summary>
    /// <param name="menuIm">Menu Input Model.</param>
    /// <returns>Newly Created Menu View Model.</returns>
    [HttpPost]
    public async Task<ActionResult<MenuVM>> CreateMenuAsync([FromBody] MenuIM menuIm)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to create a menu with appetizer id: {menuIm.AppetizerId}, main dish id: {menuIm.MainDishId} and dessert id: {menuIm.DessertId}.");

        var appetizer = await this.mealService.GetMealByIdAsync(menuIm.AppetizerId);

        if (appetizer is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to create a menu with appetizer id: {menuIm.AppetizerId}, but the appetizer does not exist.");

            return this.BadRequest(
                new
                {
                    Message = $"Appetizer with id {menuIm.AppetizerId} does not exist.",
                });
        }

        var mainDish = await this.mealService.GetMealByIdAsync(menuIm.MainDishId);

        if (mainDish is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to create a menu with main dish id: {menuIm.MainDishId}, but the main dish does not exist.");

            return this.BadRequest(
                new
                {
                    Message = $"Main dish with id {menuIm.MainDishId} does not exists.",
                });
        }

        var dessert = await this.mealService.GetMealByIdAsync(menuIm.DessertId);

        if (dessert is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to create a menu with dessert id: {menuIm.DessertId}, but the dessert does not exist.");

            return this.BadRequest(
                new
                {
                    Message = $"Dessert with id {menuIm.DessertId} does not exists.",
                });
        }

        var menu = await this.menuService.GenerateAndSaveMenuInfoAsync(menuIm);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) successfully created a menu with appetizer id: {menuIm.AppetizerId}, main dish id: {menuIm.MainDishId} and dessert id: {menuIm.DessertId}.");

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
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to edit a menu with id: {menuId}.");

        var menu = await this.menuService.GetMenuByIdAsync(menuId);

        if (menu is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to edit a menu with id: {menuId}, but the menu does not exist.");

            return this.NotFound();
        }

        var appetizer = await this.mealService.GetMealByIdAsync(menuIm.AppetizerId);

        if (appetizer is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to edit a menu with appetizer id: {menuIm.AppetizerId}, but the appetizer does not exist.");

            return this.BadRequest(
                new
                {
                    Message = $"Appetizer with id {menuIm.AppetizerId} does not exist.",
                });
        }

        var mainDish = await this.mealService.GetMealByIdAsync(menuIm.MainDishId);

        if (mainDish is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to edit a menu with main dish id: {menuIm.MainDishId}, but the main dish does not exist.");

            return this.BadRequest(
                new
                {
                    Message = $"Main dish with id {menuIm.MainDishId} does not exists.",
                });
        }

        var dessert = await this.mealService.GetMealByIdAsync(menuIm.DessertId);

        if (dessert is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to edit a menu with dessert id: {menuIm.DessertId}, but the dessert does not exist.");

            return this.BadRequest(
                new
                {
                    Message = $"Dessert with id {menuIm.DessertId} does not exists.",
                });
        }

        await this.menuService.EditMenuAsync(menuId, menuIm);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) successfully edited a menu with id: {menuId}.");

        return this.Ok();
    }

    /// <summary>
    /// Get the current price for menu.
    /// </summary>
    /// <returns>Price for the menu.</returns>
    [HttpGet("price")]
    public async Task<ActionResult<double>> GetDefaultPriceForMenuAsync()
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to get the current price for the menu.");

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
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to set the default price for the menu to: {price}.");

        await this.menuPriceService.SetDefaultPriceForMenusAsync(price);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) successfully set the default price for the menu to: {price}.");

        return this.Ok();
    }
}
