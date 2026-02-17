using System.Net.Http.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Json;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.Statistic;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.SalesChannelDashboards.Services;

/// <summary>
/// Implementation of SalesChannel-filtered statistics service using HTTP client.
/// </summary>
public class SalesChannelStatisticsService : ISalesChannelStatisticsService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<SalesChannelStatisticsService> _logger;

    public SalesChannelStatisticsService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<SalesChannelStatisticsService> logger)
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

    public async Task<SalesTodayDto?> GetSalesTodayAsync(Guid salesChannelId, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Statistics.SalesToday}?salesChannelId={salesChannelId}";

        _logger.LogInformation("Fetching sales today statistics for SalesChannel {SalesChannelId} from URL: {Url}", salesChannelId, url);

        try
        {
            var apiResponse = await _httpClient.GetFromJsonAsync(
                url, AppJsonSerializerContext.Default.ApiResponseSalesTodayDto, ct);

            if (apiResponse?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response for SalesToday (SalesChannel {SalesChannelId})", salesChannelId);
                return null;
            }

            if (apiResponse.Data == null)
            {
                _logger.LogWarning("API returned success but SalesToday Data is null (SalesChannel {SalesChannelId})", salesChannelId);
                return new SalesTodayDto();
            }

            return apiResponse.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching sales today statistics for SalesChannel {SalesChannelId}", salesChannelId);
            throw;
        }
    }

    public async Task<OrdersTodayDto?> GetOrdersTodayAsync(Guid salesChannelId, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Statistics.OrdersToday}?salesChannelId={salesChannelId}";

        _logger.LogInformation("Fetching orders today statistics for SalesChannel {SalesChannelId} from URL: {Url}", salesChannelId, url);

        try
        {
            var apiResponse = await _httpClient.GetFromJsonAsync(
                url, AppJsonSerializerContext.Default.ApiResponseOrdersTodayDto, ct);

            if (apiResponse?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response for OrdersToday (SalesChannel {SalesChannelId})", salesChannelId);
                return null;
            }

            if (apiResponse.Data == null)
            {
                _logger.LogWarning("API returned success but OrdersToday Data is null (SalesChannel {SalesChannelId})", salesChannelId);
                return new OrdersTodayDto();
            }

            return apiResponse.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching orders today statistics for SalesChannel {SalesChannelId}", salesChannelId);
            throw;
        }
    }

    public async Task<OrdersLatestDto?> GetOrdersLatestAsync(Guid salesChannelId, int count = 5, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Statistics.OrdersLatest}?count={count}&salesChannelId={salesChannelId}";

        _logger.LogInformation("Fetching latest orders for SalesChannel {SalesChannelId} from URL: {Url}", salesChannelId, url);

        try
        {
            var apiResponse = await _httpClient.GetFromJsonAsync(
                url, AppJsonSerializerContext.Default.ApiResponseOrdersLatestDto, ct);

            if (apiResponse?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response for OrdersLatest (SalesChannel {SalesChannelId})", salesChannelId);
                return null;
            }

            if (apiResponse.Data == null)
            {
                _logger.LogWarning("API returned success but OrdersLatest Data is null (SalesChannel {SalesChannelId})", salesChannelId);
                return new OrdersLatestDto();
            }

            return apiResponse.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching latest orders for SalesChannel {SalesChannelId}", salesChannelId);
            throw;
        }
    }
}
