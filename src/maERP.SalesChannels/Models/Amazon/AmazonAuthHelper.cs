using System.Collections.Concurrent;
using System.Net.Http.Json;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Models.Amazon;

/// <summary>
/// Login-with-Amazon (LWA) refresh-token → access-token exchange. SP-API since 2023 no longer
/// requires AWS SigV4 signing; the bearer access token alone authenticates regular calls.
///
/// App credentials (LWA <c>client_id</c>/<c>client_secret</c>) are resolved via
/// <see cref="IOAuthAppSettingsService"/> so the tenant override → system fallback rule applies.
/// Tokens are cached in memory per SalesChannel until ~60s before expiry; concurrent callers for
/// the same channel share a single in-flight refresh via a per-channel lock.
/// </summary>
public sealed class AmazonAuthHelper
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<AmazonAuthHelper> _logger;
    private readonly ConcurrentDictionary<Guid, CachedToken> _cache = new();
    private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _locks = new();

    public AmazonAuthHelper(
        IHttpClientFactory httpClientFactory,
        IServiceScopeFactory scopeFactory,
        ILogger<AmazonAuthHelper> logger)
    {
        _httpClientFactory = httpClientFactory;
        // Singleton helper holds the token cache; resolve the scoped IOAuthAppSettingsService
        // (per-request DbContext) via scope factory on each refresh.
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    /// <summary>
    /// Resolves a current access token for the given Amazon SalesChannel. Throws if the OAuth
    /// flow has not been completed (no refresh token) or app credentials are missing.
    /// </summary>
    public async Task<string> GetAccessTokenAsync(SalesChannel salesChannel, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(salesChannel.RefreshToken))
        {
            throw new InvalidOperationException(
                $"Amazon SalesChannel {salesChannel.Id} has no refresh token — complete the OAuth flow first.");
        }
        if (!salesChannel.TenantId.HasValue)
        {
            throw new InvalidOperationException(
                $"Amazon SalesChannel {salesChannel.Id}: tenant id is missing.");
        }

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

            await using var scope = _scopeFactory.CreateAsyncScope();
            var oauthAppSettings = scope.ServiceProvider.GetRequiredService<IOAuthAppSettingsService>();
            var credsResult = await oauthAppSettings.GetEffectiveCredentialsAsync(
                salesChannel.TenantId.Value, SalesChannelType.Amazon, cancellationToken);
            if (!credsResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join("; ", credsResult.Messages));
            }
            var creds = credsResult.Data;

            var http = _httpClientFactory.CreateClient("amazon-lwa");
            var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = salesChannel.RefreshToken,
                ["client_id"] = creds.ClientId,
                ["client_secret"] = creds.ClientSecret,
            });

            var response = await http.PostAsync(creds.TokenEndpoint, formContent, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("LWA token exchange failed: {Status} {Body}", (int)response.StatusCode, body);
                throw new InvalidOperationException($"LWA token exchange failed: {(int)response.StatusCode}");
            }

            var token = await response.Content.ReadFromJsonAsync<AmazonLwaTokenResponse>(cancellationToken: cancellationToken)
                ?? throw new InvalidOperationException("Empty LWA token response");

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
