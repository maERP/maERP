﻿using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;
using maERP.Shared.Services;

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

        builder.Services.AddAuthorizationCore();
        builder.Services.AddBlazoredLocalStorage();

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

        return builder.Build();
    }
}