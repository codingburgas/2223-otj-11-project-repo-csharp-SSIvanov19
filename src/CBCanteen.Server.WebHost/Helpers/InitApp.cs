// <copyright file="InitApp.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Services.Contracts;

namespace CBCanteen.Server.WebHost.Helpers;

/// <summary>
/// A class for initializing the application.
/// </summary>
public static class InitApp
{
    /// <summary>
    /// Initializes the application asynchronously.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task InitAppAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var menuPriceService = scope.ServiceProvider.GetRequiredService<IMenuPriceService>();

        var price = await menuPriceService.GetMenuPriceAsync();

        // If there is already a record, return
        if (price is not null)
        {
            return;
        }

        await menuPriceService.SetDefaultPriceForMenusAsync(5.5);
    }
}
