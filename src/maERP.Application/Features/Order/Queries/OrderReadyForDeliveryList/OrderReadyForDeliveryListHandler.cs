using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Order.Queries.OrderReadyForDeliveryList;

public class OrderReadyForDeliveryListHandler : IRequestHandler<OrderReadyForDeliveryListQuery, PaginatedResult<OrderListDto>>
{
    private readonly IAppLogger<OrderReadyForDeliveryListHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderReadyForDeliveryListHandler(
        IAppLogger<OrderReadyForDeliveryListHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<PaginatedResult<OrderListDto>> Handle(OrderReadyForDeliveryListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handle OrderReadyForDeliveryListQuery: {0}", request);

        if (request.OrderBy.Any() != true)
        {
            return await _orderRepository.Entities
               .Where(o => o.Status == OrderStatus.ReadyForDelivery && o.PaymentStatus == PaymentStatus.CompletelyPaid)
               .Select(o => new OrderListDto
               {
                   Id = o.Id,
                   OrderId = o.OrderId,
                   CustomerId = o.CustomerId,
                   InvoiceAddressFirstName = o.InvoiceAddressFirstName,
                   InvoiceAddressLastName = o.InvoiceAddressLastName,
                   Total = o.Total,
                   Status = o.Status,
                   PaymentStatus = o.PaymentStatus,
                   DateOrdered = o.DateOrdered
               })
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        var ordering = string.Join(",", request.OrderBy);

        return await _orderRepository.Entities
            .Where(o => o.Status == OrderStatus.ReadyForDelivery && o.PaymentStatus == PaymentStatus.CompletelyPaid)
            .OrderBy(ordering)
            .Select(o => new OrderListDto
            {
                Id = o.Id,
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                InvoiceAddressFirstName = o.InvoiceAddressFirstName,
                InvoiceAddressLastName = o.InvoiceAddressLastName,
                Total = o.Total,
                Status = o.Status,
                PaymentStatus = o.PaymentStatus,
                DateOrdered = o.DateOrdered
            })
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}