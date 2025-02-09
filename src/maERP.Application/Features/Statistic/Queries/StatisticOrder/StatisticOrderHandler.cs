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

        response.OrderTotal = await _customerRepository.Entities.CountAsync();
        response.Order30Days = await _orderRepository.Entities.Where(o => o.DateOrdered > DateTime.UtcNow.AddDays(-30)).CountAsync();
        response.CustomerTotal = await _customerRepository.Entities.CountAsync();
        
        return response;
    }
}