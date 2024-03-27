using CBCanteen.Shared.Models.Canteen.Meal;

namespace CBCanteen.Shared.Models.Canteen.Stats;

/// <summary>
/// Statistics for a single meal.
/// </summary>
public class SingleMealStats
{
    /// <summary>
    /// Gets or sets a Menu item.
    /// </summary>
    public MealVM Meal { get; set; } = new ();

    /// <summary>
    /// Gets or sets the number of items ordered.
    /// </summary>
    public int Quantity { get; set; } = 0;
}