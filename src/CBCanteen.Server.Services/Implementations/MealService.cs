// <copyright file="MealService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using AutoMapper;
using AutoMapper.QueryableExtensions;
using CBCanteen.Server.Data.Data;
using CBCanteen.Server.Data.Models.Canteen;
using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.Meal;
using Microsoft.EntityFrameworkCore;

namespace CBCanteen.Server.Services.Implementations;

/// <summary>
/// Service for meal operations.
/// </summary>
internal class MealService : IMealService
{
    private readonly ApplicationDBContext context;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="MealService"/> class.
    /// </summary>
    /// <param name="context">BD Context.</param>
    /// <param name="mapper">Mapper.</param>
    public MealService(ApplicationDBContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task EditMealAsync(string mealId, MealIM newMealInfo)
    {
        var meal = await this.context.Meals.FindAsync(mealId);

        meal!.Name = newMealInfo.Name;
        meal.UnitPrice = newMealInfo.UnitPrice;
        meal.Weight = newMealInfo.Weight;
        meal.Category = newMealInfo.Category;

        await this.SaveMealAsync(meal);
    }

    /// <inheritdoc/>
    public async Task<MealVM> GenerateAndSaveMealInfoAsync(MealIM mealIm)
    {
        var meal = this.mapper.Map<Meal>(mealIm);

        meal.Id = Guid.NewGuid().ToString();

        await this.SaveMealAsync(meal);

        return this.mapper.Map<MealVM>(meal);
    }

    /// <inheritdoc/>
    public async Task<List<MealVM>> GetAllMealsAsync()
    {
        return await this.context.Meals
            .ProjectTo<MealVM>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<MealVM?> GetMealByIdAsync(string mealId)
    {
        return this.mapper.Map<MealVM>(await this.context.Meals
            .FindAsync(mealId));
    }

    /// <inheritdoc/>
    public async Task SaveMealAsync(Meal meal)
    {
        if (this.context.Meals!.Any(o => o.Id == meal.Id))
        {
            this.context.Update(meal);
        }
        else
        {
            this.context.Add(meal);
        }

        await this.context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<List<MealVM>> SearchForMealsAsync(string keyword)
    {
        return await this.context.Meals
            .Where(meal => meal.Name.Contains(keyword))
            .ProjectTo<MealVM>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
