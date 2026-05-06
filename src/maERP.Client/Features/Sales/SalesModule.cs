using maERP.Client.Core.Constants;
using maERP.Client.Features.Saless.Models;
using maERP.Client.Features.Saless.Services;
using maERP.Client.Features.Saless.Views;

namespace maERP.Client.Features.Saless;

/// <summary>
/// Module registration for Saless feature.
/// Provides CRUD operations for sales management.
/// </summary>
public static class SalessModule
{
    /// <summary>
    /// Registers Saless services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Feature-specific services
        // SalesService: Transient - stateless, creates new instance per request
        services.AddTransient<ISalesService, SalesService>();

        // Page models
        services.AddTransient<SalesListModel>();
        services.AddTransient<SalesDetailModel>();
        services.AddTransient<SalesEditModel>();

        return services;
    }

    /// <summary>
    /// Registers Saless views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<SalesListPage, SalesListModel>(),
            new ViewMap<SalesDetailPage, SalesDetailModel>(Data: new DataMap<SalesDetailData>()),
            new ViewMap<SalesEditPage, SalesEditModel>(Data: new DataMap<SalesEditData>())
        );
    }

    /// <summary>
    /// Gets the routes for the Saless feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.SalesList, View: views.FindByViewModel<SalesListModel>());
        yield return new RouteMap(Routes.SalesDetail, View: views.FindByViewModel<SalesDetailModel>());
        yield return new RouteMap(Routes.SalesEdit, View: views.FindByViewModel<SalesEditModel>());
        yield return new RouteMap(Routes.SalesCreate, View: views.FindByViewModel<SalesEditModel>());
    }
}

/// <summary>
/// Navigation data for sales detail page.
/// </summary>
public record SalesDetailData(Guid salesId);

/// <summary>
/// Navigation data for sales edit page.
/// </summary>
public record SalesEditData(Guid salesId);
