using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.TenantEmailSettings;

/// <summary>
/// Detail DTO for the tenant-level email configuration.
/// Returned values mirror what is stored on the tenant — empty fields indicate inheritance from the server-level <see cref="maERP.Domain.Entities.Setting"/>.
/// </summary>
public class TenantEmailSettingsDetailDto
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }

    public EmailProviderType ProviderType { get; set; }
    public bool IsActive { get; set; }

    // SMTP
    public string? SmtpHost { get; set; }
    public int? SmtpPort { get; set; }
    public string? SmtpUsername { get; set; }

    /// <summary>
    /// Indicates whether an SMTP password is stored on the tenant. The actual value is never returned.
    /// </summary>
    public bool SmtpPasswordIsSet { get; set; }

    public bool? SmtpEnableSsl { get; set; }

    // Microsoft 365
    public string? M365TenantId { get; set; }
    public string? M365ClientId { get; set; }

    /// <summary>
    /// Indicates whether a Microsoft 365 client secret is stored on the tenant. The actual value is never returned.
    /// </summary>
    public bool M365ClientSecretIsSet { get; set; }

    public string? M365SenderAddress { get; set; }

    // From / Reply-To
    public string? FromAddress { get; set; }
    public string? FromName { get; set; }
    public string? ReplyToAddress { get; set; }
    public string? ReplyToName { get; set; }

    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}
