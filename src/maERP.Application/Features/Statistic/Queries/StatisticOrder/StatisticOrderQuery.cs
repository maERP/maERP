using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Statistic.Queries.StatisticOrder;

public record StatisticOrderQuery : IRequest<Result<StatisticOrderDto>>;