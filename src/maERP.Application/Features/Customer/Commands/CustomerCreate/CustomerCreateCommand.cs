using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Customer.Commands.CustomerCreate;

public class CustomerCreateCommand : CustomerInputDto, IRequest<Result<int>>
{
}