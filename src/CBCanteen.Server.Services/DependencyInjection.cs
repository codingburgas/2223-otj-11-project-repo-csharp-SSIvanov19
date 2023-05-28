// <copyright file="DependencyInjection.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Services.Contracts;
using CBCanteen.Server.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace CBCanteen.Server.Services;

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
            .AddScoped<IMealService, MealService>()
            .AddScoped<IMenuService, MenuService>()
            .AddScoped<IMenuPriceService, MenuPriceService>()
            .AddScoped<IDailyOrderService, DailyOrderService>();
    }
}