using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.User.Commands.UserDelete;

public class UserDeleteCommand : IRequest<Result<string>>
{
    public string Id { get; set; } = string.Empty;
}