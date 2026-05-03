using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.TenantOAuthAppSettings;

/// <summary>
/// Upsert payload. <see cref="ClientSecret"/> semantics: <c>null</c> → keep existing value;
/// non-null (incl. empty) → replace stored value with the input. UI must therefore omit
/// <see cref="ClientSecret"/> from the PATCH body when the user did not type a new secret.
/// </summary>
public class TenantOAuthAppSettingsInputDto
{
    public SalesChannelType Provider { get; set; }
    public bool IsActive { get; set; } = true;
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? RedirectUri { get; set; }
    public string? RuName { get; set; }
    public string? Scopes { get; set; }
    public bool? UseSandbox { get; set; }
}
