using System.ComponentModel.DataAnnotations;

namespace CBCanteen.Shared.Models.Canteen;

/// <summary>
/// Represents an order-able meal in the canteen.
/// </summary>
public class MealIM
{
    /// <summary>
    /// The name of the meal.
    /// </summary>
    [Required(ErrorMessage = "Please enter a name for the meal.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The unit price of the meal.
    /// </summary>
    [Required(ErrorMessage = "Please enter a unit price for the meal.")]
    public double UnitPrice { get; set; } = 0d;

    /// <summary>
    /// The weight of the meal.
    /// </summary>
    [Required(ErrorMessage = "Please enter a weight for the meal.")]
    public int Weight { get; set; } = 0;

    /// <summary>
    /// The category of the meal.
    /// </summary>
    [Required(ErrorMessage = "Please select a category for the meal.")]
    public MealCategories Category { get; set; }
}
