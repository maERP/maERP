using System.Net.Http.Json;
using System.Text.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.Tenant;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Tenants.Services;

/// <summary>
/// Implementation of tenant service using HTTP client.
/// </summary>
public class TenantService : ITenantService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<TenantService> _logger;

    public TenantService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<TenantService> logger)
    {
        _httpClient = httpClientFactory.CreateClient("MaErpApi");
        _tokenStorage = tokenStorage;
        _logger = logger;
    }

    private async Task<string> GetBaseUrlAsync()
    {
        var serverUrl = await _tokenStorage.GetServerUrlAsync();
        if (string.IsNullOrEmpty(serverUrl))
        {
            throw new InvalidOperationException("Server URL is not configured. Please login first.");
        }
        return serverUrl.TrimEnd('/');
    }

    public async Task<PaginatedResponse<TenantListDto>> GetTenantsAsync(
        QueryParameters parameters,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Tenants.Base}?{parameters.ToQueryString()}";

        _logger.LogInformation("Fetching tenants from URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<TenantListDto>>(
                url, JsonOptions, ct);

            if (response?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response: {Messages}",
                    string.Join(", ", response?.Messages ?? new List<string>()));
                return new PaginatedResponse<TenantListDto>();
            }

            _logger.LogInformation(
                "Fetched {Count} tenants (Page {Page}/{TotalPages}, Total: {Total})",
                response.Data?.Count ?? 0,
                response.CurrentPage,
                response.TotalPages,
                response.TotalCount);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching tenants from {Url}", url);
            throw;
        }
    }

    public async Task<TenantDetailDto?> GetTenantAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Tenants.ById(id)}";
        var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<TenantDetailDto>>(url, JsonOptions, ct);
        return apiResponse?.Data;
    }

    public async Task<Guid> CreateTenantAsync(TenantInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Tenants.Base}";

        _logger.LogInformation("Creating tenant at URL: {Url}", url);

        var response = await _httpClient.PostAsJsonAsync(url, input, JsonOptions, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Guid>>(JsonOptions, ct);
        return apiResponse?.Data ?? Guid.Empty;
    }

    public async Task UpdateTenantAsync(Guid id, TenantInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Tenants.ById(id)}";

        _logger.LogInformation("Updating tenant {Id} at URL: {Url}", id, url);

        var response = await _httpClient.PutAsJsonAsync(url, input, JsonOptions, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }
}
