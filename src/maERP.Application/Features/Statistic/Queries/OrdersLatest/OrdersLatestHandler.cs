using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.OrdersLatest;

public class OrdersLatestHandler : IRequestHandler<OrdersLatestQuery, Result<OrdersLatestDto>>
{
    private readonly IAppLogger<OrdersLatestHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrdersLatestHandler(
        IAppLogger<OrdersLatestHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrdersLatestDto>> Handle(OrdersLatestQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handle OrdersLatestQuery - fetching {Count} latest orders", request.Count);

            var baseQuery = _orderRepository.Entities.AsQueryable();
            if (request.SalesChannelId.HasValue)
                baseQuery = baseQuery.Where(o => o.SalesChannelId == request.SalesChannelId.Value);

            var orders = await baseQuery
                .Include(o => o.Customer)
                .OrderByDescending(o => o.DateOrdered)
                .Take(request.Count)
                .Select(o => new OrdersLatestItemDto
                {
                    Id = o.Id,
                    OrderNumber = $"ORD-{o.DateOrdered.Year}-{o.OrderId:D6}",
                    CustomerName = o.Customer != null
                        ? $"{o.Customer.Firstname} {o.Customer.Lastname}".Trim()
                        : $"{o.DeliveryAddressFirstName} {o.DeliveryAddressLastName}".Trim(),
                    Amount = o.Total,
                    Status = o.Status,
                    OrderDate = o.DateOrdered
                })
                .ToListAsync(cancellationToken);

            var dto = new OrdersLatestDto
            {
                Orders = orders
            };

            _logger.LogInformation("Successfully fetched {Count} latest orders", orders.Count);
            return Result<OrdersLatestDto>.Success(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while fetching latest orders: {0}", ex.Message);
            return Result<OrdersLatestDto>.Fail(ResultStatusCode.InternalServerError, "Error while fetching latest orders");
        }
    }
}
