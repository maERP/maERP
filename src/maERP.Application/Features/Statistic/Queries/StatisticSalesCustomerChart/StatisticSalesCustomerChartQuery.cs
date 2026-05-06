using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Statistic.Queries.StatisticSalesCustomerChart;

public record StatisticSalesCustomerChartQuery : IRequest<Result<StatisticSalesCustomerChartResponse>>;