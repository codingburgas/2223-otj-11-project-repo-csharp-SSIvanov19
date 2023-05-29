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

    /// <summary>
    /// Initializes a new instance of the <see cref="MealOrdersController"/> class.
    /// </summary>
    /// <param name="mealOrderService">Service for meal ordering.</param>
    /// <param name="currentUser">Current user.</param>
    /// <param name="mealService">Meal service.</param>
    public MealOrdersController(
        IMealOrderService mealOrderService,
        ICurrentUser currentUser,
        IMealService mealService)
    {
        this.mealOrderService = mealOrderService;
        this.currentUser = currentUser;
        this.mealService = mealService;
    }

    /// <summary>
    /// Creates a new meal order.
    /// </summary>
    /// <param name="mealOrderIM">Create meal order input model.</param>
    /// <returns>The created meal order.</returns>
    [HttpPost]
    public async Task<ActionResult<MealOrderVM>> CreateMealOrderAsync([FromBody] MealOrderIM mealOrderIM)
    {
        var meal = await this.mealService.GetMealByIdAsync(mealOrderIM.MealId);

        if (meal is null)
        {
            return this.NotFound();
        }

        var mealOrder = await this.mealOrderService.GenerateAndSaveMealOrderInfoAsync(mealOrderIM, this.currentUser.UserId);

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
        var mealOrder = await this.mealOrderService.GetMealOrderByIdAsync(mealOrderId);

        if (mealOrder is null)
        {
            return this.NotFound();
        }

        if (mealOrder.UserId != this.currentUser.UserId)
        {
            return this.Forbid();
        }

        await this.mealOrderService.DeleteMealOrderAsync(mealOrderId);

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
        var mealOrders = await this.mealOrderService.GetMealOrdersBetweenDates(startDateTime, endDateTime);

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
        var mealOrder = await this.mealOrderService.GetMealOrderByIdAsync(mealOrderId);

        if (mealOrder is null)
        {
            return this.NotFound();
        }

        if (mealOrder.UserId != this.currentUser.UserId)
        {
            return this.Forbid();
        }

        await this.mealOrderService.EditMealOrderAsync(mealOrderId, mealOrderIM);

        return this.Ok();
    }
}
