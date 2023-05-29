// <copyright file="UserPreferencesController.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CBCanteen.Server.WebHost.Controllers;

/// <summary>
/// Controller handling user preferences.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserPreferencesController : ControllerBase
{
    private readonly ICurrentUser currentUser;
    private readonly IUserPreferenceService userPreferenceService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserPreferencesController"/> class.
    /// </summary>
    /// <param name="currentUser">Service providing information about the currently logged in user.</param>
    /// <param name="userPreferenceService">Service handling user preferences.</param>
    public UserPreferencesController(ICurrentUser currentUser, IUserPreferenceService userPreferenceService)
    {
        this.currentUser = currentUser;
        this.userPreferenceService = userPreferenceService;
    }

    /// <summary>
    /// Returns the user's preferences.
    /// </summary>
    /// <returns>The user's <see cref="UserPreferenceVM"/>.</returns>
    [HttpGet]
    public async Task<ActionResult<UserPreferenceVM>> GetUserPreferenceAsync()
    {
        return this.Ok(await this.userPreferenceService.GetUserPreferenceAsync(this.currentUser.UserId));
    }

    /// <summary>
    /// Sets the user's preferences.
    /// </summary>
    /// <param name="userPreferenceIM">The <see cref="UserPreferenceIM"/> containing the new user's preferences.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [HttpPost]
    public async Task<IActionResult> SetUserPreferenceAsync([FromBody] UserPreferenceIM userPreferenceIM)
    {
        await this.userPreferenceService.SetUserPreferenceAsync(this.currentUser.UserId, userPreferenceIM);

        return this.Ok();
    }
}
