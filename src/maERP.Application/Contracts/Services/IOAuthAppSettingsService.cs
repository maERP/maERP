using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Application.Contracts.Services;

/// <summary>
/// Where the resolved credentials came from. Useful for diagnostics — the connector logs it
/// alongside its first call so a misconfigured tenant override is visible in Grafana.
/// </summary>
public enum OAuthCredentialSource
{
    System,
    Tenant,
    Mixed,
}

/// <summary>
/// Effective Developer-App credentials for a single (tenant, provider) combination, with
/// per-field merge of the tenant override on top of the system <c>Setting</c> rows.
/// </summary>
public sealed record OAuthAppCredentials(
    string ClientId,
    string ClientSecret,
    string AuthorizationEndpoint,
    string TokenEndpoint,
    string RedirectUri,
    string? RuName,
    string Scopes,
    bool UseSandbox,
    OAuthCredentialSource Source);

/// <summary>
/// Single source of truth for OAuth Developer-App credentials. Both the OAuth-flow controller
/// (building the authorize-URL, exchanging the code) and the connector auth-helpers (refreshing
/// access tokens) consume this contract — the field-by-field tenant→system fallback rule lives
/// here only.
/// </summary>
public interface IOAuthAppSettingsService
{
    Task<Result<OAuthAppCredentials>> GetEffectiveCredentialsAsync(
        Guid tenantId,
        SalesChannelType provider,
        CancellationToken cancellationToken = default);
}
