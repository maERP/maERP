using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using maERP.UI.Features.AI.ViewModels;
using maERP.UI.Features.Administration.ViewModels;
using maERP.UI.Features.Authentication.ViewModels;
using maERP.UI.Features.Customers.ViewModels;
using maERP.UI.Features.Dashboard.ViewModels;
using maERP.UI.Features.GoodsReceipts.ViewModels;
using maERP.UI.Features.ImportExport.ViewModels;
using maERP.UI.Features.Invoices.ViewModels;
using maERP.UI.Features.Manufacturer.ViewModels;
using maERP.UI.Features.Orders.ViewModels;
using maERP.UI.Features.Products.ViewModels;
using maERP.UI.Features.SalesChannels.ViewModels;
using maERP.UI.Features.Warehouses.ViewModels;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;
using maERP.UI.Shared.Views;
using TenantListViewModel = maERP.UI.Features.Tenants.ViewModels.TenantListViewModel;
using TenantDetailViewModel = maERP.UI.Features.Tenants.ViewModels.TenantDetailViewModel;
using TenantInputViewModel = maERP.UI.Features.Tenants.ViewModels.TenantInputViewModel;

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

        services.AddHttpClient<HttpService>();

        services.AddSingleton<IHttpService, HttpService>();
        services.AddSingleton<ITenantService, TenantService>();
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<IDebugService, DebugService>();

        services.AddSingleton<MainViewModel>();
        services.AddTransient<LoginViewModel>();
        services.AddSingleton<TenantSelectorViewModel>();

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
        services.AddTransient<ImportExportOverviewViewModel>();
        services.AddTransient<InvoiceListViewModel>();
        services.AddTransient<OrderListViewModel>();
        services.AddTransient<OrderDetailViewModel>();
        services.AddTransient<OrderInputViewModel>();
        services.AddTransient<ProductListViewModel>();
        services.AddTransient<ProductDetailViewModel>();
        services.AddTransient<ProductInputViewModel>();
        services.AddTransient<TenantListViewModel>();
        services.AddTransient<TenantDetailViewModel>();
        services.AddTransient<TenantInputViewModel>();
        services.AddTransient<SalesChannelListViewModel>();
        services.AddTransient<SalesChannelDetailViewModel>();
        services.AddTransient<SalesChannelInputViewModel>();
        services.AddTransient<TaxClassListViewModel>();
        services.AddTransient<TaxClassInputViewModel>();
        services.AddTransient<TaxClassDetailViewModel>();
        services.AddTransient<UserListViewModel>();
        services.AddTransient<UserDetailViewModel>();
        services.AddTransient<UserInputViewModel>();
        services.AddTransient<WarehouseListViewModel>();
        services.AddTransient<WarehouseDetailViewModel>();
        services.AddTransient<WarehouseInputViewModel>();
        services.AddTransient<WarehouseSelectionDialogViewModel>();
        services.AddTransient<GoodsReceiptListViewModel>();
        services.AddTransient<GoodsReceiptInputViewModel>();
        services.AddTransient<ManufacturerListViewModel>();
        services.AddTransient<ManufacturerDetailViewModel>();
        services.AddTransient<ManufacturerInputViewModel>();
        services.AddTransient<ConfirmationDialogViewModel>();
        services.AddTransient<DebugWindowViewModel>();

        _serviceProvider = services.BuildServiceProvider();
    }

    public override void OnFrameworkInitializationCompleted()
    {
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
