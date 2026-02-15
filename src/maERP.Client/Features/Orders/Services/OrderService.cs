using System.Net.Http.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Json;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.Order;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Orders.Services;

/// <summary>
/// Implementation of order service using HTTP client.
/// </summary>
public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<OrderService> logger)
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

    public async Task<PaginatedResponse<OrderListDto>> GetOrdersAsync(
        QueryParameters parameters,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Orders.Base}?{parameters.ToQueryString()}";

        _logger.LogInformation("Fetching orders from URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetFromJsonAsync(
                url, AppJsonSerializerContext.Default.PaginatedResponseOrderListDto, ct);

            if (response?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response: {Messages}",
                    string.Join(", ", response?.Messages ?? new List<string>()));
                return new PaginatedResponse<OrderListDto>();
            }

            _logger.LogInformation(
                "Fetched {Count} orders (Page {Page}/{TotalPages}, Total: {Total})",
                response.Data?.Count ?? 0,
                response.CurrentPage,
                response.TotalPages,
                response.TotalCount);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching orders from {Url}", url);
            throw;
        }
    }

    public async Task<OrderDetailDto?> GetOrderAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Orders.ById(id)}";
        var apiResponse = await _httpClient.GetFromJsonAsync(url, AppJsonSerializerContext.Default.ApiResponseOrderDetailDto, ct);
        return apiResponse?.Data;
    }

    public async Task CreateOrderAsync(OrderInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Orders.Base}";
        var response = await _httpClient.PostAsJsonAsync(url, input, AppJsonSerializerContext.Default.OrderInputDto, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task UpdateOrderAsync(Guid id, OrderInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Orders.ById(id)}";
        var response = await _httpClient.PutAsJsonAsync(url, input, AppJsonSerializerContext.Default.OrderInputDto, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task DeleteOrderAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Orders.ById(id)}";
        var response = await _httpClient.DeleteAsync(url, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }
}
