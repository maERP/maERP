using System.Net.Http.Json;
using System.Text.Json;
using maERP.Domain.Dtos.Auth;

namespace maERP.Client.Features.Auth.Services;

/// <summary>
/// Wrapper for API responses that contain data in a nested "Data" property
/// </summary>
internal class ApiResponse<T>
{
    public T? Data { get; set; }
    public int StatusCode { get; set; }
    public List<string> Messages { get; set; } = new();
    public bool Succeeded { get; set; }
}

public class MaErpAuthenticationService : IMaErpAuthenticationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<MaErpAuthenticationService> _logger;

    public MaErpAuthenticationService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<MaErpAuthenticationService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _tokenStorage = tokenStorage;
        _logger = logger;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(request.Server);

            var response = await httpClient.PostAsJsonAsync("/api/v1/auth/login", request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Login failed with status code: {StatusCode}", response.StatusCode);
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogWarning("Error response: {ErrorContent}", errorContent);
                return null;
            }

            var rawJson = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogDebug("Login response JSON: {Json}", rawJson);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Parse the wrapper response first
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<LoginResponseDto>>(rawJson, jsonOptions);
            var loginResponse = apiResponse?.Data;

            _logger.LogInformation("Parsed response - ApiSucceeded: {ApiSucceeded}, DataSucceeded: {DataSucceeded}, Token: {HasToken}, UserId: {UserId}",
                apiResponse?.Succeeded, loginResponse?.Succeeded, !string.IsNullOrEmpty(loginResponse?.Token), loginResponse?.UserId);

            if (apiResponse?.Succeeded == true && loginResponse?.Succeeded == true && !string.IsNullOrEmpty(loginResponse.Token))
            {
                await _tokenStorage.SetTokenAsync(loginResponse.Token);
                await _tokenStorage.SetServerUrlAsync(request.Server);

                if (loginResponse.CurrentTenantId.HasValue)
                {
                    await _tokenStorage.SetCurrentTenantIdAsync(loginResponse.CurrentTenantId.Value);
                }

                _logger.LogInformation("Login successful for user: {UserId}", loginResponse.UserId);
            }

            return loginResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            return null;
        }
    }

    public async Task<bool> ValidateTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        try
        {
            var serverUrl = await _tokenStorage.GetServerUrlAsync();
            if (string.IsNullOrEmpty(serverUrl))
            {
                return false;
            }

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(serverUrl);
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Try to call a protected endpoint to validate the token
            var response = await httpClient.GetAsync("/api/v1/auth/validate", cancellationToken);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error validating token");
            return false;
        }
    }
}
