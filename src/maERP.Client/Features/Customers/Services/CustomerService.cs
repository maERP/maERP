using System.Net.Http.Json;
using System.Text.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.Customer;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Customers.Services;

/// <summary>
/// Implementation of customer service using HTTP client.
/// </summary>
public class CustomerService : ICustomerService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<CustomerService> _logger;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public CustomerService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<CustomerService> logger)
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

    public async Task<PaginatedResponse<CustomerListDto>> GetCustomersAsync(
        QueryParameters parameters,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Customers.Base}?{parameters.ToQueryString()}";

        _logger.LogInformation("Fetching customers from URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<CustomerListDto>>(
                url, _jsonOptions, ct);

            if (response?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response: {Messages}",
                    string.Join(", ", response?.Messages ?? new List<string>()));
                return new PaginatedResponse<CustomerListDto>();
            }

            _logger.LogInformation(
                "Fetched {Count} customers (Page {Page}/{TotalPages}, Total: {Total})",
                response.Data?.Count ?? 0,
                response.CurrentPage,
                response.TotalPages,
                response.TotalCount);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching customers from {Url}", url);
            throw;
        }
    }

    public async Task<CustomerDetailDto?> GetCustomerAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Customers.ById(id)}";
        var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<CustomerDetailDto>>(url, _jsonOptions, ct);
        return apiResponse?.Data;
    }

    public async Task CreateCustomerAsync(CustomerInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Customers.Base}";
        var response = await _httpClient.PostAsJsonAsync(url, input, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateCustomerAsync(Guid id, CustomerInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Customers.ById(id)}";
        var response = await _httpClient.PutAsJsonAsync(url, input, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCustomerAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Customers.ById(id)}";
        var response = await _httpClient.DeleteAsync(url, ct);
        response.EnsureSuccessStatusCode();
    }
}
