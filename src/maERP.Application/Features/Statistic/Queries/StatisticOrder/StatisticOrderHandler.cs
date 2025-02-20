using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Statistic;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.StatisticOrder;

public class StatisticOrderHandler : IRequestHandler<StatisticOrderQuery, StatisticOrderDto>
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

    public async Task<StatisticOrderDto> Handle(StatisticOrderQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handle StatisticOrderQuery: {0}", request);
        
        var response = new StatisticOrderDto();
        var thirtyDaysAgo = DateTime.UtcNow.Date.AddDays(-30);

        // Get basic statistics
        response.OrderTotal = await _orderRepository.Entities.CountAsync(cancellationToken);
        response.Order30Days = await _orderRepository.Entities
            .Where(o => o.DateOrdered >= thirtyDaysAgo)
            .CountAsync(cancellationToken);
        response.CustomerTotal = await _customerRepository.Entities.CountAsync(cancellationToken);

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
            response.DailyStatistics.Add(new DailyStatistic
            {
                Date = date,
                OrderCount = dailyOrders.GetValueOrDefault(date, 0),
                NewCustomerCount = dailyNewCustomers.GetValueOrDefault(date, 0)
            });
        }

        // Sort by date ascending
        response.DailyStatistics = response.DailyStatistics.OrderBy(x => x.Date).ToList();
        
        return response;
    }
}