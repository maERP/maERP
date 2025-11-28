using Uno.Resizetizer;
using maERP.Client.Core.Constants;
using maERP.Client.Features.Auth;
using maERP.Client.Features.Auth.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Client.Features.Customers;
using maERP.Client.Features.Dashboard;
using maERP.Client.Features.Dashboard.Models;
using maERP.Client.Features.Legacy;
using maERP.Client.Features.Shell;
using maERP.Client.Features.Shell.Models;
using maERP.Client.Features.Shell.Views;
using maERP.Client.Services.Endpoints;
using maERP.Domain.Dtos.Auth;

namespace maERP.Client;

public partial class App : Application
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
    public IHost? Host { get; private set; }

    public new static App Current => (App)Application.Current;
    public IServiceProvider Services => Host!.Services;

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
                            context.HostingEnvironment.IsDevelopment() ?
                                LogLevel.Debug :
                                LogLevel.Information)

                        // Default filters for core Uno Platform namespaces
                        .CoreLogLevel(LogLevel.Warning);

                    // Configure maERP-specific log levels
                    logBuilder.AddFilter("maERP.Client",
                        context.HostingEnvironment.IsDevelopment() ? LogLevel.Debug : LogLevel.Information);
                    logBuilder.AddFilter("maERP.Client.Features.Auth", LogLevel.Debug);
                    logBuilder.AddFilter("maERP.Client.Services", LogLevel.Debug);

                    // HTTP logging - useful for debugging API calls
                    logBuilder.AddFilter("System.Net.Http",
                        context.HostingEnvironment.IsDevelopment() ? LogLevel.Information : LogLevel.Warning);

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
                )
                // Enable localization (see appsettings.json for supported languages)
                .UseLocalization()
                .UseHttp((context, services) =>
                {
#if DEBUG
                // DelegatingHandler will be automatically injected
                services.AddTransient<DelegatingHandler, DebugHttpHandler>();
#endif
                    // Add authentication handler for all HTTP requests
                    services.AddTransient<AuthenticationHandler>();
                })
                .UseAuthentication(auth =>
                    auth.AddCustom<IMaErpAuthenticationService>(custom =>
                        custom
                            .Login(async (authService, dispatcher, tokenCache, credentials, cancellationToken) =>
                            {
                                if (!credentials.TryGetValue("Email", out var email) ||
                                    !credentials.TryGetValue("Password", out var password) ||
                                    !credentials.TryGetValue("ServerUrl", out var serverUrl))
                                {
                                    return default;
                                }

                                var loginRequest = new LoginRequestDto
                                {
                                    Email = email,
                                    Password = password,
                                    Server = serverUrl
                                };

                                var response = await authService.LoginAsync(loginRequest, cancellationToken);

                                if (response?.Succeeded == true && !string.IsNullOrEmpty(response.Token))
                                {
                                    var tokens = new Dictionary<string, string>
                                    {
                                        ["AccessToken"] = response.Token,
                                        ["UserId"] = response.UserId,
                                        ["ServerUrl"] = serverUrl
                                    };

                                    if (response.CurrentTenantId.HasValue)
                                    {
                                        tokens["TenantId"] = response.CurrentTenantId.Value.ToString();
                                    }

                                    return tokens;
                                }

                                return default;
                            })
                            .Refresh(async (authService, tokenCache, cancellationToken) =>
                            {
                                var tokenStorage = tokenCache.TryGetValue("AccessToken", out var token) ? token : null;

                                if (!string.IsNullOrEmpty(tokenStorage) &&
                                    await authService.ValidateTokenAsync(tokenStorage, cancellationToken))
                                {
                                    return tokenCache;
                                }

                                return default;
                            })
                    )
                )
                .ConfigureServices(RegisterAllServices)
                .UseNavigation(RegisterRoutes)
            );
        MainWindow = builder.Window;

#if DEBUG
        MainWindow.UseStudio();
#endif
        MainWindow.SetWindowIcon();

        // Set initial window size for desktop
        if (MainWindow.AppWindow is not null)
        {
            MainWindow.AppWindow.Resize(new Windows.Graphics.SizeInt32 { Width = 1600, Height = 900 });
        }

        Host = await builder.NavigateAsync<Shell>
            (initialNavigate: async (services, navigator) =>
            {
                var auth = services.GetRequiredService<IAuthenticationService>();
                var authenticated = await auth.RefreshAsync();
                if (authenticated)
                {
                    await navigator.NavigateViewModelAsync<DashboardModel>(this, qualifier: Qualifiers.Nested);
                }
                else
                {
                    await navigator.NavigateViewModelAsync<LoginModel>(this, qualifier: Qualifiers.Nested);
                }
            });
    }

    /// <summary>
    /// Registers all services from feature modules.
    /// </summary>
    private static void RegisterAllServices(HostBuilderContext context, IServiceCollection services)
    {
        // Register handlers for HTTP client pipeline
        services.AddTransient<ServerUrlHandler>();
        services.AddTransient<AuthenticationHandler>();
#if DEBUG
        services.AddTransient<DebugHttpHandler>();
#endif

        // Register Named HttpClient "MaErpApi" with handlers for API requests
        // Handler order: ServerUrlHandler (sets base URL) -> AuthenticationHandler (adds token/tenant) -> DebugHttpHandler (logging)
#if DEBUG
        services.AddHttpClient("MaErpApi")
            .AddHttpMessageHandler<ServerUrlHandler>()
            .AddHttpMessageHandler<AuthenticationHandler>()
            .AddHttpMessageHandler<DebugHttpHandler>();
#else
        services.AddHttpClient("MaErpApi")
            .AddHttpMessageHandler<ServerUrlHandler>()
            .AddHttpMessageHandler<AuthenticationHandler>();
#endif

        // Also register default HttpClient factory for other uses
        services.AddHttpClient();

        // Register feature modules
        ShellModule.RegisterServices(services);
        AuthModule.RegisterServices(services);
        DashboardModule.RegisterServices(services);
        CustomersModule.RegisterServices(services);
        LegacyModule.RegisterServices(services);

        // Future modules will be registered here:
        // OrdersModule.RegisterServices(services);
        // ProductsModule.RegisterServices(services);
        // etc.
    }

    /// <summary>
    /// Registers all views and routes from feature modules.
    /// </summary>
    private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
    {
        // Register views from all feature modules
        ShellModule.RegisterViews(views);
        AuthModule.RegisterViews(views);
        DashboardModule.RegisterViews(views);
        CustomersModule.RegisterViews(views);
        LegacyModule.RegisterViews(views);

        // Future modules will register views here:
        // OrdersModule.RegisterViews(views);
        // ProductsModule.RegisterViews(views);
        // etc.

        // Collect routes from all feature modules
        var nestedRoutes = new List<RouteMap>();
        nestedRoutes.AddRange(AuthModule.GetRoutes(views));
        nestedRoutes.AddRange(DashboardModule.GetRoutes(views));
        nestedRoutes.AddRange(CustomersModule.GetRoutes(views));
        nestedRoutes.AddRange(LegacyModule.GetRoutes(views));

        // Future modules will add routes here:
        // nestedRoutes.AddRange(OrdersModule.GetRoutes(views));
        // nestedRoutes.AddRange(ProductsModule.GetRoutes(views));
        // etc.

        // Register the root route with all nested routes
        routes.Register(ShellModule.GetRootRoute(views, nestedRoutes));
    }
}
