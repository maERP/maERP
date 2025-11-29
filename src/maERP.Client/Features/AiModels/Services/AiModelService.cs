using System.Net.Http.Json;
using System.Text.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.AiModel;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.AiModels.Services;

/// <summary>
/// Implementation of AI model service using HTTP client.
/// </summary>
public class AiModelService : IAiModelService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<AiModelService> _logger;

    public AiModelService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<AiModelService> logger)
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

    public async Task<PaginatedResponse<AiModelListDto>> GetAiModelsAsync(
        QueryParameters parameters,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.AiModels.Base}?{parameters.ToQueryString()}";

        _logger.LogInformation("Fetching AI models from URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<AiModelListDto>>(
                url, JsonOptions, ct);

            if (response?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response: {Messages}",
                    string.Join(", ", response?.Messages ?? new List<string>()));
                return new PaginatedResponse<AiModelListDto>();
            }

            _logger.LogInformation(
                "Fetched {Count} AI models (Page {Page}/{TotalPages}, Total: {Total})",
                response.Data?.Count ?? 0,
                response.CurrentPage,
                response.TotalPages,
                response.TotalCount);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching AI models from {Url}", url);
            throw;
        }
    }
}
