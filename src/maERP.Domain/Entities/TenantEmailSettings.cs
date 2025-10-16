using System.ComponentModel.DataAnnotations;
using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

/// <summary>
/// Stores email configuration settings for each tenant
/// </summary>
public class TenantEmailSettings : BaseEntityWithoutTenant, IBaseEntityWithoutTenant
{
    /// <summary>
    /// The tenant this email configuration belongs to
    /// </summary>
    [Required]
    public Guid TenantId { get; set; }

    /// <summary>
    /// Navigation property to the tenant
    /// </summary>
    public Tenant? Tenant { get; set; }

    /// <summary>
    /// Type of email provider (SMTP, SendGrid, etc.)
    /// </summary>
    [Required]
    public EmailProviderType ProviderType { get; set; } = EmailProviderType.Smtp;

    /// <summary>
    /// Whether this email configuration is active
    /// </summary>
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

    // API Key for third-party providers (SendGrid, Mailgun, etc.)
    [MaxLength(500)]
    public string? ApiKey { get; set; }

    // From Address Settings
    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string FromAddress { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string FromName { get; set; } = string.Empty;

    // Reply-To Address (optional)
    [MaxLength(255)]
    [EmailAddress]
    public string? ReplyToAddress { get; set; }

    [MaxLength(255)]
    public string? ReplyToName { get; set; }

    // Additional Configuration (JSON)
    public string? AdditionalConfiguration { get; set; }
}
