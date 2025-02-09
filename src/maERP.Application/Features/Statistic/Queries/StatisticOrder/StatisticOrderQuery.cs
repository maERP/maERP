using maERP.Domain.Dtos.Statistic;
using MediatR;

namespace maERP.Application.Features.Statistic.Queries.StatisticOrder;

public record StatisticOrderQuery : IRequest<StatisticOrderDto>;