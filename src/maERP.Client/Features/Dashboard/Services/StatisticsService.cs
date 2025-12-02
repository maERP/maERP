using System.Net.Http.Json;
using System.Text.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.Statistic;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Dashboard.Services;

/// <summary>
/// Implementation of statistics service using HTTP client.
/// </summary>
public class StatisticsService : IStatisticsService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<StatisticsService> _logger;

    public StatisticsService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<StatisticsService> logger)
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

    public async Task<SalesTodayDto?> GetSalesTodayAsync(CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Statistics.SalesToday}";

        _logger.LogInformation("Fetching sales today statistics from URL: {Url}", url);

        try
        {
            var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<SalesTodayDto>>(
                url, JsonOptions, ct);

            if (apiResponse?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response for SalesToday: {Messages}",
                    string.Join(", ", apiResponse?.Messages ?? new List<string>()));
                return null;
            }

            if (apiResponse.Data == null)
            {
                _logger.LogWarning("API returned success but SalesToday Data is null");
                return new SalesTodayDto();
            }

            _logger.LogInformation("Successfully fetched sales today statistics - RevenueToday: {RevenueToday}",
                apiResponse.Data.RevenueToday);
            return apiResponse.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching sales today statistics from {Url}", url);
            throw;
        }
    }

    public async Task<OrdersTodayDto?> GetOrdersTodayAsync(CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Statistics.OrdersToday}";

        _logger.LogInformation("Fetching orders today statistics from URL: {Url}", url);

        try
        {
            var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<OrdersTodayDto>>(
                url, JsonOptions, ct);

            if (apiResponse?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response for OrdersToday: {Messages}",
                    string.Join(", ", apiResponse?.Messages ?? new List<string>()));
                return null;
            }

            if (apiResponse.Data == null)
            {
                _logger.LogWarning("API returned success but OrdersToday Data is null");
                return new OrdersTodayDto();
            }

            _logger.LogInformation("Successfully fetched orders today statistics - OrdersToday: {OrdersToday}",
                apiResponse.Data.OrdersToday);
            return apiResponse.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching orders today statistics from {Url}", url);
            throw;
        }
    }

    public async Task<CustomersTodayDto?> GetCustomersTodayAsync(CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Statistics.CustomersToday}";

        _logger.LogInformation("Fetching customers today statistics from URL: {Url}", url);

        try
        {
            var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<CustomersTodayDto>>(
                url, JsonOptions, ct);

            if (apiResponse?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response for CustomersToday: {Messages}",
                    string.Join(", ", apiResponse?.Messages ?? new List<string>()));
                return null;
            }

            if (apiResponse.Data == null)
            {
                _logger.LogWarning("API returned success but CustomersToday Data is null");
                return new CustomersTodayDto();
            }

            _logger.LogInformation("Successfully fetched customers today statistics - CustomersTotal: {CustomersTotal}",
                apiResponse.Data.CustomersTotal);
            return apiResponse.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching customers today statistics from {Url}", url);
            throw;
        }
    }

    public async Task<ProductsTodayDto?> GetProductsTodayAsync(CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Statistics.ProductsToday}";

        _logger.LogInformation("Fetching products today statistics from URL: {Url}", url);

        try
        {
            var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<ProductsTodayDto>>(
                url, JsonOptions, ct);

            if (apiResponse?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response for ProductsToday: {Messages}",
                    string.Join(", ", apiResponse?.Messages ?? new List<string>()));
                return null;
            }

            if (apiResponse.Data == null)
            {
                _logger.LogWarning("API returned success but ProductsToday Data is null");
                return new ProductsTodayDto();
            }

            _logger.LogInformation("Successfully fetched products today statistics - ProductsTotal: {ProductsTotal}",
                apiResponse.Data.ProductsTotal);
            return apiResponse.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching products today statistics from {Url}", url);
            throw;
        }
    }

    public async Task<OrdersLatestDto?> GetOrdersLatestAsync(int count = 5, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Statistics.OrdersLatest}?count={count}";

        _logger.LogInformation("Fetching latest orders from URL: {Url}", url);

        try
        {
            var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<OrdersLatestDto>>(
                url, JsonOptions, ct);

            if (apiResponse?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response for OrdersLatest: {Messages}",
                    string.Join(", ", apiResponse?.Messages ?? new List<string>()));
                return null;
            }

            if (apiResponse.Data == null)
            {
                _logger.LogWarning("API returned success but OrdersLatest Data is null");
                return new OrdersLatestDto();
            }

            _logger.LogInformation("Successfully fetched {Count} latest orders", apiResponse.Data.Orders.Count);
            return apiResponse.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching latest orders from {Url}", url);
            throw;
        }
    }

    public async Task<ProductsBestSellingDto?> GetProductsBestSellingAsync(int count = 5, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Statistics.ProductsBestSelling}?count={count}";

        _logger.LogInformation("Fetching best-selling products from URL: {Url}", url);

        try
        {
            var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<ProductsBestSellingDto>>(
                url, JsonOptions, ct);

            if (apiResponse?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response for ProductsBestSelling: {Messages}",
                    string.Join(", ", apiResponse?.Messages ?? new List<string>()));
                return null;
            }

            if (apiResponse.Data == null)
            {
                _logger.LogWarning("API returned success but ProductsBestSelling Data is null");
                return new ProductsBestSellingDto();
            }

            _logger.LogInformation("Successfully fetched {Count} best-selling products", apiResponse.Data.Products.Count);
            return apiResponse.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching best-selling products from {Url}", url);
            throw;
        }
    }
}
