using System.Net.Http.Json;
using System.Text.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.Country;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Countries.Services;

/// <summary>
/// Implementation of country service using HTTP client.
/// </summary>
public class CountryService : ICountryService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<CountryService> _logger;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    // Cache for countries (they rarely change)
    private List<CountryListDto>? _cachedCountries;
    private DateTime _cacheTime = DateTime.MinValue;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);

    public CountryService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<CountryService> logger)
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

    public async Task<List<CountryListDto>> GetCountriesAsync(CancellationToken ct = default)
    {
        // Return cached countries if still valid
        if (_cachedCountries != null && DateTime.UtcNow - _cacheTime < CacheDuration)
        {
            _logger.LogDebug("Returning {Count} cached countries", _cachedCountries.Count);
            return _cachedCountries;
        }

        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Countries.Base}?pageSize=300&orderBy=Name";

        _logger.LogInformation("Fetching countries from URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<CountryListDto>>(
                url, _jsonOptions, ct);

            if (response?.Succeeded != true || response.Data == null)
            {
                _logger.LogWarning("API returned unsuccessful response: {Messages}",
                    string.Join(", ", response?.Messages ?? new List<string>()));
                return _cachedCountries ?? new List<CountryListDto>();
            }

            _cachedCountries = response.Data;
            _cacheTime = DateTime.UtcNow;

            _logger.LogInformation("Fetched and cached {Count} countries", _cachedCountries.Count);

            return _cachedCountries;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching countries from {Url}", url);
            // Return cached data if available, otherwise throw
            if (_cachedCountries != null)
            {
                _logger.LogWarning("Returning stale cache due to error");
                return _cachedCountries;
            }
            throw;
        }
    }
}
