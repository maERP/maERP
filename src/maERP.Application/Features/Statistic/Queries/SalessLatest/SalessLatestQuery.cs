using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Statistic.Queries.SalessLatest;

public record SalessLatestQuery(int Count = 5, Guid? SalesChannelId = null) : IRequest<Result<SalessLatestDto>>;
