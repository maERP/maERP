namespace maERP.Client;

public partial class App
{
    /// <summary>
    /// Initializes the singleton application object. This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    private Window? MainWindow { get; set; }

    // ReSharper disable once AsyncVoidMethod
    // ReSharper disable once ArrangeModifiersOrder
    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var builder = this.CreateBuilder(args)
            // Add navigation support for toolkit controls such as TabBar and NavigationView
            .UseToolkitNavigation()
            .Configure(host => host
#if DEBUG
                // Switch to the Development environment when running in DEBUG
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
                        .Section<AppConfig>()
                        .Section<ApiClientOptions>()
                )
                // Enable localization (see appsettings.json for supported languages)
                .UseLocalization()
                .UseHttp((context, services) =>
                {
                    // Configure ApiClientOptions from configuration
                    var apiClientOptions = context.Configuration.GetSection(nameof(ApiClientOptions)).Get<ApiClientOptions>()
                                           ?? new ApiClientOptions();

                    // Configure named HttpClient for API with handler pipeline
                    // Note: BaseUrl is now set dynamically via DynamicBaseUrlHandler
                    // Handlers are added in outer-to-inner order (execution order)
                    // Execution order: DynamicBaseUrl (outermost) -> Auth -> Tenant -> Error -> Debug (innermost)
                    services.AddHttpClient("maERPApi", client =>
                        {
                            // Set a temporary BaseUrl as a placeholder
                            // This will be overridden by DynamicBaseUrlHandler with the actual server URL
                            // We need to set something here because HttpClient requires a BaseAddress for relative URIs
                            client.BaseAddress = !string.IsNullOrEmpty(apiClientOptions.BaseUrl)
                                ? new Uri(apiClientOptions.BaseUrl)
                                : new Uri("http://localhost:5000"); // Fallback placeholder
                            client.Timeout = TimeSpan.FromSeconds(apiClientOptions.TimeoutSeconds);
                        })
                        // DynamicBaseUrlHandler must be first (outermost) to set the correct URL before other handlers run
                        .AddHttpMessageHandler(sp => new DynamicBaseUrlHandler(
                            sp.GetRequiredService<IAuthenticationStateService>(),
                            sp.GetRequiredService<ILogger<DynamicBaseUrlHandler>>()))
                        .AddHttpMessageHandler(sp => new AuthenticationHandler(
                            sp.GetRequiredService<IAuthenticationStateService>(),
                            sp.GetRequiredService<ILogger<AuthenticationHandler>>()))
                        .AddHttpMessageHandler(sp => new TenantHandler(
                            sp.GetRequiredService<ITenantService>(),
                            sp.GetRequiredService<ILogger<TenantHandler>>()))
                        .AddHttpMessageHandler(sp => new ErrorHandler(
                            sp.GetRequiredService<ILogger<ErrorHandler>>()))
#if DEBUG
                        .AddHttpMessageHandler(sp => new DebugHttpHandler(
                            sp.GetRequiredService<ILogger<DebugHttpHandler>>()))
#endif
                        ;

                    // Register all API clients as typed clients using the named HttpClient
                    services.AddTransient<IAuthApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<AuthApiClient>>();
                        return new AuthApiClient(httpClient, logger);
                    });

                    services.AddTransient<ICustomersApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<CustomersApiClient>>();
                        return new CustomersApiClient(httpClient, logger);
                    });

                    services.AddTransient<IProductsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<ProductsApiClient>>();
                        return new ProductsApiClient(httpClient, logger);
                    });

                    services.AddTransient<ITenantsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<TenantsApiClient>>();
                        return new TenantsApiClient(httpClient, logger);
                    });

                    services.AddTransient<IOrdersApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<OrdersApiClient>>();
                        return new OrdersApiClient(httpClient, logger);
                    });

                    services.AddTransient<IUsersApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<UsersApiClient>>();
                        return new UsersApiClient(httpClient, logger);
                    });

                    services.AddTransient<IInvoicesApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<InvoicesApiClient>>();
                        return new InvoicesApiClient(httpClient, logger);
                    });

                    services.AddTransient<IManufacturersApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<ManufacturersApiClient>>();
                        return new ManufacturersApiClient(httpClient, logger);
                    });

                    services.AddTransient<IGoodsReceiptsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<GoodsReceiptsApiClient>>();
                        return new GoodsReceiptsApiClient(httpClient, logger);
                    });

                    services.AddTransient<IWarehousesApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<WarehousesApiClient>>();
                        return new WarehousesApiClient(httpClient, logger);
                    });

                    services.AddTransient<ITaxClassesApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<TaxClassesApiClient>>();
                        return new TaxClassesApiClient(httpClient, logger);
                    });

                    services.AddTransient<ISalesChannelsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<SalesChannelsApiClient>>();
                        return new SalesChannelsApiClient(httpClient, logger);
                    });

                    services.AddTransient<ISettingsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<SettingsApiClient>>();
                        return new SettingsApiClient(httpClient, logger);
                    });

                    services.AddTransient<IStatisticsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<StatisticsApiClient>>();
                        return new StatisticsApiClient(httpClient, logger);
                    });

                    services.AddTransient<IAIModelsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<AIModelsApiClient>>();
                        return new AIModelsApiClient(httpClient, logger);
                    });

                    services.AddTransient<IAIPromptsApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<AIPromptsApiClient>>();
                        return new AIPromptsApiClient(httpClient, logger);
                    });

                    services.AddTransient<ISuperadminApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<SuperadminApiClient>>();
                        return new SuperadminApiClient(httpClient, logger);
                    });

                    services.AddTransient<IDemoDataApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<DemoDataApiClient>>();
                        return new DemoDataApiClient(httpClient, logger);
                    });

                    services.AddTransient<IImportExportApiClient>(sp =>
                    {
                        var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("maERPApi");
                        var logger = sp.GetRequiredService<ILogger<ImportExportApiClient>>();
                        return new ImportExportApiClient(httpClient, logger);
                    });
                })
                .UseAuthentication(auth =>
                    auth.AddWeb(name: "WebAuthentication")
                )
                .ConfigureServices((_, services) =>
                {
                    // Register authentication and tenant services
                    services.AddSingleton<IAuthenticationStateService, AuthenticationStateService>();
                    services.AddSingleton<ITenantService, TenantService>();
                })
                .UseNavigation(ReactiveViewModelMappings.ViewModelMappings, RegisterRoutes)
            );
        MainWindow = builder.Window;

#if DEBUG
        MainWindow.UseStudio();
#endif
        // MainWindow.SetWindowIcon();

        await builder.NavigateAsync<Shell.Shell>
        (initialNavigate: async (services, navigator) =>
        {
            // Check if a user has an active session
            var authStateService = services.GetService<IAuthenticationStateService>();
            var isAuthenticated = authStateService != null && await authStateService.IsAuthenticatedAsync();

            if (isAuthenticated)
            {
                // User is authenticated, navigate to Dashboard
                await navigator.NavigateViewModelAsync<DashboardModel>(this, qualifier: Qualifiers.Nested);
            }
            else
            {
                // User is not authenticated, navigate to LoginPage
                await navigator.NavigateViewModelAsync<LoginModel>(this, qualifier: Qualifiers.Nested);
            }
        });
    }

    private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
    {
        views.Register(
            new ViewMap(ViewModel: typeof(Shell.ShellModel)),
            new ViewMap<LoginPage, LoginModel>(),
            new ViewMap<DashboardPage, DashboardModel>(),
            new DataViewMap<SecondPage, SecondModel, Entity>(),
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
                    new("Login", View: views.FindByViewModel<LoginModel>()),
                    new("Main", View: views.FindByViewModel<DashboardModel>(), IsDefault: true),
                    new("Second", View: views.FindByViewModel<SecondModel>()),
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
