using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Statistic.Queries.SalesToday;

public record SalesTodayQuery(Guid? SalesChannelId = null) : IRequest<Result<SalesTodayDto>>;
