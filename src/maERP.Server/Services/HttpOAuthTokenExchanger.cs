using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using maERP.Application.Contracts.Services;
using maERP.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace maERP.Server.Services;

/// <summary>
/// HTTP-backed implementation of <see cref="IOAuthTokenExchanger"/>. Routes through named
/// <c>IHttpClientFactory</c> clients (<c>"ebay"</c>, <c>"amazon-lwa"</c>) so the existing Polly
/// retry/circuit-breaker policies kick in without extra wiring.
/// </summary>
public sealed class HttpOAuthTokenExchanger : IOAuthTokenExchanger
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HttpOAuthTokenExchanger> _logger;

    public HttpOAuthTokenExchanger(IHttpClientFactory httpClientFactory, ILogger<HttpOAuthTokenExchanger> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<OAuthTokenExchangeResult> ExchangeAuthorizationCodeAsync(
        SalesChannelType provider,
        OAuthAppCredentials credentials,
        string authorizationCode,
        string redirectUri,
        CancellationToken cancellationToken = default)
    {
        var clientName = provider switch
        {
            SalesChannelType.Amazon => "amazon-lwa",
            SalesChannelType.eBay => "ebay",
            _ => "default",
        };
        var http = _httpClientFactory.CreateClient(clientName);

        var form = new Dictionary<string, string>
        {
            ["grant_type"] = "authorization_code",
            ["code"] = authorizationCode,
            ["redirect_uri"] = redirectUri,
        };

        var request = new HttpRequestMessage(HttpMethod.Post, credentials.TokenEndpoint);

        if (provider == SalesChannelType.eBay)
        {
            // eBay: HTTP Basic with client_id:client_secret on the token endpoint.
            var basic = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{credentials.ClientId}:{credentials.ClientSecret}"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", basic);
        }
        else
        {
            // Amazon LWA: client_id + client_secret as form fields.
            form["client_id"] = credentials.ClientId;
            form["client_secret"] = credentials.ClientSecret;
        }

        request.Content = new FormUrlEncodedContent(form);
        var response = await http.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("OAuth token exchange HTTP {Status}: {Body}", (int)response.StatusCode, Truncate(body, 500));
            throw new InvalidOperationException($"HTTP {(int)response.StatusCode}: {Truncate(body, 300)}");
        }

        var token = await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Empty token response.");
        if (string.IsNullOrEmpty(token.AccessToken))
        {
            throw new InvalidOperationException("Token response missing access_token.");
        }

        return new OAuthTokenExchangeResult(token.AccessToken, token.RefreshToken, token.ExpiresInSeconds);
    }

    private static string Truncate(string value, int max) =>
        string.IsNullOrEmpty(value) || value.Length <= max ? value ?? string.Empty : value[..max];

    private sealed class TokenResponse
    {
        [JsonPropertyName("access_token")] public string AccessToken { get; set; } = string.Empty;
        [JsonPropertyName("refresh_token")] public string? RefreshToken { get; set; }
        [JsonPropertyName("token_type")] public string? TokenType { get; set; }
        [JsonPropertyName("expires_in")] public int ExpiresInSeconds { get; set; }
    }
}
