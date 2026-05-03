using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Services;

/// <summary>
/// Implements the field-by-field tenant→system fallback for OAuth Developer-App credentials.
/// Tenant-level row wins per non-empty field; missing fields fall back to system <c>Setting</c>
/// rows seeded by <c>SettingsInitializer</c>.
///
/// Reads happen with <c>IgnoreQueryFilters()</c> because the per-tenant settings table is
/// scoped via the explicit <c>TenantId</c> FK (sibling of <c>TenantEmailSettings</c>); the
/// service is used both inside tenant-scoped requests and from the OAuth callback which has
/// no tenant context yet.
/// </summary>
public sealed class OAuthAppSettingsService : IOAuthAppSettingsService
{
    private readonly ApplicationDbContext _context;
    private readonly ISettingsService _settings;

    public OAuthAppSettingsService(ApplicationDbContext context, ISettingsService settings)
    {
        _context = context;
        _settings = settings;
    }

    public async Task<Result<OAuthAppCredentials>> GetEffectiveCredentialsAsync(
        Guid tenantId,
        SalesChannelType provider,
        CancellationToken cancellationToken = default)
    {
        if (!IsSupportedProvider(provider))
        {
            return Result<OAuthAppCredentials>.Fail(
                ResultStatusCode.BadRequest,
                $"OAuth flow is not supported for provider {provider}.");
        }

        var prefix = provider.ToString(); // "eBay" / "Amazon" — matches the Setting key prefix in SettingsInitializer.
        var tenantRow = await _context.TenantOAuthAppSettings
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.TenantId == tenantId && s.Provider == provider, cancellationToken);

        var tenantActive = tenantRow is { IsActive: true };

        var clientId       = Pick(tenantActive ? tenantRow!.ClientId : null,
                                  await _settings.GetSettingValueAsync($"OAuth.{prefix}.ClientId"));
        var clientSecret   = Pick(tenantActive ? tenantRow!.ClientSecret : null,
                                  await _settings.GetEncryptedSettingValueAsync($"OAuth.{prefix}.ClientSecret"));
        var authzEndpoint  = Pick(null, // No tenant override — endpoints are vendor-fixed.
                                  await _settings.GetSettingValueAsync($"OAuth.{prefix}.AuthorizationEndpoint"));
        var tokenEndpoint  = Pick(null,
                                  await _settings.GetSettingValueAsync($"OAuth.{prefix}.TokenEndpoint"));
        var redirectUri    = Pick(tenantActive ? tenantRow!.RedirectUri : null,
                                  await _settings.GetSettingValueAsync($"OAuth.{prefix}.RedirectUri"));
        var ruName         = Pick(tenantActive ? tenantRow!.RuName : null,
                                  await _settings.GetSettingValueAsync($"OAuth.{prefix}.RuName"));
        var scopes         = Pick(tenantActive ? tenantRow!.Scopes : null,
                                  await _settings.GetSettingValueAsync($"OAuth.{prefix}.Scopes"));
        var useSandbox     = (tenantActive ? tenantRow!.UseSandbox : null)
                             ?? ParseBool(await _settings.GetSettingValueAsync($"OAuth.{prefix}.UseSandbox"));

        if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
        {
            return Result<OAuthAppCredentials>.Fail(
                ResultStatusCode.BadRequest,
                $"OAuth credentials for {provider} are not configured. " +
                "Set ClientId/ClientSecret either at tenant level (TenantOAuthAppSettings) " +
                "or system level (Setting rows OAuth.{provider}.ClientId/ClientSecret).");
        }

        var source = ComputeSource(tenantActive, tenantRow, clientId, clientSecret);

        return Result<OAuthAppCredentials>.Success(new OAuthAppCredentials(
            ClientId: clientId,
            ClientSecret: clientSecret,
            AuthorizationEndpoint: authzEndpoint,
            TokenEndpoint: tokenEndpoint,
            RedirectUri: redirectUri,
            RuName: string.IsNullOrEmpty(ruName) ? null : ruName,
            Scopes: scopes ?? string.Empty,
            UseSandbox: useSandbox,
            Source: source));
    }

    private static bool IsSupportedProvider(SalesChannelType provider) =>
        provider is SalesChannelType.eBay or SalesChannelType.Amazon;

    private static string Pick(string? tenantValue, string systemValue) =>
        !string.IsNullOrWhiteSpace(tenantValue) ? tenantValue! : (systemValue ?? string.Empty);

    private static bool ParseBool(string? value) =>
        bool.TryParse(value, out var parsed) && parsed;

    private static OAuthCredentialSource ComputeSource(
        bool tenantActive,
        TenantOAuthAppSettings? tenantRow,
        string clientId,
        string clientSecret)
    {
        if (!tenantActive || tenantRow is null) return OAuthCredentialSource.System;

        var allFromTenant =
            !string.IsNullOrWhiteSpace(tenantRow.ClientId) && tenantRow.ClientId == clientId &&
            !string.IsNullOrWhiteSpace(tenantRow.ClientSecret) && tenantRow.ClientSecret == clientSecret;

        var anyFromTenant =
            !string.IsNullOrWhiteSpace(tenantRow.ClientId) ||
            !string.IsNullOrWhiteSpace(tenantRow.ClientSecret) ||
            !string.IsNullOrWhiteSpace(tenantRow.RedirectUri) ||
            !string.IsNullOrWhiteSpace(tenantRow.RuName) ||
            !string.IsNullOrWhiteSpace(tenantRow.Scopes) ||
            tenantRow.UseSandbox.HasValue;

        return allFromTenant ? OAuthCredentialSource.Tenant
             : anyFromTenant ? OAuthCredentialSource.Mixed
             : OAuthCredentialSource.System;
    }
}
