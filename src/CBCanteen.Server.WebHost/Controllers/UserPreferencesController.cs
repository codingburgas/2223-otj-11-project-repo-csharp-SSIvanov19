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
    private readonly ILogger<UserPreferencesController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserPreferencesController"/> class.
    /// </summary>
    /// <param name="currentUser">Service providing information about the currently logged in user.</param>
    /// <param name="userPreferenceService">Service handling user preferences.</param>
    /// <param name="logger">Logger.</param>
    public UserPreferencesController(
        ICurrentUser currentUser,
        IUserPreferenceService userPreferenceService,
        ILogger<UserPreferencesController> logger)
    {
        this.currentUser = currentUser;
        this.userPreferenceService = userPreferenceService;
        this.logger = logger;
    }

    /// <summary>
    /// Returns the user's preferences.
    /// </summary>
    /// <returns>The user's <see cref="UserPreferenceVM"/>.</returns>
    [HttpGet]
    public async Task<ActionResult<UserPreferenceVM>> GetUserPreferenceAsync()
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to get their preferences.");

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
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to set their preferences.");

        await this.userPreferenceService.SetUserPreferenceAsync(this.currentUser.UserId, userPreferenceIM);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) successfully set their preferences.");

        return this.Ok();
    }
}
