using MediatR;

namespace maERP.Application.Features.Customer.Queries.GetCustomers;

public record GetCustomersQuery : IRequest<List<GetCustomersResponse>>;