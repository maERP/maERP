using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Models.Email;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace maERP.Infrastructure.EmailService.Providers;

public class SendGridEmailProvider : IEmailProvider
{
    private readonly ILogger<SendGridEmailProvider> _logger;

    public SendGridEmailProvider(ILogger<SendGridEmailProvider> logger)
    {
        _logger = logger;
    }

    public async Task<bool> SendAsync(EmailMessage email, EmailSettings settings)
    {
        try
        {
            if (string.IsNullOrEmpty(settings.ApiKey))
            {
                _logger.LogError("SendGrid API Key is not configured");
                return false;
            }

            var client = new SendGridClient(settings.ApiKey);

            var from = new EmailAddress(settings.FromAddress, settings.FromName);
            var to = new EmailAddress(email.To, email.ToName ?? email.To);

            var msg = MailHelper.CreateSingleEmail(
                from,
                to,
                email.Subject,
                email.IsHtml ? null : email.Body,
                email.IsHtml ? email.Body : null
            );

            // Add CC recipients
            if (email.Cc.Any())
            {
                msg.AddCcs(email.Cc.Select(cc => new EmailAddress(cc)).ToList());
            }

            // Add BCC recipients
            if (email.Bcc.Any())
            {
                msg.AddBccs(email.Bcc.Select(bcc => new EmailAddress(bcc)).ToList());
            }

            // Add Reply-To if configured
            if (!string.IsNullOrEmpty(settings.ReplyToAddress))
            {
                msg.SetReplyTo(new EmailAddress(settings.ReplyToAddress, settings.ReplyToName ?? settings.ReplyToAddress));
            }

            // Add attachments
            foreach (var attachment in email.Attachments)
            {
                var base64Content = Convert.ToBase64String(attachment.Content);
                msg.AddAttachment(attachment.FileName, base64Content, attachment.ContentType);
            }

            // Add custom headers
            foreach (var header in email.Headers)
            {
                msg.AddHeader(header.Key, header.Value);
            }

            var response = await client.SendEmailAsync(msg);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Email sent successfully via SendGrid to {To}", email.To);
                return true;
            }
            else
            {
                var body = await response.Body.ReadAsStringAsync();
                _logger.LogError("SendGrid returned non-success status code: {StatusCode}. Body: {Body}",
                    response.StatusCode, body);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email via SendGrid to {To}. Error: {Error}",
                email.To, ex.Message);
            return false;
        }
    }
}
