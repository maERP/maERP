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
        services.AddTransient<SalesChannelDetailModel>();
        services.AddTransient<SalesChannelEditModel>();

        return services;
    }

    /// <summary>
    /// Registers SalesChannels views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<SalesChannelListPage, SalesChannelListModel>(),
            new ViewMap<SalesChannelDetailPage, SalesChannelDetailModel>(Data: new DataMap<SalesChannelDetailData>()),
            new ViewMap<SalesChannelEditPage, SalesChannelEditModel>(Data: new DataMap<SalesChannelEditData>())
        );
    }

    /// <summary>
    /// Gets the routes for the SalesChannels feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.SalesChannelList, View: views.FindByViewModel<SalesChannelListModel>());
        yield return new RouteMap(Routes.SalesChannelDetail, View: views.FindByViewModel<SalesChannelDetailModel>());
        yield return new RouteMap(Routes.SalesChannelEdit, View: views.FindByViewModel<SalesChannelEditModel>());
    }
}
