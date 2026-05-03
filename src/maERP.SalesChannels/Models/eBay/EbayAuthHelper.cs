using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Models.eBay;

/// <summary>
/// eBay OAuth refresh-token flow. Replaces the legacy client-credentials flow which only worked
/// for the public Browse API — Sell APIs require a user-token via Authorization Code, with the
/// refresh token persisted on <see cref="SalesChannel.RefreshToken"/> after the OAuth flow runs.
///
/// App credentials (client_id / client_secret) are read from <see cref="IOAuthAppSettingsService"/>
/// — the tenant-level row overrides the system-level <see cref="Setting"/> rows.
///
/// Tokens are cached in memory per channel until 60s before expiry. A per-channel
/// <see cref="SemaphoreSlim"/> coalesces concurrent refresh attempts.
/// </summary>
public sealed class EbayAuthHelper
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<EbayAuthHelper> _logger;
    private readonly ConcurrentDictionary<Guid, CachedToken> _cache = new();
    private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _locks = new();

    public EbayAuthHelper(
        IHttpClientFactory httpClientFactory,
        IServiceScopeFactory scopeFactory,
        ILogger<EbayAuthHelper> logger)
    {
        _httpClientFactory = httpClientFactory;
        // Singleton helper holds the token cache; resolve the scoped IOAuthAppSettingsService
        // (which depends on the per-request DbContext) via scope factory on each refresh.
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    /// <summary>
    /// Returns a current access token for the given channel. Throws if the channel has no
    /// refresh token yet (the OAuth flow has not been completed) or if app credentials are
    /// missing in both tenant and system settings.
    /// </summary>
    public async Task<string> GetAccessTokenAsync(SalesChannel salesChannel, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(salesChannel.RefreshToken))
        {
            throw new InvalidOperationException(
                $"eBay SalesChannel {salesChannel.Id}: complete the OAuth flow first (no refresh_token).");
        }
        if (!salesChannel.TenantId.HasValue)
        {
            throw new InvalidOperationException(
                $"eBay SalesChannel {salesChannel.Id}: tenant id is missing.");
        }

        if (_cache.TryGetValue(salesChannel.Id, out var cached) && cached.ExpiresAt > DateTime.UtcNow.AddSeconds(60))
        {
            return cached.AccessToken;
        }

        var gate = _locks.GetOrAdd(salesChannel.Id, _ => new SemaphoreSlim(1, 1));
        await gate.WaitAsync(cancellationToken);
        try
        {
            // Re-check after acquiring the lock — another caller may have refreshed for us.
            if (_cache.TryGetValue(salesChannel.Id, out cached) && cached.ExpiresAt > DateTime.UtcNow.AddSeconds(60))
            {
                return cached.AccessToken;
            }

            await using var scope = _scopeFactory.CreateAsyncScope();
            var oauthAppSettings = scope.ServiceProvider.GetRequiredService<IOAuthAppSettingsService>();
            var credsResult = await oauthAppSettings.GetEffectiveCredentialsAsync(
                salesChannel.TenantId.Value, SalesChannelType.eBay, cancellationToken);
            if (!credsResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join("; ", credsResult.Messages));
            }
            var creds = credsResult.Data;

            var http = _httpClientFactory.CreateClient("ebay");
            var request = new HttpRequestMessage(HttpMethod.Post, creds.TokenEndpoint)
            {
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(Encoding.UTF8.GetBytes($"{creds.ClientId}:{creds.ClientSecret}"))),
                },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "refresh_token",
                    ["refresh_token"] = salesChannel.RefreshToken,
                    ["scope"] = creds.Scopes,
                }),
            };

            var response = await http.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("eBay token refresh HTTP {Status}: {Body}", (int)response.StatusCode, body);
                throw new InvalidOperationException($"eBay token refresh failed: {(int)response.StatusCode}");
            }

            var token = await response.Content.ReadFromJsonAsync<EbayTokenResponse>(cancellationToken: cancellationToken)
                ?? throw new InvalidOperationException("Empty eBay token response.");
            if (string.IsNullOrEmpty(token.AccessToken))
            {
                throw new InvalidOperationException("eBay token response missing access_token.");
            }

            var entry = new CachedToken(
                token.AccessToken,
                DateTime.UtcNow.AddSeconds(Math.Max(60, token.ExpiresIn)));
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

public class EbayTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = string.Empty;
}
