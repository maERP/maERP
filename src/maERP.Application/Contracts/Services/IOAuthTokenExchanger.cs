using maERP.Domain.Enums;

namespace maERP.Application.Contracts.Services;

/// <summary>
/// Wraps the HTTP call to the provider's token endpoint that swaps an authorization code (or
/// refresh token) for a fresh access + refresh token pair. Lives behind an abstraction so the
/// Application layer does not need an <c>IHttpClientFactory</c> reference.
/// </summary>
public interface IOAuthTokenExchanger
{
    Task<OAuthTokenExchangeResult> ExchangeAuthorizationCodeAsync(
        SalesChannelType provider,
        OAuthAppCredentials credentials,
        string authorizationCode,
        string redirectUri,
        CancellationToken cancellationToken = default);
}

public sealed record OAuthTokenExchangeResult(
    string AccessToken,
    string? RefreshToken,
    int ExpiresInSeconds);
