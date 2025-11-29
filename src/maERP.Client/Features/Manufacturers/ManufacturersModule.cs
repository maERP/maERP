using maERP.Client.Core.Constants;
using maERP.Client.Features.Manufacturers.Models;
using maERP.Client.Features.Manufacturers.Services;
using maERP.Client.Features.Manufacturers.Views;

namespace maERP.Client.Features.Manufacturers;

/// <summary>
/// Module registration for Manufacturers feature.
/// Provides list view for manufacturer management.
/// </summary>
public static class ManufacturersModule
{
    /// <summary>
    /// Registers Manufacturers services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Feature-specific services
        services.AddTransient<IManufacturerService, ManufacturerService>();

        // Page models
        services.AddTransient<ManufacturerListModel>();

        return services;
    }

    /// <summary>
    /// Registers Manufacturers views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<ManufacturerListPage, ManufacturerListModel>()
        );
    }

    /// <summary>
    /// Gets the routes for the Manufacturers feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.ManufacturerList, View: views.FindByViewModel<ManufacturerListModel>());
    }
}
