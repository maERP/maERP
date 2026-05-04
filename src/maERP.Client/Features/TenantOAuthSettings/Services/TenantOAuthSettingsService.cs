using System.Net.Http.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Json;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.TenantOAuthAppSettings;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.TenantOAuthSettings.Services;

public class TenantOAuthSettingsService : ITenantOAuthSettingsService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<TenantOAuthSettingsService> _logger;

    public TenantOAuthSettingsService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<TenantOAuthSettingsService> logger)
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

    public async Task<TenantOAuthAppSettingsDetailDto?> GetAsync(string provider, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.TenantOAuthAppSettings.ByProvider(provider)}";
        var response = await _httpClient.GetFromJsonAsync(
            url, AppJsonSerializerContext.Default.ApiResponseTenantOAuthAppSettingsDetailDto, ct);
        return response?.Data;
    }

    public async Task UpsertAsync(string provider, TenantOAuthAppSettingsInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.TenantOAuthAppSettings.ByProvider(provider)}";
        var response = await _httpClient.PutAsJsonAsync(
            url, input, AppJsonSerializerContext.Default.TenantOAuthAppSettingsInputDto, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task DeleteAsync(string provider, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.TenantOAuthAppSettings.ByProvider(provider)}";
        var response = await _httpClient.DeleteAsync(url, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }
}
