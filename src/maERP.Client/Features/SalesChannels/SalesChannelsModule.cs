using maERP.Client.Core.Constants;
using maERP.Client.Features.SalesChannels.Models;
using maERP.Client.Features.SalesChannels.Services;
using maERP.Client.Features.SalesChannels.Views;

namespace maERP.Client.Features.SalesChannels;

/// <summary>
/// Module registration for SalesChannels feature.
/// Provides operations for sales channel management.
/// </summary>
public static class SalesChannelsModule
{
    /// <summary>
    /// Registers SalesChannels services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Feature-specific services
        // SalesChannelService: Transient - stateless, creates new instance per request
        services.AddTransient<ISalesChannelService, SalesChannelService>();

        // Page models
        services.AddTransient<SalesChannelListModel>();

        return services;
    }

    /// <summary>
    /// Registers SalesChannels views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<SalesChannelListPage, SalesChannelListModel>()
        );
    }

    /// <summary>
    /// Gets the routes for the SalesChannels feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.SalesChannelList, View: views.FindByViewModel<SalesChannelListModel>());
    }
}
