using System.Net.Http.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Json;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.Warehouse;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Warehouses.Services;

/// <summary>
/// Implementation of warehouse service using HTTP client.
/// </summary>
public class WarehouseService : IWarehouseService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<WarehouseService> _logger;

    public WarehouseService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<WarehouseService> logger)
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

    public async Task<PaginatedResponse<WarehouseListDto>> GetWarehousesAsync(
        QueryParameters parameters,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Warehouses.Base}?{parameters.ToQueryString()}";

        _logger.LogInformation("Fetching warehouses from URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetFromJsonAsync(
                url, AppJsonSerializerContext.Default.PaginatedResponseWarehouseListDto, ct);

            if (response?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response: {Messages}",
                    string.Join(", ", response?.Messages ?? new List<string>()));
                return new PaginatedResponse<WarehouseListDto>();
            }

            _logger.LogInformation(
                "Fetched {Count} warehouses (Page {Page}/{TotalPages}, Total: {Total})",
                response.Data?.Count ?? 0,
                response.CurrentPage,
                response.TotalPages,
                response.TotalCount);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching warehouses from {Url}", url);
            throw;
        }
    }

    public async Task<WarehouseDetailDto?> GetWarehouseAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Warehouses.ById(id)}";
        var apiResponse = await _httpClient.GetFromJsonAsync(url, AppJsonSerializerContext.Default.ApiResponseWarehouseDetailDto, ct);
        return apiResponse?.Data;
    }

    public async Task CreateWarehouseAsync(WarehouseInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Warehouses.Base}";
        var response = await _httpClient.PostAsJsonAsync(url, input, AppJsonSerializerContext.Default.WarehouseInputDto, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task UpdateWarehouseAsync(Guid id, WarehouseInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Warehouses.ById(id)}";
        var response = await _httpClient.PutAsJsonAsync(url, input, AppJsonSerializerContext.Default.WarehouseInputDto, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }
}
