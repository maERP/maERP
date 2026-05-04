using maERP.Client.Core.Constants;
using maERP.Client.Features.SystemOAuthSettings.Models;
using maERP.Client.Features.SystemOAuthSettings.Services;
using maERP.Client.Features.SystemOAuthSettings.Views;

namespace maERP.Client.Features.SystemOAuthSettings;

public static class SystemOAuthSettingsModule
{
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        services.AddTransient<ISystemOAuthSettingsService, SystemOAuthSettingsService>();
        services.AddTransient<SystemOAuthSettingsModel>();
        return services;
    }

    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(new ViewMap<SystemOAuthSettingsPage, SystemOAuthSettingsModel>());
    }

    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.SystemOAuthSettings, View: views.FindByViewModel<SystemOAuthSettingsModel>());
    }
}
