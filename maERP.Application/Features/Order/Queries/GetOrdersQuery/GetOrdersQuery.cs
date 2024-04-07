using maERP.Application.Dtos.Order;
using MediatR;

namespace maERP.Application.Features.Order.Queries.GetOrdersQuery;

public record GetOrdersQuery : IRequest<List<OrderListDto>>;