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

    /// <summary>
    /// Gets or sets the <see cref="Menu"/> table.
    /// </summary>
    public virtual DbSet<Menu> Menus { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="MenuPrice"/> table.
    /// </summary>
    public virtual DbSet<MenuPrice> MenuPrices { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DailyOrder"/> table.
    /// </summary>
    public virtual DbSet<DailyOrder> DailyOrders { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DailyOrderMeal"/> table.
    /// </summary>
    public virtual DbSet<DailyOrderMeal> DailyOrderMeals { get; set; }

    /// <summary>
    /// On model creating.
    /// </summary>
    /// <param name="builder">Builder.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<DailyOrder>()
            .HasMany(dor => dor.FreeConsumption)
            .WithMany(m => m.DailyOrders)
            .UsingEntity<DailyOrderMeal>()
            .HasKey(e => new { e.DailyOrderId, e.MealId });
    }
}
