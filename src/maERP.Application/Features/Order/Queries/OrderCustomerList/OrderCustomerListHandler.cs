using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Order.Queries.OrderCustomerList;

public class OrderCustomerListHandler : IRequestHandler<OrderCustomerListQuery, PaginatedResult<OrderListDto>>
{
    private readonly IAppLogger<OrderCustomerListHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderCustomerListHandler(
        IAppLogger<OrderCustomerListHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<PaginatedResult<OrderListDto>> Handle(OrderCustomerListQuery request, CancellationToken cancellationToken)
    {
        var orderFilterSpec = new OrderCustomerFilterSpecification(request.CustomerId, request.SearchString);

        _logger.LogInformation("Handle OrderCustomerListQuery für Kunde {CustomerId}: {Request}", request.CustomerId, request);

        if (request.OrderBy.Any() != true)
        {
            return await _orderRepository.Entities
               .Specify(orderFilterSpec)
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
            .Specify(orderFilterSpec)
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