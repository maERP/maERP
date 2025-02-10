using maERP.Domain.Dtos.Customer;
using MediatR;

namespace maERP.Application.Features.Customer.Queries.CustomerDetail;

public class CustomerDetailQuery : IRequest<CustomerDetailDto>
{
    public int Id { get; set; }
}