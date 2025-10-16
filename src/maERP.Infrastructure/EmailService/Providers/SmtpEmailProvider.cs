using System.Net;
using System.Net.Mail;
using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Models.Email;
using Microsoft.Extensions.Logging;

namespace maERP.Infrastructure.EmailService.Providers;

public class SmtpEmailProvider : IEmailProvider
{
    private readonly ILogger<SmtpEmailProvider> _logger;

    public SmtpEmailProvider(ILogger<SmtpEmailProvider> logger)
    {
        _logger = logger;
    }

    public async Task<bool> SendAsync(EmailMessage email, EmailSettings settings)
    {
        try
        {
            if (string.IsNullOrEmpty(settings.SmtpHost) || !settings.SmtpPort.HasValue)
            {
                _logger.LogError("SMTP configuration is incomplete. Host: {Host}, Port: {Port}",
                    settings.SmtpHost, settings.SmtpPort);
                return false;
            }

            using var smtpClient = new SmtpClient(settings.SmtpHost, settings.SmtpPort.Value)
            {
                EnableSsl = settings.SmtpEnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            if (!string.IsNullOrEmpty(settings.SmtpUsername) && !string.IsNullOrEmpty(settings.SmtpPassword))
            {
                smtpClient.Credentials = new NetworkCredential(settings.SmtpUsername, settings.SmtpPassword);
            }

            using var message = new MailMessage
            {
                From = new MailAddress(settings.FromAddress, settings.FromName),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = email.IsHtml
            };

            message.To.Add(new MailAddress(email.To, email.ToName ?? email.To));

            // Add CC recipients
            foreach (var cc in email.Cc)
            {
                message.CC.Add(new MailAddress(cc));
            }

            // Add BCC recipients
            foreach (var bcc in email.Bcc)
            {
                message.Bcc.Add(new MailAddress(bcc));
            }

            // Add Reply-To if configured
            if (!string.IsNullOrEmpty(settings.ReplyToAddress))
            {
                message.ReplyToList.Add(new MailAddress(settings.ReplyToAddress, settings.ReplyToName ?? settings.ReplyToAddress));
            }

            // Add attachments
            foreach (var attachment in email.Attachments)
            {
                var stream = new MemoryStream(attachment.Content);
                message.Attachments.Add(new Attachment(stream, attachment.FileName, attachment.ContentType));
            }

            // Add custom headers
            foreach (var header in email.Headers)
            {
                message.Headers.Add(header.Key, header.Value);
            }

            await smtpClient.SendMailAsync(message);

            _logger.LogInformation("Email sent successfully via SMTP to {To}", email.To);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email via SMTP to {To}. Error: {Error}",
                email.To, ex.Message);
            return false;
        }
    }
}
