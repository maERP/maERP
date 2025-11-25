using System.Collections.Immutable;
using System.Net.Http.Json;
using maERP.Client.Core.Constants;
using maERP.Domain.Dtos.Customer;

namespace maERP.Client.Features.Customers.Services;

/// <summary>
/// Implementation of customer service using HTTP client.
/// </summary>
public class CustomerService : ICustomerService
{
    private readonly HttpClient _httpClient;

    public CustomerService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("MaErpApi");
    }

    public async Task<IImmutableList<CustomerListDto>> GetCustomersAsync(
        int page = 0,
        int pageSize = 20,
        string? searchQuery = null,
        CancellationToken ct = default)
    {
        var url = $"{ApiEndpoints.Customers.Base}?page={page}&pageSize={pageSize}";
        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            url += $"&search={Uri.EscapeDataString(searchQuery)}";
        }

        var response = await _httpClient.GetFromJsonAsync<List<CustomerListDto>>(url, ct);
        return response?.ToImmutableList() ?? ImmutableList<CustomerListDto>.Empty;
    }

    public async Task<CustomerDetailDto?> GetCustomerAsync(Guid id, CancellationToken ct = default)
    {
        var url = ApiEndpoints.Customers.ById(id);
        return await _httpClient.GetFromJsonAsync<CustomerDetailDto>(url, ct);
    }

    public async Task<CustomerDetailDto> CreateCustomerAsync(CustomerInputDto input, CancellationToken ct = default)
    {
        var response = await _httpClient.PostAsJsonAsync(ApiEndpoints.Customers.Base, input, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CustomerDetailDto>(ct)
            ?? throw new InvalidOperationException("Failed to create customer");
    }

    public async Task<CustomerDetailDto> UpdateCustomerAsync(Guid id, CustomerInputDto input, CancellationToken ct = default)
    {
        var url = ApiEndpoints.Customers.ById(id);
        var response = await _httpClient.PutAsJsonAsync(url, input, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CustomerDetailDto>(ct)
            ?? throw new InvalidOperationException("Failed to update customer");
    }

    public async Task DeleteCustomerAsync(Guid id, CancellationToken ct = default)
    {
        var url = ApiEndpoints.Customers.ById(id);
        var response = await _httpClient.DeleteAsync(url, ct);
        response.EnsureSuccessStatusCode();
    }
}
