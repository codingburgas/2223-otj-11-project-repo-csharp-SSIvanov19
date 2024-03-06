// <copyright file="MealStats.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.Models.Canteen.Meal;
using CBCanteen.Shared.Models.Canteen.Menu;

namespace CBCanteen.Shared.Models.Canteen.Stats;

/// <summary>
/// Represents the statistics for a given day.
/// </summary>
public class MealStats
{
    /// <summary>
    /// Gets or sets the date to which the statistics apply.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the list of meals for the given day.
    /// </summary>
    public List<MealVM> Meals { get; set; } = new ();

    /// <summary>
    /// Gets or sets the list of menus for the given day.
    /// </summary>
    public List<MenuVM> Menus { get; set; } = new ();
}
