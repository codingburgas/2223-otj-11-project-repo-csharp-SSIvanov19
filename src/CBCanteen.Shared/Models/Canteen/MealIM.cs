namespace CBCanteen.Shared.Models.Canteen;

public class MealIM
{
    public string Name { get; set; } = string.Empty;

    public double UnitPrice { get; set; } = 0d;

    public int Weight { get; set; } = 0;

    public MealCategories Category { get; set; }
}
