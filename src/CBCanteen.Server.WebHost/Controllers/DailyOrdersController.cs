// <copyright file="DailyOrdersController.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.DailyOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CBCanteen.Server.WebHost.Controllers;

/// <summary>
/// API endpoints for managing daily orders.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class DailyOrdersController : ControllerBase
{
    private readonly IDailyOrderService dailyOrderService;
    private readonly IMenuService menuService;
    private readonly IMealService mealService;
    private readonly ILogger<DailyOrdersController> logger;
    private readonly ICurrentUser currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="DailyOrdersController"/> class.
    /// </summary>
    /// <param name="dailyOrderService">Daily order service.</param>
    /// <param name="menuService">Menu service.</param>
    /// <param name="mealService">Meal service.</param>
    /// <param name="logger">Logger.</param>
    /// <param name="currentUser">Current user.</param>
    public DailyOrdersController(
        IDailyOrderService dailyOrderService,
        IMenuService menuService,
        IMealService mealService,
        ILogger<DailyOrdersController> logger,
        ICurrentUser currentUser)
    {
        this.dailyOrderService = dailyOrderService;
        this.menuService = menuService;
        this.mealService = mealService;
        this.logger = logger;
        this.currentUser = currentUser;
    }

    /// <summary>
    /// Creates a new daily order.
    /// </summary>
    /// <param name="dailyOrderIM">The daily order input model.</param>
    /// <returns>The created daily order.</returns>
    [HttpPost]
    public async Task<ActionResult<DailyOrderVM>> CreateDailyOrderAsync([FromBody] DailyOrderIM dailyOrderIM)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to create a daily order with menu one id: {dailyOrderIM.MenuOneId} and menu two id: {dailyOrderIM.MenuTwoId}.");

        var menuOne = await this.menuService.GetMenuByIdAsync(dailyOrderIM.MenuOneId);

        if (menuOne is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) failed to create a daily order. Reason: Menu with id {dailyOrderIM.MenuOneId} does not exist.");

            return this.BadRequest(
                new
                {
                    Message = $"Menu with id {dailyOrderIM.MenuOneId} does not exist.",
                });
        }

        var menuTwo = await this.menuService.GetMenuByIdAsync(dailyOrderIM.MenuTwoId);

        if (menuTwo is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) failed to create a daily order. Reason: Menu with id {dailyOrderIM.MenuTwoId} does not exist.");

            return this.BadRequest(
                new
                {
                    Message = $"Menu with id {dailyOrderIM.MenuTwoId} does not exist.",
                });
        }

        foreach (var mealId in dailyOrderIM.FreeConsumptionIds)
        {
            var meal = await this.mealService.GetMealByIdAsync(mealId);

            if (meal is null)
            {
                this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) failed to create a daily order. Reason: Meal with id {mealId} does not exist.");

                return this.BadRequest(
                    new
                    {
                        Message = $"Meal with id {mealId} does not exist.",
                    });
            }
        }

        var dailyOrder = await this.dailyOrderService.GenerateAndSaveDailyOrderInfoAsync(dailyOrderIM);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) created daily order.");

        return this.Ok(dailyOrder);
    }

    /// <summary>
    /// Deletes a daily order with a specific ID.
    /// </summary>
    /// <param name="dailyOrderId">The ID of the daily order to delete.</param>
    /// <returns>An OK result if deletion is successful.</returns>
    [HttpDelete("{dailyOrderId}")]
    public async Task<IActionResult> DeleteDailyOrderAsync(string dailyOrderId)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to delete daily order with id: {dailyOrderId}.");

        var dailyOrder = await this.dailyOrderService.GetDailyOrderByIdAsync(dailyOrderId);

        if (dailyOrder is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) failed to delete daily order with id: {dailyOrderId}. Reason: Daily order with id {dailyOrderId} doesn't exists.");

            return this.NotFound();
        }

        await this.dailyOrderService.DeleteDailyOrderAsync(dailyOrderId);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) successfully deleted daily order with id: {dailyOrderId}.");

        return this.Ok();
    }

    /// <summary>
    /// Retrieves all daily orders between specified start and end dates.
    /// </summary>
    /// <param name="startDateTime">The start date.</param>
    /// <param name="endDateTime">The end date.</param>
    /// <returns>A list of daily orders between specified dates.</returns>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<DailyOrderVM>>> GetAllDailyOrdersBetweenDates(DateTime? startDateTime, DateTime? endDateTime)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to get all daily orders between dates: {startDateTime} and {endDateTime}.");
        return await this.dailyOrderService.GetDailyOrdersBetweenDates(startDateTime, endDateTime);
    }

    /// <summary>
    /// Edits a daily order with a specific ID.
    /// </summary>
    /// <param name="dailyOrderId">The ID of the daily order to edit.</param>
    /// <param name="dailyOrderIM">The daily order input model.</param>
    /// <returns>An OK result if editing is successful.</returns>
    [HttpPut("{dailyOrderId}")]
    public async Task<IActionResult> EditDailyOrderAsync(string dailyOrderId, DailyOrderIM dailyOrderIM)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to edit daily order with id: {dailyOrderId}.");
        var dailyOrder = await this.dailyOrderService.GetDailyOrderByIdAsync(dailyOrderId);

        if (dailyOrder is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) failed to edit daily order with id: {dailyOrderId}. Reason: Daily order with id {dailyOrderId} doesn't exists.");
            return this.NotFound();
        }

        var menuOne = await this.menuService.GetMenuByIdAsync(dailyOrderIM.MenuOneId);

        if (menuOne is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) failed to edit daily order with id: {dailyOrderId}. Reason: Menu with id {dailyOrderIM.MenuOneId} does not exist.");
            return this.BadRequest(
                new
                {
                    Message = $"Menu with id {dailyOrderIM.MenuOneId} does not exist.",
                });
        }

        var menuTwo = await this.menuService.GetMenuByIdAsync(dailyOrderIM.MenuTwoId);

        if (menuTwo is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) failed to edit daily order with id: {dailyOrderId}. Reason: Menu with id {dailyOrderIM.MenuTwoId} does not exist.");
            return this.BadRequest(
                new
                {
                    Message = $"Menu with id {dailyOrderIM.MenuTwoId} does not exist.",
                });
        }

        foreach (var mealId in dailyOrderIM.FreeConsumptionIds)
        {
            var meal = await this.mealService.GetMealByIdAsync(mealId);

            if (meal is null)
            {
                this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) failed to edit daily order with id: {dailyOrderId}. Reason: Meal with id {mealId} does not exist.");
                return this.BadRequest(
                    new
                    {
                        Message = $"Meal with id {mealId} does not exist.",
                    });
            }
        }

        await this.dailyOrderService.EditDailyOrderAsync(dailyOrderId, dailyOrderIM);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) successfully edited daily order with id: {dailyOrderId}.");
        return this.Ok();
    }
}
