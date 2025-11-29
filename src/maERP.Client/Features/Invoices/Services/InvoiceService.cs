using System.Net.Http.Json;
using System.Text.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.Invoice;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Invoices.Services;

/// <summary>
/// Implementation of invoice service using HTTP client.
/// </summary>
public class InvoiceService : IInvoiceService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<InvoiceService> _logger;

    public InvoiceService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<InvoiceService> logger)
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

    public async Task<PaginatedResponse<InvoiceListDto>> GetInvoicesAsync(
        QueryParameters parameters,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Invoices.Base}?{parameters.ToQueryString()}";

        _logger.LogInformation("Fetching invoices from URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<InvoiceListDto>>(
                url, JsonOptions, ct);

            if (response?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response: {Messages}",
                    string.Join(", ", response?.Messages ?? new List<string>()));
                return new PaginatedResponse<InvoiceListDto>();
            }

            _logger.LogInformation(
                "Fetched {Count} invoices (Page {Page}/{TotalPages}, Total: {Total})",
                response.Data?.Count ?? 0,
                response.CurrentPage,
                response.TotalPages,
                response.TotalCount);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching invoices from {Url}", url);
            throw;
        }
    }

    public async Task<InvoiceDetailDto?> GetInvoiceAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Invoices.ById(id)}";
        var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse<InvoiceDetailDto>>(url, JsonOptions, ct);
        return apiResponse?.Data;
    }
}
