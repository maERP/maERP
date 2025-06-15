using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Customer.Commands.CustomerDelete;

public class CustomerDeleteCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
}