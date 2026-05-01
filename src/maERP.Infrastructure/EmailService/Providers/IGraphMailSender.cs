using maERP.Application.Models.Email;

namespace maERP.Infrastructure.EmailService.Providers;

/// <summary>
/// Thin abstraction over the Microsoft Graph SendMail call so the Microsoft 365 provider
/// can be unit-tested without a real Graph client.
/// </summary>
public interface IGraphMailSender
{
    Task SendAsync(EmailSettings settings, EmailMessage email, CancellationToken cancellationToken = default);
}
