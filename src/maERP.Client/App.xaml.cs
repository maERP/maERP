using Uno.Resizetizer;

namespace maERP.Client;

public partial class App : Microsoft.UI.Xaml.Application
{
    /// <summary>
    /// Initializes the singleton application object. This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    protected Window? MainWindow { get; private set; }
    protected IHost? Host { get; private set; }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var builder = this.CreateBuilder(args)
            // Add navigation support for toolkit controls such as TabBar and NavigationView
            .UseToolkitNavigation()
            .Configure(host => host
#if DEBUG
                // Switch to Development environment when running in DEBUG
                .UseEnvironment(Environments.Development)
#endif
                .UseLogging(configure: (context, logBuilder) =>
                {
                    // Configure log levels for different categories of logging
                    logBuilder
                        .SetMinimumLevel(
                            context.HostingEnvironment.IsDevelopment() ? LogLevel.Information : LogLevel.Warning)

                        // Default filters for core Uno Platform namespaces
                        .CoreLogLevel(LogLevel.Warning);

                    // Uno Platform namespace filter groups
                    // Uncomment individual methods to see more detailed logging
                    //// Generic Xaml events
                    //logBuilder.XamlLogLevel(LogLevel.Debug);
                    //// Layout specific messages
                    //logBuilder.XamlLayoutLogLevel(LogLevel.Debug);
                    //// Storage messages
                    //logBuilder.StorageLogLevel(LogLevel.Debug);
                    //// Binding related messages
                    //logBuilder.XamlBindingLogLevel(LogLevel.Debug);
                    //// Binder memory references tracking
                    //logBuilder.BinderMemoryReferenceLogLevel(LogLevel.Debug);
                    //// DevServer and HotReload related
                    //logBuilder.HotReloadCoreLogLevel(LogLevel.Information);
                    //// Debug JS interop
                    //logBuilder.WebAssemblyLogLevel(LogLevel.Debug);
                }, enableUnoLogging: true)
                .UseSerilog(consoleLoggingEnabled: true, fileLoggingEnabled: true)
                .UseConfiguration(configure: configBuilder =>
                    configBuilder
                        .EmbeddedSource<App>()
                        .Section<Core.Models.AppConfig>()
                        .Section<Core.Models.ApiClientOptions>()
                )
                // Enable localization (see appsettings.json for supported languages)
                .UseLocalization()
                .UseHttp((context, services) =>
                {
                    // Configure ApiClientOptions from configuration
                    var apiClientOptions = context.Configuration.GetSection(nameof(ApiClientOptions)).Get<Core.Models.ApiClientOptions>()
                        ?? new Core.Models.ApiClientOptions();

                    // Configure named HttpClient for API with handler pipeline
                    // Note: BaseUrl is now set dynamically via DynamicBaseUrlHandler
                    // Handlers are added in outer-to-inner order (execution order)
                    // Execution order: DynamicBaseUrl (outermost) -> Auth -> Tenant -> Error -> Debug (innermost)
                    services.AddHttpClient("maERPApi", client =>
                    {
                        // Set a temporary BaseUrl as placeholder
                        // This will be overridden by DynamicBaseUrlHandler with the actual server URL
                        // We need to set something here because HttpClient requires a BaseAddress for relative URIs
                        client.BaseAddress = !string.IsNullOrEmpty(apiClientOptions.BaseUrl)
                            ? new Uri(apiClientOptions.BaseUrl)
                            : new Uri("http://localhost:5000"); // Fallback placeholder
                        client.Timeout = TimeSpan.FromSeconds(apiClientOptions.TimeoutSeconds);
                    })
                    // DynamicBaseUrlHandler must be first (outermost) to set the correct URL before other handlers run
                    .AddHttpMessageHandler(sp => new Services.Api.Handlers.DynamicBaseUrlHandler(
                        sp.GetRequiredService<Services.Authentication.IAuthenticationStateService>(),
                        sp.GetRequiredService<ILogger<Services.Api.Handlers.DynamicBaseUrlHandler>>()))
                    .AddHttpMessageHandler(sp => new Services.Api.Handlers.AuthenticationHandler(
                        sp.GetRequiredService<Services.Authentication.IAuthenticationStateService>(),
                        sp.GetRequiredService<ILogger<Services.Api.Handlers.AuthenticationHandler>>()))
                    .AddHttpMessageHandler(sp => new Services.Api.Handlers.TenantHandler(
                        sp.GetRequiredService<Services.Tenant.ITenantService>(),
                        sp.GetRequiredService<ILogger<Services.Api.Handlers.TenantHandler>>()))
                    .AddHttpMessageHandler(sp => new Services.Api.Handlers.ErrorHandler(
                        sp.GetRequiredService<ILogger<Services.Api.Handlers.ErrorHandler>>()))
#if DEBUG
                    .AddHttpMessageHandler(sp => new Services.Api.Handlers.DebugHttpHandler(
                        sp.GetRequiredService<ILogger<Services.Api.Handlers.DebugHttpHandler>>()))
#endif
                    ;

                    // Register all API clients as typed clients using the named HttpClient
                    services.AddTransient<Services.Api.Clients.IAuthApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.AuthApiClient>>();
                        return new Services.Api.Clients.AuthApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.ICustomersApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.CustomersApiClient>>();
                        return new Services.Api.Clients.CustomersApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.IProductsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.ProductsApiClient>>();
                        return new Services.Api.Clients.ProductsApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.ITenantsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.TenantsApiClient>>();
                        return new Services.Api.Clients.TenantsApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.IOrdersApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.OrdersApiClient>>();
                        return new Services.Api.Clients.OrdersApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.IUsersApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.UsersApiClient>>();
                        return new Services.Api.Clients.UsersApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.IInvoicesApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.InvoicesApiClient>>();
                        return new Services.Api.Clients.InvoicesApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.IManufacturersApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.ManufacturersApiClient>>();
                        return new Services.Api.Clients.ManufacturersApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.IGoodsReceiptsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.GoodsReceiptsApiClient>>();
                        return new Services.Api.Clients.GoodsReceiptsApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.IWarehousesApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.WarehousesApiClient>>();
                        return new Services.Api.Clients.WarehousesApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.ITaxClassesApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.TaxClassesApiClient>>();
                        return new Services.Api.Clients.TaxClassesApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.ISalesChannelsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.SalesChannelsApiClient>>();
                        return new Services.Api.Clients.SalesChannelsApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.ISettingsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.SettingsApiClient>>();
                        return new Services.Api.Clients.SettingsApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.IStatisticsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.StatisticsApiClient>>();
                        return new Services.Api.Clients.StatisticsApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.IAIModelsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.AIModelsApiClient>>();
                        return new Services.Api.Clients.AIModelsApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.IAIPromptsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.AIPromptsApiClient>>();
                        return new Services.Api.Clients.AIPromptsApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.ISuperadminApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.SuperadminApiClient>>();
                        return new Services.Api.Clients.SuperadminApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.IDemoDataApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.DemoDataApiClient>>();
                        return new Services.Api.Clients.DemoDataApiClient(httpClient, logger);
                    });

                    services.AddTransient<Services.Api.Clients.IImportExportApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<Services.Api.Clients.ImportExportApiClient>>();
                        return new Services.Api.Clients.ImportExportApiClient(httpClient, logger);
                    });
                })
                .UseAuthentication(auth =>
                    auth.AddWeb(name: "WebAuthentication")
                )
                .ConfigureServices((context, services) =>
                {
                    // Register authentication and tenant services
                    services.AddSingleton<Services.Authentication.IAuthenticationStateService, Services.Authentication.AuthenticationStateService>();
                    services.AddSingleton<Services.Tenant.ITenantService, Services.Tenant.TenantService>();
                })
                .UseNavigation(ReactiveViewModelMappings.ViewModelMappings, RegisterRoutes)
            );
        MainWindow = builder.Window;

#if DEBUG
        MainWindow.UseStudio();
#endif
        MainWindow.SetWindowIcon();

        Host = await builder.NavigateAsync<Shell.Shell>
        (initialNavigate: async (services, navigator) =>
        {
            // Check if user has an active session
            var authStateService = services.GetService<Services.Authentication.IAuthenticationStateService>();
            var isAuthenticated = authStateService != null && await authStateService.IsAuthenticatedAsync();

            if (isAuthenticated)
            {
                // User is authenticated, navigate to Dashboard
                await navigator.NavigateViewModelAsync<Features.Dashboard.Models.DashboardModel>(this, qualifier: Qualifiers.Nested);
            }
            else
            {
                // User is not authenticated, navigate to LoginPage
                await navigator.NavigateViewModelAsync<Features.Authentication.Models.LoginModel>(this, qualifier: Qualifiers.Nested);
            }
        });
    }

    private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
    {
        views.Register(
            new ViewMap(ViewModel: typeof(Shell.ShellModel)),
            new ViewMap<Features.Authentication.Views.LoginPage, Features.Authentication.Models.LoginModel>(),
            new ViewMap<Features.Dashboard.Views.DashboardPage, Features.Dashboard.Models.DashboardModel>(),
            new DataViewMap<Features.Dashboard.Views.SecondPage, Features.Dashboard.Models.SecondModel, Core.Models.Entity>(),
            new ViewMap<Features.Customers.Views.CustomerListPage, Features.Customers.Models.CustomerListModel>(),
            new ViewMap<Features.Products.Views.ProductListPage, Features.Products.Models.ProductListModel>(),
            new ViewMap<Features.Orders.Views.OrderListPage, Features.Orders.Models.OrderListModel>(),
            new ViewMap<Features.Invoices.Views.InvoiceListPage, Features.Invoices.Models.InvoiceListModel>(),
            new ViewMap<Features.Warehouses.Views.WarehouseListPage, Features.Warehouses.Models.WarehouseListModel>(),
            new ViewMap<Features.Manufacturers.Views.ManufacturerListPage, Features.Manufacturers.Models.ManufacturerListModel>(),
            new ViewMap<Features.SalesChannels.Views.SalesChannelListPage, Features.SalesChannels.Models.SalesChannelListModel>(),
            new ViewMap<Features.Statistics.Views.StatisticsPage, Features.Statistics.Models.StatisticsModel>()
        );

        routes.Register(
            new RouteMap("", View: views.FindByViewModel<Shell.ShellModel>(),
                Nested:
                [
                    new("Login", View: views.FindByViewModel<Features.Authentication.Models.LoginModel>()),
                    new("Main", View: views.FindByViewModel<Features.Dashboard.Models.DashboardModel>(), IsDefault: true),
                    new("Second", View: views.FindByViewModel<Features.Dashboard.Models.SecondModel>()),
                    new("Customers", View: views.FindByViewModel<Features.Customers.Models.CustomerListModel>()),
                    new("Products", View: views.FindByViewModel<Features.Products.Models.ProductListModel>()),
                    new("Orders", View: views.FindByViewModel<Features.Orders.Models.OrderListModel>()),
                    new("Invoices", View: views.FindByViewModel<Features.Invoices.Models.InvoiceListModel>()),
                    new("Warehouses", View: views.FindByViewModel<Features.Warehouses.Models.WarehouseListModel>()),
                    new("Manufacturers", View: views.FindByViewModel<Features.Manufacturers.Models.ManufacturerListModel>()),
                    new("SalesChannels", View: views.FindByViewModel<Features.SalesChannels.Models.SalesChannelListModel>()),
                    new("Statistics", View: views.FindByViewModel<Features.Statistics.Models.StatisticsModel>()),
                ]
            )
        );
    }
}
