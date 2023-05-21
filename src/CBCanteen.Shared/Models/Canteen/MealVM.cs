namespace CBCanteen.Shared.Models.Canteen;

public class MealVM
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public double UnitPrice { get; set; } = 0d;

    public int Weight { get; set; } = 0;

    public string Category { get; set; } = string.Empty;
    
}
