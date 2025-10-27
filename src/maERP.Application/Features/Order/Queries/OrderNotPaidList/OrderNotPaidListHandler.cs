using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Order.Queries.OrderNotPaidList;

public class OrderNotPaidListHandler : IRequestHandler<OrderNotPaidListQuery, PaginatedResult<OrderListDto>>
{
    private readonly IAppLogger<OrderNotPaidListHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    // Statische Listen für Entity Framework Expression Trees
    private static readonly List<PaymentStatus> NotPaidStatuses = new()
    {
        PaymentStatus.Unknown,
        PaymentStatus.Invoiced,
        PaymentStatus.PartiallyPaid,
        PaymentStatus.FirstReminder,
        PaymentStatus.SecondReminder,
        PaymentStatus.ThirdReminder,
        PaymentStatus.Encashment,
        PaymentStatus.Reserved,
        PaymentStatus.Delayed,
        PaymentStatus.ReviewNecessary,
        PaymentStatus.NoCreditApproved,
        PaymentStatus.CreditPreliminarilyAccepted
    };

    private static readonly List<OrderStatus> ShippableStatuses = new()
    {
        OrderStatus.Pending,
        OrderStatus.Processing,
        OrderStatus.ReadyForDelivery,
        OrderStatus.OnHold
    };

    public OrderNotPaidListHandler(
        IAppLogger<OrderNotPaidListHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<PaginatedResult<OrderListDto>> Handle(OrderNotPaidListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handle OrderNotPaidListQuery: {0}", request);

        if (request.OrderBy.Any() != true)
        {
            return await _orderRepository.Entities
               .Where(o => NotPaidStatuses.Contains(o.PaymentStatus) && ShippableStatuses.Contains(o.Status))
               .Select(o => new OrderListDto
               {
                   Id = o.Id,
                   OrderId = o.OrderId,
                   CustomerId = o.CustomerId,
                   InvoiceAddressFirstName = o.InvoiceAddressFirstName,
                   InvoiceAddressLastName = o.InvoiceAddressLastName,
                   Total = o.Total,
                   Status = o.Status.ToString(),
                   PaymentStatus = o.PaymentStatus.ToString(),
                   DateOrdered = o.DateOrdered
               })
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        var ordering = string.Join(",", request.OrderBy);

        return await _orderRepository.Entities
            .Where(o => NotPaidStatuses.Contains(o.PaymentStatus) && ShippableStatuses.Contains(o.Status))
            .OrderBy(ordering)
            .Select(o => new OrderListDto
            {
                Id = o.Id,
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                InvoiceAddressFirstName = o.InvoiceAddressFirstName,
                InvoiceAddressLastName = o.InvoiceAddressLastName,
                Total = o.Total,
                Status = o.Status.ToString(),
                PaymentStatus = o.PaymentStatus.ToString(),
                DateOrdered = o.DateOrdered
            })
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
