using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Queries.OrderNotPaidList;

public class OrderNotPaidListHandler : IRequestHandler<OrderNotPaidListQuery, PaginatedResult<OrderListDto>>
{
    private readonly IAppLogger<OrderNotPaidListHandler> _logger;
    private readonly IOrderRepository _orderRepository;

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

        // Filtern der Bestellungen, die nicht bezahlt und nicht versendet sind
        // Nicht bezahlt: PaymentStatus ist nicht CompletelyPaid
        // Nicht versendet: Status ist nicht PartiallyDelivered, Completed, Returned oder Refunded
        var notPaidStatuses = new[] {
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

        var shippableStatuses = new[] {
            OrderStatus.Pending,
            OrderStatus.Processing,
            OrderStatus.ReadyForDelivery,
            OrderStatus.OnHold
        };

        if (request.OrderBy.Any() != true)
        {
            return await _orderRepository.Entities
               .Where(o => notPaidStatuses.Contains(o.PaymentStatus) && shippableStatuses.Contains(o.Status))
               .Select(o => new OrderListDto
               {
                   Id = o.Id,
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
            .Where(o => notPaidStatuses.Contains(o.PaymentStatus) && shippableStatuses.Contains(o.Status))
            .OrderBy(ordering)
            .Select(o => new OrderListDto
            {
                Id = o.Id,
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