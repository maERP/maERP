using maERP.Client.Core.Constants;
using maERP.Client.Features.Legacy.Models;
using maERP.Client.Features.Legacy.Views;
using maERP.Client.Models;

namespace maERP.Client.Features.Legacy;

/// <summary>
/// Module for legacy pages that will be migrated or removed in the future.
/// </summary>
public static class LegacyModule
{
    /// <summary>
    /// Registers Legacy services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        services.AddTransient<SecondModel>();
        return services;
    }

    /// <summary>
    /// Registers Legacy views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new DataViewMap<SecondPage, SecondModel, Entity>()
        );
    }

    /// <summary>
    /// Gets the routes for the Legacy feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.Second, View: views.FindByViewModel<SecondModel>());
    }
}
