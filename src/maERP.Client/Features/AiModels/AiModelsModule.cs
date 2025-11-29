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

        return services;
    }

    /// <summary>
    /// Registers AiModels views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<AiModelListPage, AiModelListModel>()
        );
    }

    /// <summary>
    /// Gets the routes for the AiModels feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.AiModelList, View: views.FindByViewModel<AiModelListModel>());
    }
}
