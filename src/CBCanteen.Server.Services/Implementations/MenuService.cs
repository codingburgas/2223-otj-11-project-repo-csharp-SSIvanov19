// <copyright file="MenuService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using AutoMapper;
using AutoMapper.QueryableExtensions;
using CBCanteen.Server.Data.Data;
using CBCanteen.Server.Data.Models.Canteen;
using CBCanteen.Server.Services.Contracts;
using CBCanteen.Shared.Models.Canteen.Menu;
using Microsoft.EntityFrameworkCore;

namespace CBCanteen.Server.Services.Implementations;

/// <summary>
/// Service for menu operations.
/// </summary>
internal class MenuService : IMenuService
{
    private readonly ApplicationDBContext context;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuService"/> class.
    /// </summary>
    /// <param name="context">DB Context.</param>
    /// <param name="mapper">Mapper.</param>
    public MenuService(ApplicationDBContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task EditMenuAsync(string menuId, MenuIM newMenuInfo)
    {
        var menu = await this.context.Menus.FindAsync(menuId);

        menu!.AppetizerId = newMenuInfo.AppetizerId;
        menu.MainDishId = newMenuInfo.MainDishId;
        menu.DessertId = newMenuInfo.DessertId;

        await this.SaveMenuAsync(menu);
    }

    /// <inheritdoc/>
    public async Task<MenuVM> GenerateAndSaveMenuInfoAsync(MenuIM menuIm)
    {
        var menu = this.mapper.Map<Menu>(menuIm);

        menu.Id = Guid.NewGuid().ToString();

        await this.SaveMenuAsync(menu);

        return this.mapper.Map<MenuVM>(menu);
    }

    /// <inheritdoc/>
    public async Task<List<MenuVM>> GetAllMenusAsync()
    {
        return await this.context.Menus
            .ProjectTo<MenuVM>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<MenuVM?> GetMenuByIdAsync(string menuId)
    {
        return this.mapper.Map<MenuVM>(await this.context.Menus
            .FindAsync(menuId));
    }

    /// <inheritdoc/>
    public async Task SaveMenuAsync(Menu menu)
    {
        if (this.context.Menus!.Any(o => o.Id == menu.Id))
        {
            this.context.Update(menu);
        }
        else
        {
            this.context.Add(menu);
        }

        await this.context.SaveChangesAsync();
    }
}
