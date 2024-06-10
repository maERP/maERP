using MediatR;

namespace maERP.Application.Features.Customer.Queries.CustomerList;

public record CustomerListQuery : IRequest<List<CustomerListResponse>>;