using maERP.Client.Core.Constants;
using maERP.Client.Features.SalesChannelDashboards.Models;
using maERP.Client.Features.SalesChannelDashboards.Services;
using maERP.Client.Features.SalesChannelDashboards.Views;

namespace maERP.Client.Features.SalesChannelDashboards;

/// <summary>
/// Module registration for SalesChannel Dashboards feature.
/// Provides per-SalesChannel dashboard pages with KPIs and type-specific features.
/// </summary>
public static class SalesChannelDashboardsModule
{
    /// <summary>
    /// Registers SalesChannel Dashboard services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        services.AddTransient<ISalesChannelStatisticsService, SalesChannelStatisticsService>();
        services.AddTransient<PosDashboardModel>();
        services.AddTransient<Shopware5DashboardModel>();

        return services;
    }

    /// <summary>
    /// Registers SalesChannel Dashboard views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<PosDashboardPage, PosDashboardModel>(Data: new DataMap<SalesChannelDashboardData>()),
            new ViewMap<Shopware5DashboardPage, Shopware5DashboardModel>(Data: new DataMap<SalesChannelDashboardData>())
        );
    }

    /// <summary>
    /// Gets the routes for the SalesChannel Dashboards feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.PosDashboard, View: views.FindByViewModel<PosDashboardModel>());
        yield return new RouteMap(Routes.Shopware5Dashboard, View: views.FindByViewModel<Shopware5DashboardModel>());
    }
}
