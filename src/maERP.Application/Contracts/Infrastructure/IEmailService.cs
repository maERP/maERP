using maERP.Application.Models.Email;

namespace maERP.Application.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> SendEmail(EmailMessage email);
}
