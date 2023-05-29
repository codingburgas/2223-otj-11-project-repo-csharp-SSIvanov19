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

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuOrdersController"/> class.
    /// </summary>
    /// <param name="menuOrderService">Service for menu ordering.</param>
    /// <param name="currentUser">Current user.</param>
    /// <param name="menuService">Menu service.</param>
    public MenuOrdersController(
        IMenuOrderService menuOrderService,
        ICurrentUser currentUser,
        IMenuService menuService)
    {
        this.menuOrderService = menuOrderService;
        this.currentUser = currentUser;
        this.menuService = menuService;
    }

    /// <summary>
    /// Creates a new menu order.
    /// </summary>
    /// <param name="menuOrderIM">Create menu order input model.</param>
    /// <returns>The created menu order.</returns>
    [HttpPost]
    public async Task<ActionResult<MenuOrderVM>> CreateMenuOrderAsync([FromBody] MenuOrderIM menuOrderIM)
    {
        var menu = await this.menuService.GetMenuByIdAsync(menuOrderIM.MenuId);

        if (menu is null)
        {
            return this.NotFound();
        }

        var menuOrder = await this.menuOrderService.GenerateAndSaveMenuOrderInfoAsync(menuOrderIM, this.currentUser.UserId);

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
        var menuOrder = await this.menuOrderService.GetMenuOrderByIdAsync(menuOrderId);

        if (menuOrder is null)
        {
            return this.NotFound();
        }

        if (menuOrder.UserId != this.currentUser.UserId)
        {
            return this.Forbid();
        }

        await this.menuOrderService.DeleteMenuOrderAsync(menuOrderId);

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
        var menuOrders = await this.menuOrderService.GetMenuOrdersBetweenDates(startDateTime, endDateTime);

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
        var menuOrder = await this.menuOrderService.GetMenuOrderByIdAsync(menuOrderId);

        if (menuOrder is null)
        {
            return this.NotFound();
        }

        if (menuOrder.UserId != this.currentUser.UserId)
        {
            return this.Forbid();
        }

        await this.menuOrderService.EditMenuOrderAsync(menuOrderId, menuOrderIM);

        return this.Ok();
    }
}
