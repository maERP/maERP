using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Statistic.Queries.StatisticOrderCustomerChart;

public record StatisticOrderCustomerChartQuery : IRequest<Result<StatisticOrderCustomerChartResponse>>;