using CBCanteen.Client.Services.Contracts;
using CBCanteen.Client.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace CBCanteen.Client.Services;

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
            .AddScoped<IUserService, UserService>()
            .AddScoped<ICalendarService, CalendarService>();
    }
}
