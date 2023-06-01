// <copyright file="MealsController.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using AutoMapper;
using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.Meal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CBCanteen.Server.WebHost.Controllers;

/// <summary>
/// Controller for meals.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class MealsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMealService mealService;
    private readonly ICurrentUser currentUser;
    private readonly ILogger<MealsController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MealsController"/> class.
    /// </summary>
    /// <param name="mapper">Mapper.</param>
    /// <param name="mealService">Meal Service.</param>
    /// <param name="currentUser">Current user.</param>
    /// <param name="logger">Logger.</param>
    public MealsController(
        IMapper mapper,
        IMealService mealService,
        ICurrentUser currentUser,
        ILogger<MealsController> logger)
    {
        this.mapper = mapper;
        this.mealService = mealService;
        this.currentUser = currentUser;
        this.logger = logger;
    }

    /// <summary>
    /// Endpoint to create a new meal.
    /// </summary>
    /// <param name="mealIm">Meal input model.</param>
    /// <returns>The newly created meal as view model.</returns>
    [HttpPost]
    public async Task<ActionResult<MealVM>> CreateMealVMAsync([FromBody] MealIM mealIm)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to create a meal.");

        var meal = await this.mealService.GenerateAndSaveMealInfoAsync(mealIm);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) created a meal with id: {meal.Id}.");

        return this.Ok(meal);
    }

    /// <summary>
    /// Retrieves all meals from the server asynchronous.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation and contains the retrieved meals as view model.</returns>
    [HttpGet]
    public async Task<ActionResult<List<MealVM>>> GetAllMealsAsync()
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to get all meals.");

        var meals = await this.mealService.GetAllMealsAsync();

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) got all meals.");

        return this.Ok(meals);
    }

    /// <summary>
    /// Endpoint to edit a meal.
    /// </summary>
    /// <param name="mealId">ID of the meal to be edited.</param>
    /// <param name="mealIm">Updated meal input model.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPut("{mealId}")]
    public async Task<IActionResult> EditMealAsync(string mealId, [FromBody] MealIM mealIm)
    {
        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) is trying to edit a meal with id: {mealId}.");

        var meal = await this.mealService.GetMealByIdAsync(mealId);

        if (meal is null)
        {
            this.logger.LogWarning($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) tried to edit a meal with id: {mealId} but the meal was not found.");

            return this.NotFound();
        }

        await this.mealService.EditMealAsync(mealId, mealIm);

        this.logger.LogInformation($"User with email: {this.currentUser.UserEmail} ({this.currentUser.UserId}) edited a meal with id: {mealId}.");

        return this.Ok();
    }
}
