
using CBCanteen.Server.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;


namespace CBCanteen.Server.Services;

/// <summary>
/// Dependency Injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add Services.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IMealService, IMealService>();
    }
}