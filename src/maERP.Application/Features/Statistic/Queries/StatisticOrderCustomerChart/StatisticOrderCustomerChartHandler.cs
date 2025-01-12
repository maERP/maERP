using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.StatisticOrderCustomerChart;

public class StatisticOrderCustomerChartHandler : IRequestHandler<StatisticOrderCustomerChartQuery, StatisticOrderCustomerChartResponse>
{
    private readonly IAppLogger<StatisticOrderCustomerChartHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public StatisticOrderCustomerChartHandler(IAppLogger<StatisticOrderCustomerChartHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<StatisticOrderCustomerChartResponse> Handle(StatisticOrderCustomerChartQuery request, CancellationToken cancellationToken)
    { 
        var response = new StatisticOrderCustomerChartResponse();
        
        _logger.LogInformation("Handle StatisticOrderCustomerChartQuery: {0}", request);

        response.chartData = await _orderRepository.Entities
            .Where(order => order.DateOrdered >= DateTime.UtcNow.AddDays(-30))
            .GroupBy(order => order.DateOrdered.Date)
            .Select(group => new OrderCustomerChartDto
            {
                Date = group.Key,
                OrdersNew = group.Count(),
                CustomersNew = group.Select(order => order.CustomerId).Distinct().Count()
            })
            .ToListAsync();

        return response;
    }
}