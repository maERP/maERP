using maERP.Client.Contracts.Services;
using maERP.Client.Services;
using maERP.Client.ViewModels;
using maERP.Client.Views;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<DashboardModel>();
        builder.Services.AddTransient<OrdersPage>();
        builder.Services.AddTransient<OrdersViewModel>();
        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<ProductsViewModel>();
		builder.Services.AddTransient<SettingsPage>();
		builder.Services.AddTransient<SettingsViewModel>();

		// builder.Services.AddScoped(typeof(IDataService<>), typeof(IDataService<>));
		builder.Services.AddSingleton<INavigationService, NavigationService>();

        return builder.Build();
	}
}