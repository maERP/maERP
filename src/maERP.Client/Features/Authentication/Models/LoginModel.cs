namespace maERP.Client.Features.Authentication.Models;

public partial record LoginModel
{
    private readonly INavigator _navigator;
    private readonly Services.Api.Clients.IAuthApiClient _authApiClient;
    private readonly Services.Authentication.IAuthenticationStateService _authStateService;
    private readonly ILogger<LoginModel> _logger;

    public LoginModel(
        INavigator navigator,
        Services.Api.Clients.IAuthApiClient authApiClient,
        Services.Authentication.IAuthenticationStateService authStateService,
        ILogger<LoginModel> logger)
    {
        _navigator = navigator;
        _authApiClient = authApiClient;
        _authStateService = authStateService;
        _logger = logger;
    }

    public string Title { get; } = "Login";

    // MVUX State properties - MVUX generates the State wrappers automatically
    public IState<string> Email => State<string>.Value(this, () =>
    {
#if DEBUG
        if (System.Diagnostics.Debugger.IsAttached)
        {
            return "admin@localhost.com";
        }
#endif
        return string.Empty;
    });

    public IState<string> Password => State<string>.Value(this, () =>
    {
#if DEBUG
        if (System.Diagnostics.Debugger.IsAttached)
        {
            return "P@ssword1";
        }
#endif
        return string.Empty;
    });

    public IState<string> ServerUrl => State<string>.Async(this, async ct =>
    {
#if DEBUG
        if (System.Diagnostics.Debugger.IsAttached)
        {
            return "https://localhost:8443";
        }
#endif
        // Try to load saved server URL, default to localhost for development
        var savedUrl = await _authStateService.GetServerUrlAsync();
        return savedUrl ?? "https://localhost:8443";
    });

    public async ValueTask Login(CancellationToken cancellationToken)
    {
        try
        {
            var email = await Email;
            var password = await Password;
            var serverUrl = await ServerUrl;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(email))
            {
                _logger.LogWarning("Login failed: Email is required");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                _logger.LogWarning("Login failed: Password is required");
                return;
            }

            if (string.IsNullOrWhiteSpace(serverUrl))
            {
                _logger.LogWarning("Login failed: Server URL is required");
                return;
            }

            // Save server URL (will be used by DynamicBaseUrlHandler for subsequent requests)
            await _authStateService.SetServerUrlAsync(serverUrl);

            // Create login request DTO
            var loginRequest = new maERP.Domain.Dtos.Auth.LoginRequestDto
            {
                Email = email,
                Password = password,
                Server = serverUrl
            };

            // Perform login
            var response = await _authApiClient.LoginAsync(loginRequest, cancellationToken);

            if (response?.Token != null && response.Succeeded)
            {
                // Store authentication token
                await _authStateService.SetAccessTokenAsync(response.Token);

                _logger.LogInformation("Login successful for user {UserId}", response.UserId);

                // Navigate to main application
                await _navigator.NavigateViewModelAsync<Features.Dashboard.Models.DashboardModel>(
                    this,
                    qualifier: Qualifiers.ClearBackStack);
            }
            else
            {
                _logger.LogWarning("Login failed: {Message}", response?.Message ?? "Invalid credentials");
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error during login");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
        }
    }
}
