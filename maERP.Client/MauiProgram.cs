using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using maERP.Shared.Services;
using maERP.Shared.Contracts;
using maERP.Shared.Providers;

namespace maERP.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped(typeof(IDataService<>), typeof(DataService<>));



        return builder.Build();
    }
}