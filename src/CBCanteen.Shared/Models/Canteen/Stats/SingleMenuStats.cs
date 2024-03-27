using CBCanteen.Shared.Models.Canteen.Menu;

namespace CBCanteen.Shared.Models.Canteen.Stats;

/// <summary>
/// Statistics for a single menu.
/// </summary>
public class SingleMenuStats
{
    /// <summary>
    /// Gets or sets a Menu item.
    /// </summary>
    public MenuVM Menu { get; set; } = new ();

    /// <summary>
    /// Gets or sets the number of items ordered.
    /// </summary>
    public int Quantity { get; set; } = 0;
}