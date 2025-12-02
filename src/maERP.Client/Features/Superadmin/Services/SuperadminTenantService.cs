using System.Net.Http.Json;
using System.Text.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.Superadmin;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Dtos.User;
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

    public async Task<SuperadminTenantDetailDto?> GetTenantDetailAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Superadmin.TenantById(id)}";

        _logger.LogInformation("Fetching tenant details with users from URL: {Url}", url);

        try
        {
            var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<SuperadminTenantDetailDto>>(url, JsonOptions, ct);
            return apiResponse?.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching tenant details from {Url}", url);
            throw;
        }
    }

    public async Task UpdateTenantAsync(Guid id, TenantInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Superadmin.TenantById(id)}";

        _logger.LogInformation("Updating tenant {Id} at URL: {Url}", id, url);

        var response = await _httpClient.PutAsJsonAsync(url, input, JsonOptions, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task AssignUserToTenantAsync(string userId, Guid tenantId, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Superadmin.UserTenants(userId)}";

        _logger.LogInformation("Assigning user {UserId} to tenant {TenantId} at URL: {Url}", userId, tenantId, url);

        var payload = new { TenantId = tenantId };
        var response = await _httpClient.PostAsJsonAsync(url, payload, JsonOptions, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task RemoveUserFromTenantAsync(string userId, Guid tenantId, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Superadmin.UserTenantById(userId, tenantId)}";

        _logger.LogInformation("Removing user {UserId} from tenant {TenantId} at URL: {Url}", userId, tenantId, url);

        var response = await _httpClient.DeleteAsync(url, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task<List<UserListDto>> GetAllUsersAsync(CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        // Fetch a large page to get all users
        var url = $"{baseUrl}{ApiEndpoints.Superadmin.Users}?pageSize=1000";

        _logger.LogInformation("Fetching all users from URL: {Url}", url);

        try
        {
            var apiResponse = await _httpClient.GetFromJsonAsync<PaginatedResponse<UserListDto>>(url, JsonOptions, ct);
            return apiResponse?.Data ?? new List<UserListDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching users from {Url}", url);
            throw;
        }
    }
}
