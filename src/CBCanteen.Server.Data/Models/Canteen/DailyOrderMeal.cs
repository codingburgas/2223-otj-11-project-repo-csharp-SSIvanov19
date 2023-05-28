using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBCanteen.Server.Data.Models.Canteen;

/// <summary>
/// Represents the bridge table between daily order and meal entities.
/// </summary>
public class DailyOrderMeal
{
    /// <summary>
    /// Gets or sets the ID of the daily order item in this combination.
    /// </summary>
    public string DailyOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the meal item in this combination.
    /// </summary>
    public string MealId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the daily order item in this combination.
    /// </summary>
    [ForeignKey(nameof(DailyOrderId))]
    public DailyOrder DailyOrder { get; set; } = null!;

    /// <summary>
    /// Gets or sets the meal item in this combination.
    /// </summary>
    [ForeignKey(nameof(MealId))]
    public Meal Meal { get; set; } = null!;
}
