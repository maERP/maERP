using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using maERP.UI.Shared.Features.AI.ViewModels;
using maERP.UI.Shared.Features.Administration.ViewModels;
using maERP.UI.Shared.Features.Authentication.ViewModels;
using maERP.UI.Shared.Features.Customers.ViewModels;
using maERP.UI.Shared.Features.Dashboard.ViewModels;
using maERP.UI.Shared.Features.GoodsReceipts.ViewModels;
using maERP.UI.Shared.Features.ImportExport.ViewModels;
using maERP.UI.Shared.Features.Invoices.ViewModels;
using maERP.UI.Shared.Features.Manufacturer.ViewModels;
using maERP.UI.Shared.Features.Orders.ViewModels;
using maERP.UI.Shared.Features.Products.ViewModels;
using maERP.UI.Shared.Features.SalesChannels.ViewModels;
using maERP.UI.Shared.Features.Tenant.ViewModels;
using maERP.UI.Shared.Features.Warehouses.ViewModels;
using maERP.UI.Shared.Services;
using maERP.UI.Shared.Shared.ViewModels;
using maERP.UI.Shared.Shared.Views;
using SuperadminTenantsListViewModel = maERP.UI.Shared.Features.Superadmin.ViewModels.SuperadminTenantsListViewModel;
using SuperadminTenantsDetailViewModel = maERP.UI.Shared.Features.Superadmin.ViewModels.SuperadminTenantsDetailViewModel;
using SuperadminTenantsInputViewModel = maERP.UI.Shared.Features.Superadmin.ViewModels.SuperadminTenantsInputViewModel;
using SuperadminUserListViewModel = maERP.UI.Shared.Features.Superadmin.ViewModels.SuperadminUserListViewModel;
using SuperadminUserDetailViewModel = maERP.UI.Shared.Features.Superadmin.ViewModels.SuperadminUserDetailViewModel;
using SuperadminUserInputViewModel = maERP.UI.Shared.Features.Superadmin.ViewModels.SuperadminUserInputViewModel;

namespace maERP.UI;

public partial class App : Application
{
    private ServiceProvider? _serviceProvider;
    private Window? _window;

    public static ServiceProvider? Services { get; private set; }

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        ConfigureServices();

#if HAS_UNO || NETFX_CORE
        _window = Microsoft.UI.Xaml.Window.Current;
#else
        _window = new Window();
#endif

        var mainViewModel = _serviceProvider?.GetRequiredService<MainViewModel>();

        // Create the main view
#if HAS_UNO_SKIA || HAS_UNO_WASM
        // For Desktop/Browser: Use MainView directly
        _window.Content = new MainView
        {
            DataContext = mainViewModel
        };
#else
        // For Windows/Mobile: Use MainWindow
        _window.Content = new MainView
        {
            DataContext = mainViewModel
        };
#endif

        _window.Activate();
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
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<RegistrationViewModel>();
        services.AddSingleton<ForgotPasswordViewModel>();
        services.AddSingleton<ResetPasswordViewModel>();
        services.AddSingleton<TenantSetupViewModel>();
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
        services.AddTransient<SuperadminTenantsListViewModel>();
        services.AddTransient<SuperadminTenantsDetailViewModel>();
        services.AddTransient<SuperadminTenantsInputViewModel>();
        services.AddTransient<SalesChannelListViewModel>();
        services.AddTransient<SalesChannelDetailViewModel>();
        services.AddTransient<SalesChannelInputViewModel>();
        services.AddTransient<TaxClassListViewModel>();
        services.AddTransient<TaxClassInputViewModel>();
        services.AddTransient<TaxClassDetailViewModel>();
        services.AddTransient<SuperadminUserListViewModel>();
        services.AddTransient<SuperadminUserDetailViewModel>();
        services.AddTransient<SuperadminUserInputViewModel>();
        services.AddTransient<WarehouseListViewModel>();
        services.AddTransient<WarehouseDetailViewModel>();
        services.AddTransient<WarehouseInputViewModel>();
        services.AddTransient<WarehouseSelectionDialogViewModel>();
        services.AddTransient<GoodsReceiptListViewModel>();
        services.AddTransient<GoodsReceiptInputViewModel>();
        services.AddTransient<ManufacturerListViewModel>();
        services.AddTransient<ManufacturerDetailViewModel>();
        services.AddTransient<ManufacturerInputViewModel>();
        services.AddTransient<TenantListViewModel>();
        services.AddTransient<maERP.UI.Shared.Features.Tenant.ViewModels.TenantDetailViewModel>();
        services.AddTransient<TenantInputViewModel>();
        services.AddTransient<ConfirmationDialogViewModel>();
        services.AddTransient<DebugWindowViewModel>();

        _serviceProvider = services.BuildServiceProvider();
        Services = _serviceProvider;
    }
}
