using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Queries.OrderCustomerList;

public class OrderCustomerListHandler : IRequestHandler<OrderCustomerListQuery, PaginatedResult<OrderListDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<OrderCustomerListHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderCustomerListHandler(IMapper mapper,
        IAppLogger<OrderCustomerListHandler> logger, 
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
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
               .ProjectTo<OrderListDto>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        var ordering = string.Join(",", request.OrderBy);

        return await _orderRepository.Entities
            .Specify(orderFilterSpec)
            .OrderBy(ordering)
            .ProjectTo<OrderListDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
} 