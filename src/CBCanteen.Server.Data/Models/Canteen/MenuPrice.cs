// <copyright file="MenuPrice.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

namespace CBCanteen.Server.Data.Models.Canteen;

/// <summary>
/// Represents the default price of a menu.
/// </summary>
public class MenuPrice
{
    /// <summary>
    /// Gets or sets the unique identifier of the menu price.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the menu.
    /// </summary>
    public double Price { get; set; } = 4.5;
}
