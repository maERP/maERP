using maERP.Application.Contracts.Persistence;
using maERP.Domain.Models;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Repositories;

public class OrderImportRepository : IOrderImportRepository
{
    private readonly ILogger<ProductImportRepository> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderImportRepository(ILogger<ProductImportRepository> logger, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task ImportOrUpdateFromSalesChannel(int salesChannelId, SalesChannelImportOrder importOrder)
    {
        var existingOrder = await _orderRepository.GetByRemoteOrderIdAsync(salesChannelId, importOrder.RemoteOrderId);

        if (existingOrder == null)
        {
            _logger.LogInformation("Order {0} does not exist, creating Product and SalesChannel", importOrder.RemoteOrderId);

            var newOrder = new Order
            {
                SalesChannelId = salesChannelId,
                RemoteOrderId = importOrder.RemoteOrderId,

                CustomerId = importOrder.CustomerId,
                Status = importOrder.Status,
            };

            await _orderRepository.CreateAsync(newOrder);
            _logger.LogInformation("Order {0} created", importOrder.RemoteOrderId);
        }
        else
        {
            _logger.LogInformation("Order {0} already exists, check for SalesChannel", existingOrder.RemoteOrderId);
            bool somethingChanged = false;

            if (somethingChanged)
            {
                await _orderRepository.UpdateAsync(existingOrder);
                _logger.LogInformation("Product {0} updated", importOrder.RemoteOrderId);
            }
        }
    }
}