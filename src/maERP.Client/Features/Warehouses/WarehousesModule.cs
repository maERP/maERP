using maERP.Client.Core.Constants;
using maERP.Client.Features.Warehouses.Models;
using maERP.Client.Features.Warehouses.Services;
using maERP.Client.Features.Warehouses.Views;

namespace maERP.Client.Features.Warehouses;

/// <summary>
/// Module registration for Warehouses feature.
/// Provides list and detail operations for warehouse management.
/// </summary>
public static class WarehousesModule
{
    /// <summary>
    /// Registers Warehouses services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Feature-specific services
        // WarehouseService: Transient - stateless, creates new instance per request
        services.AddTransient<IWarehouseService, WarehouseService>();

        // Page models
        services.AddTransient<WarehouseListModel>();
        services.AddTransient<WarehouseDetailModel>();

        return services;
    }

    /// <summary>
    /// Registers Warehouses views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<WarehouseListPage, WarehouseListModel>(),
            new ViewMap<WarehouseDetailPage, WarehouseDetailModel>(Data: new DataMap<WarehouseDetailData>())
        );
    }

    /// <summary>
    /// Gets the routes for the Warehouses feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.WarehouseList, View: views.FindByViewModel<WarehouseListModel>());
        yield return new RouteMap(Routes.WarehouseDetail, View: views.FindByViewModel<WarehouseDetailModel>());
    }
}

/// <summary>
/// Navigation data for warehouse detail page.
/// </summary>
public record WarehouseDetailData(Guid WarehouseId);
