using maERP.Application.Mediator;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.Account.Commands.UpdateCurrentUser;

public class UpdateCurrentUserCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
