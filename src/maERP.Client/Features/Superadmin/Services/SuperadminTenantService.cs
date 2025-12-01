using System.Net.Http.Json;
using System.Text.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.Tenant;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Superadmin.Services;

/// <summary>
/// Implementation of superadmin tenant service using HTTP client.
/// </summary>
public class SuperadminTenantService : ISuperadminTenantService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<SuperadminTenantService> _logger;

    public SuperadminTenantService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<SuperadminTenantService> logger)
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
        var url = $"{baseUrl}{ApiEndpoints.Superadmin.Tenants}?{parameters.ToQueryString()}";

        _logger.LogInformation("Fetching superadmin tenants from URL: {Url}", url);

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
            _logger.LogError(ex, "Error fetching superadmin tenants from {Url}", url);
            throw;
        }
    }

    public async Task<TenantDetailDto?> GetTenantAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Superadmin.TenantById(id)}";
        var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<TenantDetailDto>>(url, JsonOptions, ct);
        return apiResponse?.Data;
    }

    public async Task UpdateTenantAsync(Guid id, TenantInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Superadmin.TenantById(id)}";

        _logger.LogInformation("Updating tenant {Id} at URL: {Url}", id, url);

        var response = await _httpClient.PutAsJsonAsync(url, input, JsonOptions, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }
}
