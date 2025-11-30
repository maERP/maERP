using maERP.Client.Core.Constants;
using maERP.Client.Features.AiModels.Models;
using maERP.Client.Features.AiModels.Services;
using maERP.Client.Features.AiModels.Views;

namespace maERP.Client.Features.AiModels;

/// <summary>
/// Module registration for AiModels feature.
/// Provides management for AI model configurations.
/// </summary>
public static class AiModelsModule
{
    /// <summary>
    /// Registers AiModels services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Feature-specific services
        // AiModelService: Transient - stateless, creates new instance per request
        services.AddTransient<IAiModelService, AiModelService>();

        // Page models
        services.AddTransient<AiModelListModel>();
        services.AddTransient<AiModelDetailModel>();
        services.AddTransient<AiModelEditModel>();

        return services;
    }

    /// <summary>
    /// Registers AiModels views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<AiModelListPage, AiModelListModel>(),
            new ViewMap<AiModelDetailPage, AiModelDetailModel>(Data: new DataMap<AiModelDetailData>()),
            new ViewMap<AiModelEditPage, AiModelEditModel>(Data: new DataMap<AiModelEditData>())
        );
    }

    /// <summary>
    /// Gets the routes for the AiModels feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.AiModelList, View: views.FindByViewModel<AiModelListModel>());
        yield return new RouteMap(Routes.AiModelDetail, View: views.FindByViewModel<AiModelDetailModel>());
        yield return new RouteMap(Routes.AiModelEdit, View: views.FindByViewModel<AiModelEditModel>());
        yield return new RouteMap(Routes.AiModelCreate, View: views.FindByViewModel<AiModelEditModel>());
    }
}
