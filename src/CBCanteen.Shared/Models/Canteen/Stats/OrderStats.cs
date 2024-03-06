// <copyright file="OrderStats.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

namespace CBCanteen.Shared.Models.Canteen.Stats;

/// <summary>
/// Represents the order statistics for a specific date.
/// </summary>
public class OrderStats
{
    /// <summary>
    /// Gets or sets the specific date represented by the statistics.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the list of user order statistics for the specific date.
    /// </summary>
    public List<UserOrderStats> UserOrders { get; set; } = new ();
}
