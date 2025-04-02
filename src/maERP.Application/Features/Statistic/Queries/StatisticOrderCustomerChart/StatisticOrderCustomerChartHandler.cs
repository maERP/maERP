using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.StatisticOrderCustomerChart;

public class StatisticOrderCustomerChartHandler : IRequestHandler<StatisticOrderCustomerChartQuery, Result<StatisticOrderCustomerChartResponse>>
{
    private readonly IAppLogger<StatisticOrderCustomerChartHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public StatisticOrderCustomerChartHandler(IAppLogger<StatisticOrderCustomerChartHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<Result<StatisticOrderCustomerChartResponse>> Handle(StatisticOrderCustomerChartQuery request, CancellationToken cancellationToken)
    { 
        try
        {
            _logger.LogInformation("Handle StatisticOrderCustomerChartQuery: {0}", request);
            
            var response = new StatisticOrderCustomerChartResponse();
            
            response.chartData = await _orderRepository.Entities
                .Where(order => order.DateOrdered >= DateTime.UtcNow.AddDays(-30))
                .GroupBy(order => order.DateOrdered.Date)
                .Select(group => new OrderCustomerChartDto
                {
                    Date = group.Key,
                    OrdersNew = group.Count(),
                    CustomersNew = group.Select(order => order.CustomerId).Distinct().Count()
                })
                .ToListAsync(cancellationToken);

            return Result<StatisticOrderCustomerChartResponse>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Fehler beim Ermitteln der Bestell- und Kundendiagrammdaten: {0}", ex.Message);
            return Result<StatisticOrderCustomerChartResponse>.Fail(ResultStatusCode.InternalServerError, 
                "Fehler beim Ermitteln der Bestell- und Kundendiagrammdaten");
        }
    }
}