using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Models.Email;
using maERP.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace maERP.Infrastructure.EmailService.Providers;

/// <summary>
/// Microsoft 365 email provider. Uses the Microsoft Graph SendMail endpoint with
/// app-only client credentials (Mail.Send application permission).
/// </summary>
public class Microsoft365EmailProvider : IEmailProvider
{
    private readonly IGraphMailSender _graphMailSender;
    private readonly ILogger<Microsoft365EmailProvider> _logger;

    public Microsoft365EmailProvider(
        IGraphMailSender graphMailSender,
        ILogger<Microsoft365EmailProvider> logger)
    {
        _graphMailSender = graphMailSender;
        _logger = logger;
    }

    public EmailProviderType ProviderType => EmailProviderType.Microsoft365;

    public async Task<bool> SendAsync(EmailMessage email, EmailSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.M365TenantId) ||
            string.IsNullOrWhiteSpace(settings.M365ClientId) ||
            string.IsNullOrWhiteSpace(settings.M365ClientSecret))
        {
            _logger.LogError("Microsoft 365 configuration is incomplete (TenantId/ClientId/ClientSecret).");
            return false;
        }

        var senderAddress = string.IsNullOrWhiteSpace(settings.M365SenderAddress)
            ? settings.FromAddress
            : settings.M365SenderAddress;

        if (string.IsNullOrWhiteSpace(senderAddress))
        {
            _logger.LogError("Microsoft 365 configuration is missing a sender address (M365SenderAddress or FromAddress).");
            return false;
        }

        try
        {
            await _graphMailSender.SendAsync(settings, email);
            _logger.LogInformation("Email sent successfully via Microsoft 365 to {To}", email.To);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email via Microsoft 365 to {To}. Error: {Error}",
                email.To, ex.Message);
            return false;
        }
    }
}
