using maERP.Client.Core.Constants;
using maERP.Client.Features.Dashboard.Models;
using maERP.Client.Features.Dashboard.Services;
using maERP.Client.Features.Dashboard.Views;

namespace maERP.Client.Features.Dashboard;

/// <summary>
/// Module registration for Dashboard feature.
/// The Dashboard is the main entry point after authentication.
/// </summary>
public static class DashboardModule
{
    /// <summary>
    /// Registers Dashboard services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IStatisticsService, StatisticsService>();
        services.AddTransient<DashboardModel>();
        return services;
    }

    /// <summary>
    /// Registers Dashboard views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<DashboardPage, DashboardModel>()
        );
    }

    /// <summary>
    /// Gets the routes for the Dashboard feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.Dashboard, View: views.FindByViewModel<DashboardModel>(), IsDefault: true);
    }
}
