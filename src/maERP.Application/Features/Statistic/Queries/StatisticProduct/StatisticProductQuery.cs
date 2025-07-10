using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Statistic.Queries.StatisticProduct;

public record StatisticProductQuery : IRequest<Result<StatisticProductDto>>;