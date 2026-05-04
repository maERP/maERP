using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.SystemOAuthSettings;

/// <summary>
/// Read projection of the system-wide <c>OAuth.{Provider}.*</c> Setting rows for a single
/// provider. Bundles the 8 keys the UI needs into one DTO so the Client doesn't have to make
/// 8 round-trips. <c>ClientSecret</c> itself is never returned — <see cref="HasClientSecret"/>
/// reports presence only.
/// </summary>
public class SystemOAuthSettingsDto
{
    public SalesChannelType Provider { get; set; }
    public string? ClientId { get; set; }
    public bool HasClientSecret { get; set; }
    public string? RuName { get; set; }
    public string? RedirectUri { get; set; }
    public string? AuthorizationEndpoint { get; set; }
    public string? TokenEndpoint { get; set; }
    public string? Scopes { get; set; }
    public bool UseSandbox { get; set; }
    public string? PublicBaseUrl { get; set; }
}

/// <summary>
/// Write payload for system OAuth settings. <c>ClientSecret</c> semantics: <c>null</c> = keep
/// existing value (UI omits when not rotated); non-null = replace stored ciphertext.
/// </summary>
public class SystemOAuthSettingsInputDto
{
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? RuName { get; set; }
    public string? RedirectUri { get; set; }
    public string? AuthorizationEndpoint { get; set; }
    public string? TokenEndpoint { get; set; }
    public string? Scopes { get; set; }
    public bool UseSandbox { get; set; }
    public string? PublicBaseUrl { get; set; }
}
