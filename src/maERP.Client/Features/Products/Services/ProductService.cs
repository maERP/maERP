using System.Net.Http.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Json;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.Product;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Products.Services;

/// <summary>
/// Implementation of product service using HTTP client.
/// </summary>
public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<ProductService> logger)
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

    public async Task<PaginatedResponse<ProductListDto>> GetProductsAsync(
        QueryParameters parameters,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Products.Base}?{parameters.ToQueryString()}";

        _logger.LogInformation("Fetching products from URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetFromJsonAsync(
                url, AppJsonSerializerContext.Default.PaginatedResponseProductListDto, ct);

            if (response?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response: {Messages}",
                    string.Join(", ", response?.Messages ?? new List<string>()));
                return new PaginatedResponse<ProductListDto>();
            }

            _logger.LogInformation(
                "Fetched {Count} products (Page {Page}/{TotalPages}, Total: {Total})",
                response.Data?.Count ?? 0,
                response.CurrentPage,
                response.TotalPages,
                response.TotalCount);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching products from {Url}", url);
            throw;
        }
    }

    public async Task<ProductDetailDto?> GetProductAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Products.ById(id)}";
        var apiResponse = await _httpClient.GetFromJsonAsync(url, AppJsonSerializerContext.Default.ApiResponseProductDetailDto, ct);
        return apiResponse?.Data;
    }

    public async Task CreateProductAsync(ProductInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Products.Base}";

        _logger.LogInformation("Creating product at URL: {Url}", url);

        var response = await _httpClient.PostAsJsonAsync(url, input, AppJsonSerializerContext.Default.ProductInputDto, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task UpdateProductAsync(Guid id, ProductInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Products.ById(id)}";

        _logger.LogInformation("Updating product {Id} at URL: {Url}", id, url);

        var response = await _httpClient.PutAsJsonAsync(url, input, AppJsonSerializerContext.Default.ProductInputDto, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }
}
