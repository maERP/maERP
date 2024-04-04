using maERP.Application.Models.Email;

namespace maERP.Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailMessage email);
}
