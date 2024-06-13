using MediatR;

namespace maERP.Application.Features.Statistic.Queries.StatisticOrderChart;

public record StatisticOrderCustomerChartQuery : IRequest<StatisticOrderCustomerChartResponse>;