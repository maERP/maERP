using System.ComponentModel.DataAnnotations;
using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

/// <summary>
/// Stores email configuration settings for each tenant.
/// Fields left null/empty fall back to the server-level configuration in <see cref="Setting"/>.
/// </summary>
public class TenantEmailSettings : BaseEntityWithoutTenant, IBaseEntityWithoutTenant
{
    [Required]
    public Guid TenantId { get; set; }

    public Tenant? Tenant { get; set; }

    [Required]
    public EmailProviderType ProviderType { get; set; } = EmailProviderType.Smtp;

    [Required]
    public bool IsActive { get; set; } = true;

    // SMTP Settings
    [MaxLength(255)]
    public string? SmtpHost { get; set; }

    public int? SmtpPort { get; set; }

    [MaxLength(255)]
    public string? SmtpUsername { get; set; }

    [MaxLength(500)]
    public string? SmtpPassword { get; set; }

    public bool? SmtpEnableSsl { get; set; } = true;

    // Microsoft 365 (Graph API, client credentials / app-only)
    [MaxLength(255)]
    public string? M365TenantId { get; set; }

    [MaxLength(255)]
    public string? M365ClientId { get; set; }

    [MaxLength(500)]
    public string? M365ClientSecret { get; set; }

    [MaxLength(255)]
    [EmailAddress]
    public string? M365SenderAddress { get; set; }

    // From Address Settings (optional — fall back to server defaults when null/empty)
    [MaxLength(255)]
    [EmailAddress]
    public string? FromAddress { get; set; }

    [MaxLength(255)]
    public string? FromName { get; set; }

    // Reply-To Address (optional)
    [MaxLength(255)]
    [EmailAddress]
    public string? ReplyToAddress { get; set; }

    [MaxLength(255)]
    public string? ReplyToName { get; set; }
}
