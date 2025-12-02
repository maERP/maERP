using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Statistic.Queries.OrdersToday;

public record OrdersTodayQuery : IRequest<Result<OrdersTodayDto>>;
