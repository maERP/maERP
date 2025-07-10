using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Statistic.Queries.StatisticOrderCustomerChart;

public record StatisticOrderCustomerChartQuery : IRequest<Result<StatisticOrderCustomerChartResponse>>;