using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.StatisticOrder;

public class StatisticOrderHandler : IRequestHandler<StatisticOrderQuery, Result<StatisticOrderDto>>
{
    private readonly IAppLogger<StatisticOrderHandler> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;

    public StatisticOrderHandler(IAppLogger<StatisticOrderHandler> logger,
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
    }

    public async Task<Result<StatisticOrderDto>> Handle(StatisticOrderQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handle StatisticOrderQuery: {0}", request);

            var statisticDto = new StatisticOrderDto();
            var thirtyDaysAgo = DateTime.UtcNow.Date.AddDays(-30);

            // Get basic statistics
            statisticDto.OrderTotal = await _orderRepository.Entities.CountAsync(cancellationToken);
            statisticDto.Order30Days = await _orderRepository.Entities
                .Where(o => o.DateOrdered >= thirtyDaysAgo)
                .CountAsync(cancellationToken);
            statisticDto.CustomerTotal = await _customerRepository.Entities.CountAsync(cancellationToken);

            // Get daily statistics for the last 30 days
            var dailyOrders = await _orderRepository.Entities
                .Where(o => o.DateOrdered >= thirtyDaysAgo)
                .GroupBy(o => o.DateOrdered.Date)
                .Select(g => new { Date = g.Key, OrderCount = g.Count() })
                .ToDictionaryAsync(x => x.Date, x => x.OrderCount, cancellationToken);

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
                    OrderCount = dailyOrders.GetValueOrDefault(date, 0),
                    NewCustomerCount = dailyNewCustomers.GetValueOrDefault(date, 0)
                });
            }

            // Sort by date ascending
            statisticDto.DailyStatistics = statisticDto.DailyStatistics.OrderBy(x => x.Date).ToList();

            return Result<StatisticOrderDto>.Success(statisticDto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Fehler beim Ermitteln der Bestellstatistik: {0}", ex.Message);
            return Result<StatisticOrderDto>.Fail(ResultStatusCode.InternalServerError, "Fehler beim Ermitteln der Bestellstatistik");
        }
    }
}