using MediatR;

namespace maERP.Application.Features.Statistic.Queries.StatisticOrder;

public record StatisticOrderQuery : IRequest<StatisticOrderResponse>;