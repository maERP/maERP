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

    public async Task<LoginResponseDto?> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Refreshing JWT token");

        var serverUrl = await _tokenStorage.GetServerUrlAsync();
        var token = await _tokenStorage.GetTokenAsync();

        if (string.IsNullOrEmpty(serverUrl) || string.IsNullOrEmpty(token))
        {
            _logger.LogWarning("Token refresh failed: No server URL or token configured");
            return null;
        }

        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(serverUrl);
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.PostAsync("/api/v1/auth/refresh-token", null, cancellationToken);
        await response.EnsureSuccessOrThrowApiExceptionAsync(cancellationToken);

        var rawJson = await response.Content.ReadAsStringAsync(cancellationToken);
        _logger.LogDebug("Refresh token response JSON: {Json}", rawJson);

        var apiResponse = JsonSerializer.Deserialize(rawJson, AppJsonSerializerContext.Default.ApiResponseLoginResponseDto);
        var refreshResponse = apiResponse?.Data;

        if (apiResponse?.Succeeded == true && refreshResponse?.Succeeded == true && !string.IsNullOrEmpty(refreshResponse.Token))
        {
            await _tokenStorage.SetTokenAsync(refreshResponse.Token);

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
