using maERP.Client.Core.Constants;
using maERP.Client.Features.TenantOAuthSettings.Models;
using maERP.Client.Features.TenantOAuthSettings.Services;
using maERP.Client.Features.TenantOAuthSettings.Views;

namespace maERP.Client.Features.TenantOAuthSettings;

public static class TenantOAuthSettingsModule
{
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        services.AddTransient<ITenantOAuthSettingsService, TenantOAuthSettingsService>();
        services.AddTransient<TenantOAuthSettingsModel>();
        return services;
    }

    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(new ViewMap<TenantOAuthSettingsPage, TenantOAuthSettingsModel>());
    }

    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.TenantOAuthSettings, View: views.FindByViewModel<TenantOAuthSettingsModel>());
    }
}
