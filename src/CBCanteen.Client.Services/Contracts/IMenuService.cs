// <copyright file="IMenuService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.Models.Canteen.Meal;
using CBCanteen.Shared.Models.Canteen.Menu;

namespace CBCanteen.Client.Services.Contracts;

/// <summary>
/// Service for interacting with the menu.
/// </summary>
public interface IMenuService
{
    /// <summary>
    /// Creates a new menu item.
    /// </summary>
    /// <param name="menuIm">The new menu item.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<MenuVM> CreateMenuAsync(MenuIM menuIm);

    /// <summary>
    /// Edits an existing menu item.
    /// </summary>
    /// <param name="menuId">The ID of the menu item to edit.</param>
    /// <param name="menuIm">The updated menu item.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task EditMenuAsync(string menuId, MenuIM menuIm);

    /// <summary>
    /// Gets all meals from a menu and orders them by their category.
    /// </summary>
    /// <param name="menuVM">The menu.</param>
    /// <returns>A list of meal ordered by their category.</returns>
    List<MealVM> GetMealsFromMenuAsync(MenuVM menuVM);

    /// <summary>
    /// Gets the default price for a menu.
    /// </summary>
    /// <returns>The default price.</returns>
    Task<double> GetDefaultPriceForMenuAsync();

    /// <summary>
    /// Sets the default price for a menu.
    /// </summary>
    /// <param name="defaultPrice">The new default price.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SetDefaultPriceForMenuAsync(double defaultPrice);
}
