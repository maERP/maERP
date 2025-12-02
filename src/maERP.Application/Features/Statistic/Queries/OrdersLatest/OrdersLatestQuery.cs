using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Statistic.Queries.OrdersLatest;

public record OrdersLatestQuery(int Count = 5) : IRequest<Result<OrdersLatestDto>>;
