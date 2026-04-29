using System.Net.Http.Json;
using maERP.Client.Core.Json;
using maERP.Domain.Dtos.ServerInfo;

namespace maERP.Client.Features.Auth.Services;

public class ServerInfoService : IServerInfoService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ServerInfoService> _logger;

    public ServerInfoService(IHttpClientFactory httpClientFactory, ILogger<ServerInfoService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<ServerInfoResponseDto?> GetServerInfoAsync(string serverUrl, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(serverUrl))
        {
            return null;
        }

        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(serverUrl.TrimEnd('/'));
            httpClient.Timeout = TimeSpan.FromSeconds(5);

            var response = await httpClient.GetAsync("/api/v1/server-info", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Server-info request returned {StatusCode}", response.StatusCode);
                return null;
            }

            return await response.Content.ReadFromJsonAsync(
                AppJsonSerializerContext.Default.ServerInfoResponseDto, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to fetch server-info from {ServerUrl}", serverUrl);
            return null;
        }
    }
}
