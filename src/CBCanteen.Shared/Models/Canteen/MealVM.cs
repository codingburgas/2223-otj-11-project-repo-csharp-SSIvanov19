namespace CBCanteen.Shared.Models.Canteen;

/// <summary>
/// Represents the view model for a meal in the canteen.
/// </summary>
public class MealVM
{
    /// <summary>
    /// The id of the meal.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The name of the meal.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The unit price of the meal.
    /// </summary>
    public double UnitPrice { get; set; } = 0d;

    /// <summary>
    /// The weight of the meal.
    /// </summary>
    public int Weight { get; set; } = 0;

    /// <summary>
    /// The category of the meal.
    /// </summary>
    public string Category { get; set; } = string.Empty;
}
