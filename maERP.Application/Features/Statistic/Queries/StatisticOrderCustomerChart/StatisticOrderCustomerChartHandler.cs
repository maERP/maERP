using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace maERP.Application.Features.Statistic.Queries.StatisticOrderChart;

public class StatisticOrderCustomerChartHandler : IRequestHandler<StatisticOrderCustomerChartQuery, StatisticOrderCustomerChartResponse>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<StatisticOrderCustomerChartHandler> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;

    public StatisticOrderCustomerChartHandler(IMapper mapper,
        IAppLogger<StatisticOrderCustomerChartHandler> logger,
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
    }

    public async Task<StatisticOrderCustomerChartResponse> Handle(StatisticOrderCustomerChartQuery request, CancellationToken cancellationToken)
    { 
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
            .ToListAsync();
        
        return response;
    }
}