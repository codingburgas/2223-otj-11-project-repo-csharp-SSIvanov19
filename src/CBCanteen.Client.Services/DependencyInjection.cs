// <copyright file="DependencyInjection.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Client.Services.Contracts;
using CBCanteen.Client.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace CBCanteen.Client.Services;

/// <summary>
/// Dependency Injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add Services.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IUserService, UserService>()
            .AddScoped<ICalendarService, CalendarService>()
            .AddScoped<IMealService, MealService>()
            .AddScoped<IMenuService, MenuService>()
            .AddScoped<IDailyOrderService, DailyOrderService>()
            .AddScoped<IUserPreferenceService, UserPreferenceService>()
            .AddScoped<ILunchHourService, LunchHourService>()
            .AddScoped<IMealOrderService, MealOrderService>()
            .AddScoped<IMenuOrderService, MenuOrderService>();
    }
}
