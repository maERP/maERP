using maERP.Client.Core.Constants;
using maERP.Client.Features.Account.Models;
using maERP.Client.Features.Account.Services;
using maERP.Client.Features.Account.Views;

namespace maERP.Client.Features.Account;

/// <summary>
/// Module registration for the "Mein Konto" feature: own profile + password change.
/// </summary>
public static class AccountModule
{
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<AccountModel>();
        return services;
    }

    public static void RegisterViews(IViewRegistry views)
    {
        views.Register(
            new ViewMap<AccountPage, AccountModel>()
        );
    }

    public static IEnumerable<RouteMap> GetRoutes(IViewRegistry views)
    {
        yield return new RouteMap(Routes.UserProfile, View: views.FindByViewModel<AccountModel>());
    }
}
