using maERP.Application.Dtos.Customer;
using MediatR;

namespace maERP.Application.Features.Customer.Queries.GetCustomerDetailQuery;

public class GetCustomerDetailQuery : IRequest<CustomerDetailDto>
{
    public int Id { get; set; }
}