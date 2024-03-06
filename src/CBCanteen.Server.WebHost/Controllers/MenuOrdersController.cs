// <copyright file="MenuOrdersController.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.MenuOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CBCanteen.Server.WebHost.Controllers;

/// <summary>
/// Controller for menu orders.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MenuOrdersController : ControllerBase
{
    private readonly IMenuOrderService menuOrderService;
    private readonly IMenuService menuService;
    private readonly ICurrentUser currentUser;
    private readonly ILogger<MenuOrdersController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuOrdersController"/> class.
    /// </summary>
    /// <param name="menuOrderService">Service for menu ordering.</param>
    /// <param name="currentUser">Current user.</param>
    /// <param name="menuService">Menu service.</param>
    /// <param name="logger">Logger.</param>
    public MenuOrdersController(
        IMenuOrderService menuOrderService,
        ICurrentUser currentUser,
        IMenuService menuService,
        ILogger<MenuOrdersController> logger)
    {
        this.menuOrderService = menuOrderService;
        this.currentUser = currentUser;
        this.menuService = menuService;
        this.logger = logger;
    }

    /// <summary>
    /// Creates a new menu order.
    /// </summary>
    /// <param name="menuOrderIM">Create menu order input model.</param>
    /// <returns>The created menu order.</returns>
    [HttpPost]
    public async Task<ActionResult<MenuOrderVM>> CreateMenuOrderAsync([FromBody] MenuOrderIM menuOrderIM)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to create a menu order with menu id: {menuOrderIM.MenuId}.");

        var menu = await this.menuService.GetMenuByIdAsync(menuOrderIM.MenuId);

        if (menu is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to create a menu order with menu id: {menuOrderIM.MenuId} but the menu was not found.");

            return this.NotFound();
        }

        var menuOrder = await this.menuOrderService.GenerateAndSaveMenuOrderInfoAsync(menuOrderIM, this.currentUser.UserId);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) created a menu order with menu id: {menuOrderIM.MenuId}.");

        return this.Ok(menuOrder);
    }

    /// <summary>
    /// Deletes an existing menu order.
    /// </summary>
    /// <param name="menuOrderId">The id of the menu order to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [HttpDelete("{menuOrderId}")]
    public async Task<IActionResult> DeleteMenuOrderAsync(string menuOrderId)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to delete a menu order with id: {menuOrderId}.");

        var menuOrder = await this.menuOrderService.GetMenuOrderByIdAsync(menuOrderId);

        if (menuOrder is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to delete a menu order with id: {menuOrderId} but the menu order was not found.");

            return this.NotFound();
        }

        if (menuOrder.UserId != this.currentUser.UserId)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to delete a menu order with id: {menuOrderId} but the menu order does not belong to the user.");

            return this.Forbid();
        }

        await this.menuOrderService.DeleteMenuOrderAsync(menuOrderId);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) deleted a menu order with id: {menuOrderId}.");

        return this.Ok();
    }

    /// <summary>
    /// Gets all menu orders between specified dates.
    /// </summary>
    /// <param name="startDateTime">Start date time.</param>
    /// <param name="endDateTime">End date time.</param>
    /// <returns>Menu orders between specified dates.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuOrderVM>>> GetAllMenusOrdersBetweenDates(DateTime? startDateTime, DateTime? endDateTime)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to get all menu orders between dates: {startDateTime} and {endDateTime}.");

        var menuOrders = await this.menuOrderService.GetMenuOrdersBetweenDates(startDateTime, endDateTime);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) got all menu orders between dates: {startDateTime} and {endDateTime}.");

        return this.Ok(menuOrders.Where(m => m.UserId == this.currentUser.UserId));
    }

    /// <summary>
    /// Edits an existing menu order.
    /// </summary>
    /// <param name="menuOrderId">The id of the menu order to edit.</param>
    /// <param name="menuOrderIM">The menu order to edit.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [HttpPut("{menuOrderId}")]
    public async Task<IActionResult> EditMenuOrderAsync(string menuOrderId, MenuOrderIM menuOrderIM)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to edit a menu order with id: {menuOrderId}.");

        var menuOrder = await this.menuOrderService.GetMenuOrderByIdAsync(menuOrderId);

        if (menuOrder is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to edit a menu order with id: {menuOrderId} but the menu order was not found.");

            return this.NotFound();
        }

        if (menuOrder.UserId != this.currentUser.UserId)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to edit a menu order with id: {menuOrderId} but the menu order does not belong to the user.");

            return this.Forbid();
        }

        await this.menuOrderService.EditMenuOrderAsync(menuOrderId, menuOrderIM);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) edited a menu order with id: {menuOrderId}.");

        return this.Ok();
    }
}
