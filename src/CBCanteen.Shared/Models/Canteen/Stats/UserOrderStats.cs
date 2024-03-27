// <copyright file="UserOrderStats.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Shared.Models.Canteen.Meal;
using CBCanteen.Shared.Models.Canteen.Menu;

namespace CBCanteen.Shared.Models.Canteen.Stats;

/// <summary>
/// Represents user's order statistics.
/// </summary>
public class UserOrderStats
{
    /// <summary>
    /// Gets or sets user's id.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets user's name.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of meals ordered by the user.
    /// </summary>
    public List<SingleMealStats> Meals { get; set; } = new List<SingleMealStats>();

    /// <summary>
    /// Gets or sets the list of menus ordered by the user.
    /// </summary>
    public List<SingleMenuStats> Menus { get; set; } = new List<SingleMenuStats>();
}
