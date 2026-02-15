using System.Net.Http.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Json;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.AiPrompt;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.AiPrompts.Services;

/// <summary>
/// Implementation of AI prompt service using HTTP client.
/// </summary>
public class AiPromptService : IAiPromptService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<AiPromptService> _logger;

    public AiPromptService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<AiPromptService> logger)
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

    public async Task<PaginatedResponse<AiPromptListDto>> GetAiPromptsAsync(
        QueryParameters parameters,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.AiPrompts.Base}?{parameters.ToQueryString()}";

        _logger.LogInformation("Fetching AI prompts from URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetFromJsonAsync(
                url, AppJsonSerializerContext.Default.PaginatedResponseAiPromptListDto, ct);

            if (response?.Succeeded != true)
            {
                _logger.LogWarning("API returned unsuccessful response: {Messages}",
                    string.Join(", ", response?.Messages ?? new List<string>()));
                return new PaginatedResponse<AiPromptListDto>();
            }

            _logger.LogInformation(
                "Fetched {Count} AI prompts (Page {Page}/{TotalPages}, Total: {Total})",
                response.Data?.Count ?? 0,
                response.CurrentPage,
                response.TotalPages,
                response.TotalCount);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching AI prompts from {Url}", url);
            throw;
        }
    }

    public async Task<AiPromptDetailDto?> GetAiPromptAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.AiPrompts.ById(id)}";

        _logger.LogInformation("Fetching AI prompt {Id} from URL: {Url}", id, url);

        try
        {
            return await _httpClient.GetFromJsonAsync(url, AppJsonSerializerContext.Default.AiPromptDetailDto, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching AI prompt {Id} from {Url}", id, url);
            throw;
        }
    }

    public async Task<Guid> CreateAiPromptAsync(AiPromptInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.AiPrompts.Base}";

        _logger.LogInformation("Creating AI prompt at URL: {Url}", url);

        try
        {
            var response = await _httpClient.PostAsJsonAsync(url, input, AppJsonSerializerContext.Default.AiPromptInputDto, ct);
            await response.EnsureSuccessOrThrowApiExceptionAsync(ct);

            var createdId = await response.Content.ReadFromJsonAsync(AppJsonSerializerContext.Default.Guid, ct);
            _logger.LogInformation("Created AI prompt with ID: {Id}", createdId);
            return createdId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating AI prompt at {Url}", url);
            throw;
        }
    }

    public async Task UpdateAiPromptAsync(Guid id, AiPromptInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.AiPrompts.ById(id)}";

        _logger.LogInformation("Updating AI prompt {Id} at URL: {Url}", id, url);

        try
        {
            var response = await _httpClient.PutAsJsonAsync(url, input, AppJsonSerializerContext.Default.AiPromptInputDto, ct);
            await response.EnsureSuccessOrThrowApiExceptionAsync(ct);

            _logger.LogInformation("Updated AI prompt {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating AI prompt {Id} at {Url}", id, url);
            throw;
        }
    }

    public async Task DeleteAiPromptAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.AiPrompts.ById(id)}";

        _logger.LogInformation("Deleting AI prompt {Id} at URL: {Url}", id, url);

        try
        {
            var response = await _httpClient.DeleteAsync(url, ct);
            await response.EnsureSuccessOrThrowApiExceptionAsync(ct);

            _logger.LogInformation("Deleted AI prompt {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting AI prompt {Id} at {Url}", id, url);
            throw;
        }
    }
}
