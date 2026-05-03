using System.Collections.Concurrent;
using System.Net.Http.Json;
using maERP.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Models.Shopware6;

/// <summary>
/// OAuth client_credentials exchange for the Shopware 6 Admin API.
/// Tokens are short-lived (~10 minutes). Cached in memory per channel until 60s before expiry,
/// with per-channel locking to coalesce concurrent refresh attempts.
/// </summary>
public sealed class Shopware6AuthHelper
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<Shopware6AuthHelper> _logger;
    private readonly ConcurrentDictionary<Guid, CachedToken> _cache = new();
    private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _locks = new();

    public Shopware6AuthHelper(IHttpClientFactory httpClientFactory, ILogger<Shopware6AuthHelper> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<string> GetAccessTokenAsync(SalesChannel salesChannel, Shopware6ChannelConfig config, CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(salesChannel.Id, out var cached) && cached.ExpiresAt > DateTime.UtcNow.AddSeconds(60))
        {
            return cached.AccessToken;
        }

        var gate = _locks.GetOrAdd(salesChannel.Id, _ => new SemaphoreSlim(1, 1));
        await gate.WaitAsync(cancellationToken);
        try
        {
            if (_cache.TryGetValue(salesChannel.Id, out cached) && cached.ExpiresAt > DateTime.UtcNow.AddSeconds(60))
            {
                return cached.AccessToken;
            }

            var http = _httpClientFactory.CreateClient("shopware6");
            var tokenUrl = salesChannel.Url.TrimEnd('/') + "/api/oauth/token";
            var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = config.ApiClientId,
                ["client_secret"] = config.ApiClientSecret,
            });

            var response = await http.PostAsync(tokenUrl, formContent, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("Shopware6 OAuth token exchange failed: {Status} {Body}", (int)response.StatusCode, body);
                throw new InvalidOperationException($"Shopware6 OAuth token exchange failed: {(int)response.StatusCode}");
            }

            var token = await response.Content.ReadFromJsonAsync<Sw6OAuthTokenResponse>(cancellationToken: cancellationToken)
                ?? throw new InvalidOperationException("Empty Shopware6 OAuth token response");

            var entry = new CachedToken(token.AccessToken, DateTime.UtcNow.AddSeconds(Math.Max(60, token.ExpiresIn)));
            _cache[salesChannel.Id] = entry;
            return entry.AccessToken;
        }
        finally
        {
            gate.Release();
        }
    }

    private sealed record CachedToken(string AccessToken, DateTime ExpiresAt);
}
