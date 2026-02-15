using System.Net.Http.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Json;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.Manufacturer;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Manufacturers.Services;

/// <summary>
/// Implementation of manufacturer service using HTTP client.
/// </summary>
public class ManufacturerService : IManufacturerService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<ManufacturerService> _logger;

    public ManufacturerService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<ManufacturerService> logger)
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

    public async Task<PaginatedResponse<ManufacturerListDto>> GetManufacturersAsync(
        QueryParameters parameters,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Manufacturers.Base}?{parameters.ToQueryString()}";

        _logger.LogInformation("Fetching manufacturers from URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetFromJsonAsync(
                url, AppJsonSerializerContext.Default.PaginatedResponseManufacturerListDto, ct);

            if (response?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response: {Messages}",
                    string.Join(", ", response?.Messages ?? new List<string>()));
                return new PaginatedResponse<ManufacturerListDto>();
            }

            _logger.LogInformation(
                "Fetched {Count} manufacturers (Page {Page}/{TotalPages}, Total: {Total})",
                response.Data?.Count ?? 0,
                response.CurrentPage,
                response.TotalPages,
                response.TotalCount);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching manufacturers from {Url}", url);
            throw;
        }
    }

    public async Task<ManufacturerDetailDto?> GetManufacturerAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Manufacturers.ById(id)}";

        _logger.LogInformation("Fetching manufacturer {Id} from URL: {Url}", id, url);

        try
        {
            var response = await _httpClient.GetFromJsonAsync(
                url, AppJsonSerializerContext.Default.ManufacturerDetailDto, ct);

            return response;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Manufacturer {Id} not found", id);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching manufacturer {Id} from {Url}", id, url);
            throw;
        }
    }

    public async Task CreateManufacturerAsync(
        ManufacturerInputDto input,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Manufacturers.Base}";

        _logger.LogInformation("Creating manufacturer at URL: {Url}", url);

        var response = await _httpClient.PostAsJsonAsync(url, input, AppJsonSerializerContext.Default.ManufacturerInputDto, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task UpdateManufacturerAsync(
        Guid id,
        ManufacturerInputDto input,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Manufacturers.ById(id)}";

        _logger.LogInformation("Updating manufacturer {Id} at URL: {Url}", id, url);

        var response = await _httpClient.PutAsJsonAsync(url, input, AppJsonSerializerContext.Default.ManufacturerInputDto, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }
}
