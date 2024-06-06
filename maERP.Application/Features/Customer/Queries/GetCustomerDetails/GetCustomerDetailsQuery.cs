using maERP.Application.Dtos.Customer;
using MediatR;

namespace maERP.Application.Features.Customer.Queries.GetCustomerDetailQuery;

public class GetCustomerDetailsQuery : IRequest<CustomerDetailDto>
{
    public int Id { get; set; }
}