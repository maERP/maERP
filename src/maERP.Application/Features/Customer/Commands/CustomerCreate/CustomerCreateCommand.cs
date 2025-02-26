using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Customer.Commands.CustomerCreate;

public class CustomerCreateCommand : IRequest<Result<int>>
{
    public string Firstname { get; set; } = string.Empty;  
    public string Lastname { get; set; } = string.Empty;
}