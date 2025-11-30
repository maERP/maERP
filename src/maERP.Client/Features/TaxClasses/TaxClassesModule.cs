using maERP.Client.Core.Constants;
using maERP.Client.Features.TaxClasses.Models;
using maERP.Client.Features.TaxClasses.Services;
using maERP.Client.Features.TaxClasses.Views;

namespace maERP.Client.Features.TaxClasses;

/// <summary>
/// Module registration for TaxClasses feature.
/// Provides list, detail, and edit views for tax class management.
/// </summary>
public static class TaxClassesModule
{
    /// <summary>
    /// Registers TaxClasses services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Feature-specific services
        // TaxClassService: Transient - stateless, creates new instance per request
        services.AddTransient<ITaxClassService, TaxClassService>();

        // Page models
        services.AddTransient<TaxClassListModel>();
        services.AddTransient<TaxClassDetailModel>();
        services.AddTransient<TaxClassEditModel>();

        return services;
    }

    /// <summary>
    /// Registers TaxClasses views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<TaxClassListPage, TaxClassListModel>(),
            new ViewMap<TaxClassDetailPage, TaxClassDetailModel>(Data: new DataMap<TaxClassDetailData>()),
            new ViewMap<TaxClassEditPage, TaxClassEditModel>(Data: new DataMap<TaxClassEditData>())
        );
    }

    /// <summary>
    /// Gets the routes for the TaxClasses feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.TaxClassList, View: views.FindByViewModel<TaxClassListModel>());
        yield return new RouteMap(Routes.TaxClassDetail, View: views.FindByViewModel<TaxClassDetailModel>());
        yield return new RouteMap(Routes.TaxClassEdit, View: views.FindByViewModel<TaxClassEditModel>());
    }
}

/// <summary>
/// Navigation data for tax class detail page.
/// </summary>
public record TaxClassDetailData(Guid TaxClassId);

/// <summary>
/// Navigation data for tax class edit page.
/// </summary>
public record TaxClassEditData(Guid? TaxClassId = null);
