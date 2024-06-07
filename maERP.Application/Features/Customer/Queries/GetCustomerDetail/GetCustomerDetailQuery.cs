using MediatR;

namespace maERP.Application.Features.Customer.Queries.GetCustomerDetail;

public class GetCustomerDetailQuery : IRequest<GetCustomerDetailResponse>
{
    public int Id { get; set; }
}