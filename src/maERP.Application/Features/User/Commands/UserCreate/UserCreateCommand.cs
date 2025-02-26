using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.User.Commands.UserCreate;

public class UserCreateCommand : IRequest<Result<string>>
{
    public string Email { get; set; } =  string.Empty;  
    public string Password { get; set; } =  string.Empty;
}