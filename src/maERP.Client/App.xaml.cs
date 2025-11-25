using Uno.Resizetizer;
using maERP.Client.Services.Authentication;
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
                                LogLevel.Information :
                                LogLevel.Warning)

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
                .ConfigureServices((context, services) =>
                {
                    // Register HttpClientFactory for services that need IHttpClientFactory
                    services.AddHttpClient();

                    // Register authentication services
                    services.AddSingleton<ITokenStorageService, TokenStorageService>();
                    services.AddSingleton<IMaErpAuthenticationService, MaErpAuthenticationService>();

                    // Register ShellModel as singleton to share authentication state
                    services.AddSingleton<ShellModel>();

                    // Register page models
                    services.AddTransient<LoginModel>();
                    services.AddTransient<MainModel>();
                    services.AddTransient<SecondModel>();
                })
                .UseNavigation(RegisterRoutes)
            );
        MainWindow = builder.Window;

#if DEBUG
        MainWindow.UseStudio();
#endif
        MainWindow.SetWindowIcon();

        Host = await builder.NavigateAsync<Shell>
            (initialNavigate: async (services, navigator) =>
            {
                var auth = services.GetRequiredService<IAuthenticationService>();
                var authenticated = await auth.RefreshAsync();
                if (authenticated)
                {
                    await navigator.NavigateViewModelAsync<MainModel>(this, qualifier: Qualifiers.Nested);
                }
                else
                {
                    await navigator.NavigateViewModelAsync<LoginModel>(this, qualifier: Qualifiers.Nested);
                }
            });
    }

    private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
    {
        views.Register(
            new ViewMap(ViewModel: typeof(ShellModel)),
            new ViewMap<LoginPage, LoginModel>(),
            new ViewMap<MainPage, MainModel>(),
            new DataViewMap<SecondPage, SecondModel, Entity>()
        );

        routes.Register(
            new RouteMap("", View: views.FindByViewModel<ShellModel>(),
                Nested:
                [
                    new ("Login", View: views.FindByViewModel<LoginModel>()),
                    new ("Main", View: views.FindByViewModel<MainModel>(), IsDefault:true),
                    new ("Second", View: views.FindByViewModel<SecondModel>()),
                ]
            )
        );
    }
}
