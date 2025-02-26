using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Customer.Queries.CustomerDetail;

public class CustomerDetailQuery : IRequest<Result<CustomerDetailDto>>
{
    public int Id { get; set; }
}