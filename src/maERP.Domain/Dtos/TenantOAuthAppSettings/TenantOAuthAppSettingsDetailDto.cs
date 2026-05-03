using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.TenantOAuthAppSettings;

/// <summary>
/// Detail projection — same shape as list plus Scopes. <see cref="ClientSecret"/> is always
/// returned empty; <see cref="HasClientSecret"/> indicates whether one is stored. UI must use
/// a placeholder ("***" if HasClientSecret, else empty) and only PUT a non-empty value when
/// the user actually rotates the secret.
/// </summary>
public class TenantOAuthAppSettingsDetailDto
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public SalesChannelType Provider { get; set; }
    public bool IsActive { get; set; }
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; } = string.Empty;
    public bool HasClientSecret { get; set; }
    public string? RedirectUri { get; set; }
    public string? RuName { get; set; }
    public string? Scopes { get; set; }
    public bool? UseSandbox { get; set; }
}
