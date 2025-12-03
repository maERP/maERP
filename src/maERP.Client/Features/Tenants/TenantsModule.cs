using maERP.Client.Core.Constants;
using maERP.Client.Core.Services.NameGeneration;
using maERP.Client.Features.Tenants.Models;
using maERP.Client.Features.Tenants.Services;
using maERP.Client.Features.Tenants.Views;

namespace maERP.Client.Features.Tenants;

/// <summary>
/// Module registration for Tenants feature.
/// Provides listing and editing of tenants the user has access to.
/// </summary>
public static class TenantsModule
{
    /// <summary>
    /// Registers Tenants services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Feature-specific services
        // TenantService: Transient - stateless, creates new instance per request
        services.AddTransient<ITenantService, TenantService>();

        // Name generators - Singleton for consistent randomization per session
        services.AddSingleton<INameGeneratorFactory, NameGeneratorFactory>();

        // Page models
        services.AddTransient<TenantListModel>();
        services.AddTransient<TenantDetailModel>();
        services.AddTransient<TenantEditModel>();
        services.AddTransient<DemoDataGeneratorModel>();

        return services;
    }

    /// <summary>
    /// Registers Tenants views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<TenantListPage, TenantListModel>(),
            new ViewMap<TenantDetailPage, TenantDetailModel>(Data: new DataMap<TenantDetailData>()),
            new ViewMap<TenantEditPage, TenantEditModel>(Data: new DataMap<TenantEditData>()),
            new ViewMap<DemoDataGeneratorPage, DemoDataGeneratorModel>(Data: new DataMap<DemoDataGeneratorData>())
        );
    }

    /// <summary>
    /// Gets the routes for the Tenants feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.TenantList, View: views.FindByViewModel<TenantListModel>());
        yield return new RouteMap(Routes.TenantDetail, View: views.FindByViewModel<TenantDetailModel>());
        yield return new RouteMap(Routes.TenantEdit, View: views.FindByViewModel<TenantEditModel>());
        yield return new RouteMap(Routes.TenantCreate, View: views.FindByViewModel<TenantEditModel>());
        yield return new RouteMap(Routes.DemoDataGenerator, View: views.FindByViewModel<DemoDataGeneratorModel>());
    }
}
