using maERP.Client.Core.Constants;
using maERP.Client.Features.Auth.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Client.Features.Auth.Views;

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
        services.AddSingleton<IMaErpAuthenticationService, MaErpAuthenticationService>();

        // Page models (transient - new instance per navigation)
        services.AddTransient<LoginModel>();

        return services;
    }

    /// <summary>
    /// Registers Auth views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<LoginPage, LoginModel>()
        );
    }

    /// <summary>
    /// Gets the routes for the Auth feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.Login, View: views.FindByViewModel<LoginModel>());
    }
}
