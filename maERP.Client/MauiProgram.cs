using maERP.Client.Contracts;
using maERP.Client.Services;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace maERP.Client
{
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
		    builder.Logging.AddDebug();
#endif

            /*
            builder.Services.AddTransient<DashboardPage>();
            builder.Services.AddTransient<DashboardModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<OrdersPage>();
            builder.Services.AddTransient<OrdersViewModel>();
            builder.Services.AddTransient<ProductsPage>();
            builder.Services.AddTransient<ProductsViewModel>();
            builder.Services.AddTransient<ProductsDetailPage>();
            builder.Services.AddTransient<ProductsDetailViewModel>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<SettingsViewModel>();
            */

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthStateProvider>());

            builder.Services.AddScoped(typeof(IDataService<>), typeof(DataService<>));
            // builder.Services.AddSingleton<INavigationService, NavigationService>();

            return builder.Build();
        }
    }
}