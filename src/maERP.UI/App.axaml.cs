using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

using maERP.UI.ViewModels;
using maERP.UI.Views;
using maERP.UI.Services;

namespace maERP.UI;

public partial class App : Application
{
    private ServiceProvider? _serviceProvider;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        ConfigureServices();
    }

    private void ConfigureServices()
    {
        var services = new ServiceCollection();

        // Register HttpClient and global HttpService
        services.AddHttpClient<IHttpService, HttpService>();

        // Register services
        services.AddSingleton<IHttpService, HttpService>();
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddSingleton<ISettingsService, SettingsService>();

        // Register ViewModels
        services.AddSingleton<MainViewModel>();
        services.AddTransient<LoginViewModel>();
        
        services.AddTransient<AiModelListViewModel>();
        services.AddTransient<AiModelDetailViewModel>();
        services.AddTransient<AiModelInputViewModel>();
        services.AddTransient<AiPromptListViewModel>();
        services.AddTransient<AiPromptDetailViewModel>();
        services.AddTransient<AiPromptInputViewModel>();
        services.AddTransient<CustomerListViewModel>();
        services.AddTransient<CustomerDetailViewModel>();
        services.AddTransient<CustomerInputViewModel>();
        services.AddTransient<DashboardViewModel>();
        services.AddTransient<InvoiceListViewModel>();
        services.AddTransient<OrderListViewModel>();
        services.AddTransient<OrderDetailViewModel>();
        services.AddTransient<ProductListViewModel>();
        services.AddTransient<ProductDetailViewModel>();
        services.AddTransient<SalesChannelListViewModel>();
        services.AddTransient<SalesChannelDetailViewModel>();        
        services.AddTransient<TaxClassListViewModel>();        
        services.AddTransient<UserListViewModel>();        
        services.AddTransient<WarehouseListViewModel>();
        services.AddTransient<WarehouseDetailViewModel>();
        
        _serviceProvider = services.BuildServiceProvider();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        var mainViewModel = _serviceProvider?.GetRequiredService<MainViewModel>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = mainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
