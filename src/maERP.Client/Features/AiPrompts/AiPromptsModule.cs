using maERP.Client.Core.Constants;
using maERP.Client.Features.AiPrompts.Models;
using maERP.Client.Features.AiPrompts.Services;
using maERP.Client.Features.AiPrompts.Views;

namespace maERP.Client.Features.AiPrompts;

/// <summary>
/// Module registration for AiPrompts feature.
/// Provides management for AI prompt configurations.
/// </summary>
public static class AiPromptsModule
{
    /// <summary>
    /// Registers AiPrompts services with the DI container.
    /// </summary>
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Feature-specific services
        // AiPromptService: Transient - stateless, creates new instance per request
        services.AddTransient<IAiPromptService, AiPromptService>();

        // Page models
        services.AddTransient<AiPromptListModel>();
        services.AddTransient<AiPromptDetailModel>();
        services.AddTransient<AiPromptEditModel>();

        return services;
    }

    /// <summary>
    /// Registers AiPrompts views with the view registry.
    /// </summary>
    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<AiPromptListPage, AiPromptListModel>(),
            new ViewMap<AiPromptDetailPage, AiPromptDetailModel>(Data: new DataMap<AiPromptDetailData>()),
            new ViewMap<AiPromptEditPage, AiPromptEditModel>(Data: new DataMap<AiPromptEditData>())
        );
    }

    /// <summary>
    /// Gets the routes for the AiPrompts feature.
    /// </summary>
    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.AiPromptList, View: views.FindByViewModel<AiPromptListModel>());
        yield return new RouteMap(Routes.AiPromptDetail, View: views.FindByViewModel<AiPromptDetailModel>());
        yield return new RouteMap(Routes.AiPromptEdit, View: views.FindByViewModel<AiPromptEditModel>());
        yield return new RouteMap(Routes.AiPromptCreate, View: views.FindByViewModel<AiPromptEditModel>());
    }
}
