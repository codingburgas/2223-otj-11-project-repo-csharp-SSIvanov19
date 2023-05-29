// <copyright file="LunchHourController.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CBCanteen.Server.WebHost.Controllers;

/// <summary>
/// Controller handling lunch hours.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LunchHourController : ControllerBase
{
    private readonly ILunchHoursService lunchHoursService;
    private readonly ICurrentUser currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="LunchHourController"/> class.
    /// </summary>
    /// <param name="lunchHoursService">Lunch hour service.</param>
    /// <param name="currentUser">Current user.</param>
    public LunchHourController(ILunchHoursService lunchHoursService, ICurrentUser currentUser)
    {
        this.lunchHoursService = lunchHoursService;
        this.currentUser = currentUser;
    }

    /// <summary>
    /// Gets the lunch hours of the current user.
    /// </summary>
    /// <returns>The lunch hours of the current user.</returns>
    [HttpGet]
    public async Task<ActionResult<UserLunchHoursVM?>> GetUserLunchHoursAsync()
    {
        return this.Ok(await this.lunchHoursService.GetUserLunchHoursAsync(this.currentUser.UserId));
    }

    /// <summary>
    /// Sets the lunch hours of the current user.
    /// </summary>
    /// <param name="userLunchHoursIM">The user lunch hours input model.</param>
    /// <returns>The lunch hours of the current user.</returns>
    [HttpPost]
    public async Task<IActionResult> SetUserLunchHoursAsync([FromBody] UserLunchHoursIM userLunchHoursIM)
    {
        await this.lunchHoursService.SetUserLunchHoursAsync(userLunchHoursIM, this.currentUser.UserId);

        return this.Ok();
    }
}
