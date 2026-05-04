using System.Net.Http.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Json;
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
            var response = await _httpClient.GetFromJsonAsync(
                url, AppJsonSerializerContext.Default.PaginatedResponseSalesChannelListDto, ct);

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
        var apiResponse = await _httpClient.GetFromJsonAsync(url, AppJsonSerializerContext.Default.ApiResponseSalesChannelDetailDto, ct);
        return apiResponse?.Data;
    }

    public async Task CreateSalesChannelAsync(SalesChannelInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SalesChannels.Base}";
        var response = await _httpClient.PostAsJsonAsync(url, input, AppJsonSerializerContext.Default.SalesChannelInputDto, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task UpdateSalesChannelAsync(Guid id, SalesChannelInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SalesChannels.ById(id)}";
        var response = await _httpClient.PutAsJsonAsync(url, input, AppJsonSerializerContext.Default.SalesChannelInputDto, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task<SalesChannelSyncResultDto?> TriggerSyncAsync(Guid id, string operation, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SalesChannels.Sync(id, operation)}";
        var response = await _httpClient.PostAsync(url, content: null, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
        return await response.Content.ReadFromJsonAsync(
            AppJsonSerializerContext.Default.SalesChannelSyncResultDto, ct);
    }

    public async Task<SalesChannelSyncResultDto?> TestConnectionAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SalesChannels.TestConnection(id)}";
        var response = await _httpClient.PostAsync(url, content: null, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
        return await response.Content.ReadFromJsonAsync(
            AppJsonSerializerContext.Default.SalesChannelSyncResultDto, ct);
    }

    public async Task<List<ChannelSyncRunDto>> GetSyncRunsAsync(Guid id, int take = 50, int offset = 0, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SalesChannels.SyncRuns(id)}?take={take}&offset={offset}";
        var response = await _httpClient.GetFromJsonAsync(
            url, AppJsonSerializerContext.Default.ListChannelSyncRunDto, ct);
        return response ?? new List<ChannelSyncRunDto>();
    }

    public async Task<List<ChannelExportOutboxDto>> GetDeadLetterAsync(Guid id, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SalesChannels.DeadLetter(id)}";
        var response = await _httpClient.GetFromJsonAsync(
            url, AppJsonSerializerContext.Default.ListChannelExportOutboxDto, ct);
        return response ?? new List<ChannelExportOutboxDto>();
    }

    public async Task RetryDeadLetterAsync(Guid id, Guid outboxId, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SalesChannels.RetryDeadLetter(id, outboxId)}";
        var response = await _httpClient.PostAsync(url, content: null, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task<OAuthStartResult> StartOAuthAsync(Guid id, string provider, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SalesChannels.OAuthStart(id, provider)}";
        var response = await _httpClient.PostAsync(url, content: null, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);

        var apiResponse = await response.Content.ReadFromJsonAsync(
            AppJsonSerializerContext.Default.ApiResponseOAuthStartResponseDto, ct);
        var dto = apiResponse?.Data
                  ?? throw new InvalidOperationException("OAuth start response was empty.");
        return new OAuthStartResult(dto.AuthorizeUrl, dto.State);
    }

    public async Task DisconnectOAuthAsync(Guid id, string provider, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SalesChannels.OAuthDisconnect(id, provider)}";
        var response = await _httpClient.PostAsync(url, content: null, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }
}
