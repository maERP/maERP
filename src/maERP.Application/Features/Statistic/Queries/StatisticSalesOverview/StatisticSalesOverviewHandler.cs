using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.StatisticSalesOverview;

public class StatisticSalesOverviewHandler : IRequestHandler<StatisticSalesOverviewQuery, Result<StatisticSalesOverviewDto>>
{
    private readonly IAppLogger<StatisticSalesOverviewHandler> _logger;
    private readonly ISalesRepository _salesRepository;
    private readonly ICustomerRepository _customerRepository;

    public StatisticSalesOverviewHandler(IAppLogger<StatisticSalesOverviewHandler> logger,
        ISalesRepository salesRepository,
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _salesRepository = salesRepository;
        _customerRepository = customerRepository;
    }

    public async Task<Result<StatisticSalesOverviewDto>> Handle(StatisticSalesOverviewQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handle StatisticSalesOverviewQuery: {0}", request);

            var statisticDto = new StatisticSalesOverviewDto();
            var thirtyDaysAgo = DateTime.UtcNow.Date.AddDays(-30);

            // Get basic statistics
            statisticDto.SalesTotal = await _salesRepository.Entities.CountAsync(cancellationToken);
            statisticDto.Sales30Days = await _salesRepository.Entities
                .Where(o => o.DateSalesed >= thirtyDaysAgo)
                .CountAsync(cancellationToken);
            statisticDto.CustomerTotal = await _customerRepository.Entities.CountAsync(cancellationToken);

            // Get daily statistics for the last 30 days
            var dailySaless = await _salesRepository.Entities
                .Where(o => o.DateSalesed >= thirtyDaysAgo)
                .GroupBy(o => o.DateSalesed.Date)
                .Select(g => new { Date = g.Key, SalesCount = g.Count() })
                .ToDictionaryAsync(x => x.Date, x => x.SalesCount, cancellationToken);

            var dailyNewCustomers = await _customerRepository.Entities
                .Where(c => c.DateCreated >= thirtyDaysAgo)
                .GroupBy(c => c.DateCreated.Date)
                .Select(g => new { Date = g.Key, CustomerCount = g.Count() })
                .ToDictionaryAsync(x => x.Date, x => x.CustomerCount, cancellationToken);

            // Combine the results for each day
            for (var date = thirtyDaysAgo; date <= DateTime.UtcNow.Date; date = date.AddDays(1))
            {
                statisticDto.DailyStatistics.Add(new DailyStatistic
                {
                    Date = date,
                    SalesCount = dailySaless.GetValueOrDefault(date, 0),
                    NewCustomerCount = dailyNewCustomers.GetValueOrDefault(date, 0)
                });
            }

            // Sort by date ascending
            statisticDto.DailyStatistics = statisticDto.DailyStatistics.OrderBy(x => x.Date).ToList();

            return Result<StatisticSalesOverviewDto>.Success(statisticDto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Fehler beim Ermitteln der Bestellstatistik: {0}", ex.Message);
            return Result<StatisticSalesOverviewDto>.Fail(ResultStatusCode.InternalServerError, "Fehler beim Ermitteln der Bestellstatistik");
        }
    }
}