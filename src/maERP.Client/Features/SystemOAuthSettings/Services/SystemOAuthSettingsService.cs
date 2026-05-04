using System.Net.Http.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Json;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.SystemOAuthSettings;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.SystemOAuthSettings.Services;

public class SystemOAuthSettingsService : ISystemOAuthSettingsService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<SystemOAuthSettingsService> _logger;

    public SystemOAuthSettingsService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<SystemOAuthSettingsService> logger)
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

    public async Task<SystemOAuthSettingsDto?> GetAsync(string provider, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SystemOAuthAppSettings.ByProvider(provider)}";
        var response = await _httpClient.GetFromJsonAsync(
            url, AppJsonSerializerContext.Default.ApiResponseSystemOAuthSettingsDto, ct);
        return response?.Data;
    }

    public async Task UpsertAsync(string provider, SystemOAuthSettingsInputDto input, CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.SystemOAuthAppSettings.ByProvider(provider)}";
        var response = await _httpClient.PutAsJsonAsync(
            url, input, AppJsonSerializerContext.Default.SystemOAuthSettingsInputDto, ct);
        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }
}
