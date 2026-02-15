using maERP.Client.Features.Auth.Services;

namespace maERP.Client.Features.Auth;

/// <summary>
/// Module registration for Authentication feature.
/// Handles login, logout, and authentication state management.
/// </summary>
public static class AuthModule
{
    /// <summary>
    /// Registers Auth services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Authentication services (singleton for state management)
        services.AddSingleton<ITokenStorageService, TokenStorageService>();
        services.AddSingleton<ITenantContextService, TenantContextService>();
        services.AddSingleton<IMaErpAuthenticationService, MaErpAuthenticationService>();

        // Session management
        services.AddSingleton<SessionSettings>();
        services.AddSingleton<ISessionManager, SessionManager>();

        return services;
    }

    /// <summary>
    /// Registers Auth views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        // Login is handled by Shell's LoginOverlay - no page registration needed
    }

    /// <summary>
    /// Gets the routes for the Auth feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        // Login is handled by Shell's LoginOverlay - no routes needed
        yield break;
    }
}
