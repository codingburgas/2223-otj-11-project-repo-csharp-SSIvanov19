namespace CBCanteen.Server.Services.Contracts;

/// <summary>
/// Interface for menu price service.
/// </summary>
public interface IMenuPriceService
{
    /// <summary>
    /// Sets the default price for menus.
    /// </summary>
    /// <param name="price">Default price.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task SetDefaultPriceForMenusAsync(double price);

    /// <summary>
    /// Gets the current default menu price.
    /// </summary>
    /// <returns>Current price if any.</returns>
    Task<double?> GetMenuPriceAsync();
}
