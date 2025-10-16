using maERP.Application.Models.Email;

namespace maERP.Application.Contracts.Infrastructure;

public interface IEmailService
{
    /// <summary>
    /// Sends an email using the tenant-specific configuration
    /// </summary>
    Task<bool> SendEmailAsync(EmailMessage email, Guid? tenantId = null);

    /// <summary>
    /// Sends a password reset email
    /// </summary>
    Task<bool> SendPasswordResetEmailAsync(string toEmail, string toName, string resetToken, Guid? tenantId = null);

    /// <summary>
    /// Sends a welcome email
    /// </summary>
    Task<bool> SendWelcomeEmailAsync(string toEmail, string toName, Guid? tenantId = null);
}

public interface IEmailProvider
{
    /// <summary>
    /// Sends an email using the specific provider implementation
    /// </summary>
    Task<bool> SendAsync(EmailMessage email, EmailSettings settings);
}
