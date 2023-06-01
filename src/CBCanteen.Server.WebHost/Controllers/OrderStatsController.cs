﻿using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.Meal;
using CBCanteen.Shared.Models.Canteen.Menu;
using CBCanteen.Shared.Models.Canteen.Stats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CBCanteen.Server.WebHost.Controllers;

/// <summary>
/// Controller for order statistics.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class OrderStatsController : ControllerBase
{
    private readonly IMenuOrderService menuOrderService;
    private readonly IMealOrderService mealOrderService;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderStatsController"/> class.
    /// </summary>
    /// <param name="menuOrderService">Menu order service.</param>
    /// <param name="mealOrderService">Meal order service.</param>
    public OrderStatsController(IMenuOrderService menuOrderService, IMealOrderService mealOrderService)
    {
        this.menuOrderService = menuOrderService;
        this.mealOrderService = mealOrderService;
    }

    /// <summary>
    /// Gets the order statistics for the given time period.
    /// </summary>
    /// <param name="startTime">Start Time.</param>
    /// <param name="endTime">End Time.</param>
    /// <returns>List with stats for each day</returns>
    [HttpGet("meals")]
    public async Task<ActionResult<List<MealStats>>> GetMealStatsAsync(DateOnly startTime, DateOnly endTime)
    {
        if (startTime > endTime)
        {
            return this.BadRequest("Start time should be before end time.");
        }

        List<MealStats> returnVal = new ();

        do
        {
            var menus = await this.menuOrderService.GetMenuOrdersBetweenDates(startTime.ToDateTime(TimeOnly.MinValue), endTime.ToDateTime(TimeOnly.MinValue));
            var meals = await this.mealOrderService.GetMealOrdersBetweenDates(startTime.ToDateTime(TimeOnly.MinValue), endTime.ToDateTime(TimeOnly.MinValue));

            returnVal.Add(new MealStats()
            {
                Date = startTime,
                Meals = meals.Select(m => m.Meal).ToList(),
                Menus = menus.Select(m => m.Menu).ToList(),
            });
        }
        while (startTime != endTime);

        return this.Ok(returnVal);
    }

    /// <summary>
    /// Gets the order statistics for the given time period.
    /// </summary>
    /// <param name="startTime">Start time.</param>
    /// <param name="endTime">End time.</param>
    /// <returns>List with stats for the time period.</returns>
    [HttpGet("orders")]
    public async Task<ActionResult<List<OrderStats>>> GetOrderStatsAsync(DateOnly startTime, DateOnly endTime)
    {
        if (startTime > endTime)
        {
            return this.BadRequest("Start time should be before end time.");
        }

        List<OrderStats> returnVal = new ();

        do
        {
            List<UserOrderStats> orderStats = new ();
            var menus = await this.menuOrderService.GetMenuOrdersBetweenDates(startTime.ToDateTime(TimeOnly.MinValue), endTime.ToDateTime(TimeOnly.MinValue));
            var meals = await this.mealOrderService.GetMealOrdersBetweenDates(startTime.ToDateTime(TimeOnly.MinValue), endTime.ToDateTime(TimeOnly.MinValue));

            foreach (var menu in menus)
            {
                if (orderStats.Select(os => os.UserId).Contains(menu.UserId))
                {
                    orderStats.Where(os => os.UserId == menu.UserId).FirstOrDefault()!.Menus.Add(menu.Menu);
                }
                else
                {
                    orderStats.Add(new UserOrderStats()
                    {
                        UserId = menu.UserId,
                        Menus = new List<MenuVM>() { menu.Menu },
                    });
                }
            }

            foreach (var meal in meals)
            {
                if (orderStats.Select(os => os.UserId).Contains(meal.UserId))
                {
                    orderStats.Where(os => os.UserId == meal.UserId).FirstOrDefault()!.Meals.Add(meal.Meal);
                }
                else
                {
                    orderStats.Add(new UserOrderStats()
                    {
                        UserId = meal.UserId,
                        Meals = new List<MealVM>() { meal.Meal },
                    });
                }
            }

            returnVal.Add(new OrderStats()
            {
                Date = startTime,
                UserOrders = orderStats,
            });
        }
        while (startTime != endTime);

        return this.Ok(returnVal);
    }
}