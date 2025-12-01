using maERP.Client.Core.Constants;
using maERP.Client.Features.Superadmin.Models;
using maERP.Client.Features.Superadmin.Services;
using maERP.Client.Features.Superadmin.Views;

namespace maERP.Client.Features.Superadmin;

/// <summary>
/// Module registration for Superadmin feature.
/// Provides access to all tenants for users with Superadmin role.
/// </summary>
public static class SuperadminModule
{
    /// <summary>
    /// Registers Superadmin services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Feature-specific services
        // SuperadminTenantService: Transient - stateless, creates new instance per request
        services.AddTransient<ISuperadminTenantService, SuperadminTenantService>();

        // Page models
        services.AddTransient<SuperadminTenantListModel>();
        services.AddTransient<SuperadminTenantEditModel>();

        return services;
    }

    /// <summary>
    /// Registers Superadmin views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<SuperadminTenantListPage, SuperadminTenantListModel>(),
            new ViewMap<SuperadminTenantEditPage, SuperadminTenantEditModel>(Data: new DataMap<SuperadminTenantEditData>())
        );
    }

    /// <summary>
    /// Gets the routes for the Superadmin feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.SuperadminTenantList, View: views.FindByViewModel<SuperadminTenantListModel>());
        yield return new RouteMap(Routes.SuperadminTenantEdit, View: views.FindByViewModel<SuperadminTenantEditModel>());
    }
}
