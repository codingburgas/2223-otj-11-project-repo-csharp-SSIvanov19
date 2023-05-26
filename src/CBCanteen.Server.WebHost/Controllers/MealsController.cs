// <copyright file="MealsController.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using AutoMapper;
using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.Canteen;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="MealsController"/> class.
    /// </summary>
    /// <param name="mapper">Mapper.</param>
    /// <param name="mealService">Meal Service.</param>
    public MealsController(IMapper mapper, IMealService mealService)
    {
        this.mapper = mapper;
        this.mealService = mealService;
    }

    /// <summary>
    /// Endpoint to create a new meal.
    /// </summary>
    /// <param name="mealIm">Meal input model.</param>
    /// <returns>The newly created meal as view model.</returns>
    [HttpPost]
    public async Task<ActionResult<MealVM>> CreateMealVMAsync([FromBody] MealIM mealIm)
    {
        var meal = await this.mealService.GenerateAndSaveMealInfoAsync(mealIm);

        return this.Ok(meal);
    }

    /// <summary>
    /// Retrieves all meals from the server asynchronous.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation and contains the retrieved meals as view model.</returns>
    [HttpGet]
    public async Task<ActionResult<List<MealVM>>> GetAllMealsAsync()
    {
        var meals = await this.mealService.GetAllMealsAsync();
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
        var meal = await this.mealService.GetMealByIdAsync(mealId);

        if (meal is null)
        {
            return this.NotFound();
        }

        await this.mealService.EditMealAsync(mealId, mealIm);

        return this.Ok();
    }
}
