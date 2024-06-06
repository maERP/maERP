using maERP.Application.Dtos.Customer;
using MediatR;

namespace maERP.Application.Features.Customer.Queries.GetCustomersQuery;

public record GetCustomersQuery : IRequest<List<CustomerListDto>>;