using MediatR;

namespace maERP.Application.Features.Statistic.Queries.StatisticOrderCustomerChart;

public record StatisticOrderCustomerChartQuery : IRequest<StatisticOrderCustomerChartResponse>;