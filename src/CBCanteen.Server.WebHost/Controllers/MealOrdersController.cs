// <copyright file="MealOrdersController.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.DailyOrder;
using CBCanteen.Shared.Models.Canteen.MealOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CBCanteen.Server.WebHost.Controllers;

/// <summary>
/// Controller for meal orders.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MealOrdersController : ControllerBase
{
    private readonly IMealOrderService mealOrderService;
    private readonly IMealService mealService;
    private readonly ICurrentUser currentUser;
    private readonly ILogger<MealOrdersController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MealOrdersController"/> class.
    /// </summary>
    /// <param name="mealOrderService">Service for meal ordering.</param>
    /// <param name="currentUser">Current user.</param>
    /// <param name="mealService">Meal service.</param>
    /// <param name="logger">Logger.</param>
    public MealOrdersController(
        IMealOrderService mealOrderService,
        ICurrentUser currentUser,
        IMealService mealService,
        ILogger<MealOrdersController> logger)
    {
        this.mealOrderService = mealOrderService;
        this.currentUser = currentUser;
        this.mealService = mealService;
        this.logger = logger;
    }

    /// <summary>
    /// Creates a new meal order.
    /// </summary>
    /// <param name="mealOrderIM">Create meal order input model.</param>
    /// <returns>The created meal order.</returns>
    [HttpPost]
    public async Task<ActionResult<MealOrderVM>> CreateMealOrderAsync([FromBody] MealOrderIM mealOrderIM)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to create a meal order with meal id: {mealOrderIM.MealId}.");

        var meal = await this.mealService.GetMealByIdAsync(mealOrderIM.MealId);

        if (meal is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to create a meal order with meal id: {mealOrderIM.MealId} but the meal was not found.");
            return this.NotFound();
        }

        var mealOrder = await this.mealOrderService.GenerateAndSaveMealOrderInfoAsync(mealOrderIM, this.currentUser.UserId);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) created a meal order with meal id: {mealOrderIM.MealId}.");

        return this.Ok(mealOrder);
    }

    /// <summary>
    /// Deletes an existing meal order.
    /// </summary>
    /// <param name="mealOrderId">The id of the meal order to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [HttpDelete("{mealOrderId}")]
    public async Task<IActionResult> DeleteMealOrderAsync(string mealOrderId)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to delete a meal order with id: {mealOrderId}.");

        var mealOrder = await this.mealOrderService.GetMealOrderByIdAsync(mealOrderId);

        if (mealOrder is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to delete a meal order with id: {mealOrderId} but the meal order was not found.");

            return this.NotFound();
        }

        if (mealOrder.UserId != this.currentUser.UserId)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to delete a meal order with id: {mealOrderId} but the meal order does not belong to the user.");

            return this.Forbid();
        }

        await this.mealOrderService.DeleteMealOrderAsync(mealOrderId);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) deleted a meal order with id: {mealOrderId}.");

        return this.Ok();
    }

    /// <summary>
    /// Gets all meal orders between specified dates.
    /// </summary>
    /// <param name="startDateTime">Start date time.</param>
    /// <param name="endDateTime">End date time.</param>
    /// <returns>Meal orders between specified dates.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MealOrderVM>>> GetAllMealsOrdersBetweenDates(DateTime? startDateTime, DateTime? endDateTime)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to get all meal orders between dates: {startDateTime} and {endDateTime}.");

        var mealOrders = await this.mealOrderService.GetMealOrdersBetweenDates(startDateTime, endDateTime);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) got all meal orders between dates: {startDateTime} and {endDateTime}.");

        return this.Ok(mealOrders.Where(m => m.UserId == this.currentUser.UserId));
    }

    /// <summary>
    /// Edits an existing meal order.
    /// </summary>
    /// <param name="mealOrderId">The id of the meal order to edit.</param>
    /// <param name="mealOrderIM">The meal order to edit.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [HttpPut("{mealOrderId}")]
    public async Task<IActionResult> EditMealOrderAsync(string mealOrderId, MealOrderIM mealOrderIM)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to edit a meal order with id: {mealOrderId}.");

        var mealOrder = await this.mealOrderService.GetMealOrderByIdAsync(mealOrderId);

        if (mealOrder is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to edit a meal order with id: {mealOrderId} but the meal order was not found.");

            return this.NotFound();
        }

        if (mealOrder.UserId != this.currentUser.UserId)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to edit a meal order with id: {mealOrderId} but the meal order does not belong to the user.");

            return this.Forbid();
        }

        await this.mealOrderService.EditMealOrderAsync(mealOrderId, mealOrderIM);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) edited a meal order with id: {mealOrderId}.");

        return this.Ok();
    }
}
