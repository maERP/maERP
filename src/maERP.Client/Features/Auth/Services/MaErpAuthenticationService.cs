using System.Net.Http.Json;
using System.Text.Json;
using maERP.Client.Core.Exceptions;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Json;
using maERP.Client.Core.Models;
using maERP.Domain.Dtos.Auth;

namespace maERP.Client.Features.Auth.Services;

public class MaErpAuthenticationService : IMaErpAuthenticationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ITenantContextService _tenantContext;
    private readonly ILogger<MaErpAuthenticationService> _logger;

    public MaErpAuthenticationService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ITenantContextService tenantContext,
        ILogger<MaErpAuthenticationService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _tokenStorage = tokenStorage;
        _tenantContext = tenantContext;
        _logger = logger;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Attempting login for user: {Email} to server: {Server}",
            request.Email, request.Server);

        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(request.Server);

        var response = await httpClient.PostAsJsonAsync("/api/v1/auth/login", request, AppJsonSerializerContext.Default.LoginRequestDto, cancellationToken);

        // Use ApiException for HTTP errors to propagate server error messages
        await response.EnsureSuccessOrThrowApiExceptionAsync(cancellationToken);

        var rawJson = await response.Content.ReadAsStringAsync(cancellationToken);
        _logger.LogDebug("Login response JSON: {Json}", rawJson);

        // Parse the wrapper response first
        var apiResponse = JsonSerializer.Deserialize(rawJson, AppJsonSerializerContext.Default.ApiResponseLoginResponseDto);
        var loginResponse = apiResponse?.Data;

        _logger.LogInformation("Parsed response - ApiSucceeded: {ApiSucceeded}, DataSucceeded: {DataSucceeded}, Token: {HasToken}, UserId: {UserId}",
            apiResponse?.Succeeded, loginResponse?.Succeeded, !string.IsNullOrEmpty(loginResponse?.Token), loginResponse?.UserId);

        if (apiResponse?.Succeeded == true && loginResponse?.Succeeded == true && !string.IsNullOrEmpty(loginResponse.Token))
        {
            await _tokenStorage.SetTokenAsync(loginResponse.Token);
            await _tokenStorage.SetServerUrlAsync(request.Server);

            if (!string.IsNullOrEmpty(loginResponse.RefreshToken))
            {
                await _tokenStorage.SetRefreshTokenAsync(loginResponse.RefreshToken, loginResponse.RefreshTokenExpiresAt);
            }

            await _tokenStorage.SetRememberMeAsync(request.RememberMe);

            // Store available tenants in context (handles current tenant selection)
            if (loginResponse.AvailableTenants != null)
            {
                // If a specific current tenant ID is provided, set it first
                if (loginResponse.CurrentTenantId.HasValue)
                {
                    await _tokenStorage.SetCurrentTenantIdAsync(loginResponse.CurrentTenantId.Value);
                    _logger.LogInformation("Using current tenant ID from login response: {TenantId}", loginResponse.CurrentTenantId.Value);
                }

                // Store available tenants (will restore/select current tenant automatically)
                await _tenantContext.SetAvailableTenantsAsync(loginResponse.AvailableTenants);
            }
            else
            {
                _logger.LogWarning("No tenant available for user after login");
            }

            _logger.LogInformation("Login successful for user: {UserId}", loginResponse.UserId);
        }

        return loginResponse;
    }

    public async Task<LoginResponseDto?> RegisterAsync(string serverUrl, RegisterRequestDto request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Attempting registration for user: {Email} to server: {Server}", request.Email, serverUrl);

        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(serverUrl);

        var response = await httpClient.PostAsJsonAsync("/api/v1/auth/register", request, AppJsonSerializerContext.Default.RegisterRequestDto, cancellationToken);

        // Surface server-side problem details (e.g. 403 when registration disabled, 400 on weak password)
        await response.EnsureSuccessOrThrowApiExceptionAsync(cancellationToken);

        var rawJson = await response.Content.ReadAsStringAsync(cancellationToken);
        _logger.LogDebug("Register response JSON: {Json}", rawJson);

        var apiResponse = JsonSerializer.Deserialize(rawJson, AppJsonSerializerContext.Default.ApiResponseLoginResponseDto);
        var registerResponse = apiResponse?.Data;

        if (apiResponse?.Succeeded == true && registerResponse?.Succeeded == true && !string.IsNullOrEmpty(registerResponse.Token))
        {
            await _tokenStorage.SetTokenAsync(registerResponse.Token);
            await _tokenStorage.SetServerUrlAsync(serverUrl);

            if (!string.IsNullOrEmpty(registerResponse.RefreshToken))
            {
                await _tokenStorage.SetRefreshTokenAsync(registerResponse.RefreshToken, registerResponse.RefreshTokenExpiresAt);
            }

            if (registerResponse.AvailableTenants != null)
            {
                if (registerResponse.CurrentTenantId.HasValue)
                {
                    await _tokenStorage.SetCurrentTenantIdAsync(registerResponse.CurrentTenantId.Value);
                }

                await _tenantContext.SetAvailableTenantsAsync(registerResponse.AvailableTenants);
            }

            _logger.LogInformation("Registration + auto-login successful for user: {UserId}", registerResponse.UserId);
        }

        return registerResponse;
    }

    public async Task<LoginResponseDto?> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Refreshing JWT token via refresh-token");

        var serverUrl = await _tokenStorage.GetServerUrlAsync();
        var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

        if (string.IsNullOrEmpty(serverUrl) || string.IsNullOrEmpty(refreshToken))
        {
            _logger.LogWarning("Token refresh failed: No server URL or refresh token configured");
            return null;
        }

        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(serverUrl);

        // Anonymous endpoint: send only the refresh token. Crucially, we do NOT attach the
        // (likely-expired) Bearer token here — that's the bug the new flow fixes.
        var requestBody = new RefreshTokenRequestDto { RefreshToken = refreshToken };
        var response = await httpClient.PostAsJsonAsync(
            "/api/v1/auth/refresh-token",
            requestBody,
            AppJsonSerializerContext.Default.RefreshTokenRequestDto,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            // Refresh failed → the stored refresh token is dead. Clean up so the user gets a
            // fresh login prompt instead of being stuck in a 401-spam loop.
            _logger.LogWarning("Refresh token rejected by server with {StatusCode} — clearing stored credentials",
                response.StatusCode);
            await _tokenStorage.ClearRefreshTokenAsync();
            await _tokenStorage.ClearTokenAsync();
            return null;
        }

        var rawJson = await response.Content.ReadAsStringAsync(cancellationToken);
        _logger.LogDebug("Refresh token response JSON: {Json}", rawJson);

        var apiResponse = JsonSerializer.Deserialize(rawJson, AppJsonSerializerContext.Default.ApiResponseLoginResponseDto);
        var refreshResponse = apiResponse?.Data;

        if (apiResponse?.Succeeded == true && refreshResponse?.Succeeded == true && !string.IsNullOrEmpty(refreshResponse.Token))
        {
            await _tokenStorage.SetTokenAsync(refreshResponse.Token);

            if (!string.IsNullOrEmpty(refreshResponse.RefreshToken))
            {
                await _tokenStorage.SetRefreshTokenAsync(refreshResponse.RefreshToken, refreshResponse.RefreshTokenExpiresAt);
            }

            if (refreshResponse.AvailableTenants != null)
            {
                if (refreshResponse.CurrentTenantId.HasValue)
                {
                    await _tokenStorage.SetCurrentTenantIdAsync(refreshResponse.CurrentTenantId.Value);
                }

                await _tenantContext.SetAvailableTenantsAsync(refreshResponse.AvailableTenants);
            }

            _logger.LogInformation("Token refreshed successfully with {Count} tenants",
                refreshResponse.AvailableTenants?.Count ?? 0);
        }

        return refreshResponse;
    }

    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        var serverUrl = await _tokenStorage.GetServerUrlAsync();
        var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

        // Always clear local first — even if the server call fails, the user wants to be logged out locally.
        try
        {
            if (!string.IsNullOrEmpty(serverUrl) && !string.IsNullOrEmpty(refreshToken))
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.BaseAddress = new Uri(serverUrl);

                var body = new RefreshTokenRequestDto { RefreshToken = refreshToken };
                using var response = await httpClient.PostAsJsonAsync(
                    "/api/v1/auth/logout",
                    body,
                    AppJsonSerializerContext.Default.RefreshTokenRequestDto,
                    cancellationToken);
                // Don't throw — server-side revocation is best-effort.
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Server-side logout failed; clearing local credentials anyway");
        }

        await _tokenStorage.ClearRefreshTokenAsync();
        await _tokenStorage.ClearTokenAsync();
    }

    public async Task<bool> ValidateTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Validating authentication token");

        try
        {
            var serverUrl = await _tokenStorage.GetServerUrlAsync();
            if (string.IsNullOrEmpty(serverUrl))
            {
                _logger.LogWarning("Token validation failed: No server URL configured");
                return false;
            }

            _logger.LogDebug("Validating token against server: {ServerUrl}", serverUrl);

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(serverUrl);
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Try to call a protected endpoint to validate the token
            var response = await httpClient.GetAsync("/api/v1/auth/validate", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Token validation successful");
                return true;
            }

            _logger.LogWarning("Token validation failed with status code: {StatusCode}", response.StatusCode);
            return false;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Token validation failed: Server connection error");
            return false;
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogWarning(ex, "Token validation failed: Request timeout or cancellation");
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Token validation failed: Unexpected error");
            return false;
        }
    }
}
