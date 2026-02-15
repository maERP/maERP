using System.Net.Http.Json;
using System.Web;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Json;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Dtos.User;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Tenants.Services;

/// <summary>
/// Implementation of tenant service using HTTP client.
/// </summary>
public class TenantService : ITenantService
{
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
            var response = await _httpClient.GetFromJsonAsync(
                url, AppJsonSerializerContext.Default.PaginatedResponseTenantListDto, ct);

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
        var apiResponse = await _httpClient.GetFromJsonAsync(url, AppJsonSerializerContext.Default.ApiResponseTenantDetailDto, ct);
        return apiResponse?.Data;
    }

    public async Task<Guid> CreateTenantAsync(TenantInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Tenants.Base}";

        _logger.LogInformation("Creating tenant at URL: {Url}", url);

        var response = await _httpClient.PostAsJsonAsync(url, input, AppJsonSerializerContext.Default.TenantInputDto, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);

        var apiResponse = await response.Content.ReadFromJsonAsync(AppJsonSerializerContext.Default.ApiResponseGuid, ct);
        return apiResponse?.Data ?? Guid.Empty;
    }

    public async Task UpdateTenantAsync(Guid id, TenantInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Tenants.ById(id)}";

        _logger.LogInformation("Updating tenant {Id} at URL: {Url}", id, url);

        var response = await _httpClient.PutAsJsonAsync(url, input, AppJsonSerializerContext.Default.TenantInputDto, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task<UserListDto?> SearchUserByEmailAsync(Guid tenantId, string email, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var encodedEmail = HttpUtility.UrlEncode(email);
        var url = $"{baseUrl}{ApiEndpoints.Tenants.UserSearch(tenantId)}?email={encodedEmail}";

        _logger.LogInformation("Searching user by email at URL: {Url}", url);

        var response = await _httpClient.GetAsync(url, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);

        var apiResponse = await response.Content.ReadFromJsonAsync(AppJsonSerializerContext.Default.ApiResponseUserListDto, ct);
        return apiResponse?.Data;
    }

    public async Task AddUserToTenantAsync(Guid tenantId, string email, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Tenants.Users(tenantId)}";

        _logger.LogInformation("Adding user to tenant {TenantId} at URL: {Url}", tenantId, url);

        var payload = new AddUserToTenantPayload(email);
        var response = await _httpClient.PostAsJsonAsync(url, payload, AppJsonSerializerContext.Default.AddUserToTenantPayload, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task DeleteTenantAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Tenants.ById(id)}";

        _logger.LogInformation("Deleting tenant {Id} at URL: {Url}", id, url);

        var response = await _httpClient.DeleteAsync(url, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }
}
