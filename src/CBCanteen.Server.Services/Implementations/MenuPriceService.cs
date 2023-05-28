// <copyright file="MenuPriceService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Data.Data;
using CBCanteen.Server.Data.Models.Canteen;
using CBCanteen.Server.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CBCanteen.Server.Services.Implementations;

/// <summary>
/// Service for menu price.
/// </summary>
internal class MenuPriceService : IMenuPriceService
{
    private readonly ApplicationDBContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuPriceService"/> class.
    /// </summary>
    /// <param name="context">DB Context.</param>
    public MenuPriceService(ApplicationDBContext context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public async Task<double?> GetMenuPriceAsync()
    {
        return (await this.context.MenuPrices.FirstOrDefaultAsync())?.Price;
    }

    /// <inheritdoc/>
    public async Task SetDefaultPriceForMenusAsync(double price)
    {
        var menu = await this.context.MenuPrices.FirstOrDefaultAsync();

        if (menu is null)
        {
            menu = new MenuPrice
            {
                Id = Guid.NewGuid().ToString(),
                Price = price,
            };

            await this.context.MenuPrices.AddAsync(menu);
        }
        else
        {
            menu.Price = price;
        }

        await this.context.SaveChangesAsync();
    }
}
