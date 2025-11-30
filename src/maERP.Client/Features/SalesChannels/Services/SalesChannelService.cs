using System.Net.Http.Json;
using System.Text.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.SalesChannel;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.SalesChannels.Services;

/// <summary>
/// Implementation of sales channel service using HTTP client.
/// </summary>
public class SalesChannelService : ISalesChannelService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<SalesChannelService> _logger;

    public SalesChannelService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<SalesChannelService> logger)
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

    public async Task<PaginatedResponse<SalesChannelListDto>> GetSalesChannelsAsync(
        QueryParameters parameters,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SalesChannels.Base}?{parameters.ToQueryString()}";

        _logger.LogInformation("Fetching sales channels from URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<SalesChannelListDto>>(
                url, JsonOptions, ct);

            if (response?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response: {Messages}",
                    string.Join(", ", response?.Messages ?? new List<string>()));
                return new PaginatedResponse<SalesChannelListDto>();
            }

            _logger.LogInformation(
                "Fetched {Count} sales channels (Page {Page}/{TotalPages}, Total: {Total})",
                response.Data?.Count ?? 0,
                response.CurrentPage,
                response.TotalPages,
                response.TotalCount);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching sales channels from {Url}", url);
            throw;
        }
    }

    public async Task<SalesChannelDetailDto?> GetSalesChannelAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SalesChannels.ById(id)}";
        var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<SalesChannelDetailDto>>(url, JsonOptions, ct);
        return apiResponse?.Data;
    }

    public async Task CreateSalesChannelAsync(SalesChannelInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SalesChannels.Base}";
        var response = await _httpClient.PostAsJsonAsync(url, input, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task UpdateSalesChannelAsync(Guid id, SalesChannelInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SalesChannels.ById(id)}";
        var response = await _httpClient.PutAsJsonAsync(url, input, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }
}
