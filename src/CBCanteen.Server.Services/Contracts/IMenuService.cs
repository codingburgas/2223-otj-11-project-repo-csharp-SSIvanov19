// <copyright file="IMenuService.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Data.Models.Canteen;
using CBCanteen.Shared.Models.Canteen.Menu;

namespace CBCanteen.Server.Services.Contracts;

/// <summary>
/// Interface for the menu service.
/// </summary>
public interface IMenuService
{
    /// <summary>
    /// Saves a menu item to the database.
    /// </summary>
    /// <param name="menu">The menu item to be saved.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task SaveMenuAsync(Menu menu);

    /// <summary>
    /// Generates and saves a menu item to the database.
    /// </summary>
    /// <param name="menu">The menu item to generate information for.</param>
    /// <returns>A generated MenuVM object.</returns>
    Task<MenuVM> GenerateAndSaveMenuInfoAsync(MenuIM menu);

    /// <summary>
    /// Edits an existing menu item in the database.
    /// </summary>
    /// <param name="menuId">The ID of the menu item to be edited.</param>
    /// <param name="newMenuInfo">The updated menu information.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task EditMenuAsync(string menuId, MenuIM newMenuInfo);

    /// <summary>
    /// Retrieves a menu item by ID from the database.
    /// </summary>
    /// <param name="menuId">The ID of the menu item to retrieve.</param>
    /// <returns>A MenuVM object, or null if not found.</returns>
    Task<MenuVM?> GetMenuByIdAsync(string menuId);

    /// <summary>
    /// Retrieves all menu items from the database.
    /// </summary>
    /// <returns>A list of all MenuVM objects.</returns>
    Task<List<MenuVM>> GetAllMenusAsync();
}
