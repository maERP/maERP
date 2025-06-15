using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Queries.OrderList;

public class OrderListHandler : IRequestHandler<OrderListQuery, PaginatedResult<OrderListDto>>
{
    private readonly IAppLogger<OrderListHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderListHandler(
        IAppLogger<OrderListHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<PaginatedResult<OrderListDto>> Handle(OrderListQuery request, CancellationToken cancellationToken)
    {
        var orderFilterSpec = new OrderFilterSpecification(request.SearchString);

        _logger.LogInformation("Handle OrderListQuery: {0}", request);

        if (request.OrderBy.Any() != true)
        {
            return await _orderRepository.Entities
               .Specify(orderFilterSpec)
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
            .Specify(orderFilterSpec)
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