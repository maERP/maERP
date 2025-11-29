using System.Net.Http.Json;
using System.Text.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.TaxClass;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.TaxClasses.Services;

/// <summary>
/// Implementation of tax class service using HTTP client.
/// </summary>
public class TaxClassService : ITaxClassService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<TaxClassService> _logger;

    public TaxClassService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<TaxClassService> logger)
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

    public async Task<PaginatedResponse<TaxClassListDto>> GetTaxClassesAsync(
        QueryParameters parameters,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.TaxClasses.Base}?{parameters.ToQueryString()}";

        _logger.LogInformation("Fetching tax classes from URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<TaxClassListDto>>(
                url, JsonOptions, ct);

            if (response?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response: {Messages}",
                    string.Join(", ", response?.Messages ?? new List<string>()));
                return new PaginatedResponse<TaxClassListDto>();
            }

            _logger.LogInformation(
                "Fetched {Count} tax classes (Page {Page}/{TotalPages}, Total: {Total})",
                response.Data?.Count ?? 0,
                response.CurrentPage,
                response.TotalPages,
                response.TotalCount);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching tax classes from {Url}", url);
            throw;
        }
    }

    public async Task<TaxClassDetailDto?> GetTaxClassAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.TaxClasses.ById(id)}";
        var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<TaxClassDetailDto>>(url, JsonOptions, ct);
        return apiResponse?.Data;
    }
}
