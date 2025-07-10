using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Customer.Commands.CustomerUpdate;

public class CustomerUpdateCommand : CustomerInputDto, IRequest<Result<int>>
{
}