using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Customer.Commands.CustomerUpdate;

public class CustomerUpdateCommand : CustomerUpdateDto, IRequest<Result<int>>
{
}