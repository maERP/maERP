using MediatR;

namespace maERP.Application.Features.Customer.Queries.CustomerDetail;

public class CustomerDetailQuery : IRequest<CustomerDetailResponse>
{
    public int Id { get; set; }
}