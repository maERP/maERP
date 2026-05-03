using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.TenantOAuthAppSettings;

/// <summary>
/// Summary projection for the list view. Secrets are never included — <see cref="HasClientSecret"/>
/// reports presence only.
/// </summary>
public class TenantOAuthAppSettingsListDto
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public SalesChannelType Provider { get; set; }
    public bool IsActive { get; set; }
    public string? ClientId { get; set; }
    public bool HasClientSecret { get; set; }
    public string? RedirectUri { get; set; }
    public string? RuName { get; set; }
    public bool? UseSandbox { get; set; }
}
