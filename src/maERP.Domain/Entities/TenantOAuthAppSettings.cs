using System.ComponentModel.DataAnnotations;
using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

/// <summary>
/// Per-tenant Developer-App credentials for an OAuth-based SalesChannel provider (eBay, Amazon, ...).
/// Sibling of <see cref="TenantEmailSettings"/>: nullable fields fall back to the server-level
/// <c>OAuth.{Provider}.*</c> rows in <see cref="Setting"/> when not set.
///
/// One row per <c>(TenantId, Provider)</c> tuple — adding a new provider does not require an EF
/// migration, just a new row.
/// </summary>
public class TenantOAuthAppSettings : BaseEntityWithoutTenant, IBaseEntityWithoutTenant
{
    [Required]
    public Guid TenantId { get; set; }

    public Tenant? Tenant { get; set; }

    /// <summary>
    /// OAuth provider — reuses <see cref="SalesChannelType"/> so every connected channel-type
    /// can have its own per-tenant developer app.
    /// </summary>
    [Required]
    public SalesChannelType Provider { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;

    /// <summary>OAuth client id (eBay App ID, Amazon LWA client id, ...).</summary>
    [MaxLength(255)]
    public string? ClientId { get; set; }

    /// <summary>OAuth client secret. Encrypted at rest via <c>EncryptedStringConverter</c>.</summary>
    [MaxLength(4096)]
    public string? ClientSecret { get; set; }

    /// <summary>Browser redirect URI registered with the provider (Amazon LWA, ...).</summary>
    [MaxLength(500)]
    public string? RedirectUri { get; set; }

    /// <summary>eBay-specific RuName alias — passed both as authorize <c>redirect_uri</c> and at token-exchange.</summary>
    [MaxLength(255)]
    public string? RuName { get; set; }

    /// <summary>Optional override of system-level scopes (space-separated).</summary>
    [MaxLength(2000)]
    public string? Scopes { get; set; }

    /// <summary>
    /// Tenant-level sandbox switch. <c>null</c> means: fall back to the system-level
    /// <c>OAuth.{Provider}.UseSandbox</c> setting.
    /// </summary>
    public bool? UseSandbox { get; set; }
}
