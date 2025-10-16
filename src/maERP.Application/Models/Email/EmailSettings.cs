using maERP.Domain.Enums;

namespace maERP.Application.Models.Email;

public class EmailSettings
{
    public EmailProviderType ProviderType { get; set; } = EmailProviderType.Smtp;

    // SMTP Settings
    public string? SmtpHost { get; set; }
    public int? SmtpPort { get; set; }
    public string? SmtpUsername { get; set; }
    public string? SmtpPassword { get; set; }
    public bool SmtpEnableSsl { get; set; } = true;

    // API Key for third-party providers
    public string? ApiKey { get; set; }

    // From Settings
    public string FromAddress { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;

    // Reply-To Settings
    public string? ReplyToAddress { get; set; }
    public string? ReplyToName { get; set; }
}