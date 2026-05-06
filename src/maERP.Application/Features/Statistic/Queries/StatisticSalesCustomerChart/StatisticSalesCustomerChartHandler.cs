using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.StatisticSalesCustomerChart;

public class StatisticSalesCustomerChartHandler : IRequestHandler<StatisticSalesCustomerChartQuery, Result<StatisticSalesCustomerChartResponse>>
{
    private readonly IAppLogger<StatisticSalesCustomerChartHandler> _logger;
    private readonly ISalesRepository _salesRepository;

    public StatisticSalesCustomerChartHandler(IAppLogger<StatisticSalesCustomerChartHandler> logger,
        ISalesRepository salesRepository)
    {
        _logger = logger;
        _salesRepository = salesRepository;
    }

    public async Task<Result<StatisticSalesCustomerChartResponse>> Handle(StatisticSalesCustomerChartQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handle StatisticSalesCustomerChartQuery: {0}", request);

            var response = new StatisticSalesCustomerChartResponse();

            response.chartData = await _salesRepository.Entities
                .Where(sales => sales.DateSalesed >= DateTime.UtcNow.AddDays(-30))
                .GroupBy(sales => sales.DateSalesed.Date)
                .Select(group => new SalesCustomerChartDto
                {
                    Date = group.Key,
                    SalessNew = group.Count(),
                    CustomersNew = group.Select(sales => sales.CustomerId).Distinct().Count()
                })
                .ToListAsync(cancellationToken);

            return Result<StatisticSalesCustomerChartResponse>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Fehler beim Ermitteln der Bestell- und Kundendiagrammdaten: {0}", ex.Message);
            return Result<StatisticSalesCustomerChartResponse>.Fail(ResultStatusCode.InternalServerError,
                "Fehler beim Ermitteln der Bestell- und Kundendiagrammdaten");
        }
    }
}