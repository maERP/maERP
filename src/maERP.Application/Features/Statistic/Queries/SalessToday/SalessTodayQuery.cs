using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Statistic.Queries.SalessToday;

public record SalessTodayQuery(Guid? SalesChannelId = null) : IRequest<Result<SalessTodayDto>>;
