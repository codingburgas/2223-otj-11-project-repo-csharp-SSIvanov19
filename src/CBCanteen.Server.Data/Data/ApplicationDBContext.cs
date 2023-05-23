// <copyright file="ApplicationDBContext.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using CBCanteen.Server.Data.Models.Canteen;
using Microsoft.EntityFrameworkCore;

namespace CBCanteen.Server.Data.Data;

/// <summary>
/// The database context for the application.
/// </summary>
public class ApplicationDBContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDBContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="Meal"/> table.
    /// </summary>
    public virtual DbSet<Meal> Meals { get; set; }
}
