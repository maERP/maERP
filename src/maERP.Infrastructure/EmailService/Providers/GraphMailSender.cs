using Azure.Identity;
using maERP.Application.Models.Email;
using Microsoft.Graph;
using Microsoft.Graph.Users.Item.SendMail;
using GraphModels = Microsoft.Graph.Models;

namespace maERP.Infrastructure.EmailService.Providers;

/// <summary>
/// Default <see cref="IGraphMailSender"/> implementation that talks to Microsoft Graph
/// using app-only client credentials (Mail.Send application permission).
/// </summary>
public class GraphMailSender : IGraphMailSender
{
    public async Task SendAsync(EmailSettings settings, EmailMessage email, CancellationToken cancellationToken = default)
    {
        var credential = new ClientSecretCredential(
            settings.M365TenantId,
            settings.M365ClientId,
            settings.M365ClientSecret);

        var graphClient = new GraphServiceClient(credential, new[] { "https://graph.microsoft.com/.default" });

        var senderAddress = string.IsNullOrWhiteSpace(settings.M365SenderAddress)
            ? settings.FromAddress
            : settings.M365SenderAddress;

        var message = new GraphModels.Message
        {
            Subject = email.Subject,
            Body = new GraphModels.ItemBody
            {
                ContentType = email.IsHtml ? GraphModels.BodyType.Html : GraphModels.BodyType.Text,
                Content = email.Body
            },
            ToRecipients = new List<GraphModels.Recipient>
            {
                new()
                {
                    EmailAddress = new GraphModels.EmailAddress
                    {
                        Address = email.To,
                        Name = email.ToName ?? email.To
                    }
                }
            },
            From = new GraphModels.Recipient
            {
                EmailAddress = new GraphModels.EmailAddress
                {
                    Address = settings.FromAddress,
                    Name = settings.FromName
                }
            }
        };

        if (email.Cc.Count > 0)
        {
            message.CcRecipients = email.Cc
                .Select(addr => new GraphModels.Recipient { EmailAddress = new GraphModels.EmailAddress { Address = addr } })
                .ToList();
        }

        if (email.Bcc.Count > 0)
        {
            message.BccRecipients = email.Bcc
                .Select(addr => new GraphModels.Recipient { EmailAddress = new GraphModels.EmailAddress { Address = addr } })
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(settings.ReplyToAddress))
        {
            message.ReplyTo = new List<GraphModels.Recipient>
            {
                new()
                {
                    EmailAddress = new GraphModels.EmailAddress
                    {
                        Address = settings.ReplyToAddress,
                        Name = settings.ReplyToName ?? settings.ReplyToAddress
                    }
                }
            };
        }

        if (email.Attachments.Count > 0)
        {
            message.Attachments = email.Attachments
                .Select(a => (GraphModels.Attachment)new GraphModels.FileAttachment
                {
                    OdataType = "#microsoft.graph.fileAttachment",
                    Name = a.FileName,
                    ContentType = a.ContentType,
                    ContentBytes = a.Content
                })
                .ToList();
        }

        if (email.Headers.Count > 0)
        {
            message.InternetMessageHeaders = email.Headers
                .Select(h => new GraphModels.InternetMessageHeader { Name = h.Key, Value = h.Value })
                .ToList();
        }

        var requestBody = new SendMailPostRequestBody
        {
            Message = message,
            SaveToSentItems = false
        };

        await graphClient.Users[senderAddress]
            .SendMail
            .PostAsync(requestBody, cancellationToken: cancellationToken);
    }
}
