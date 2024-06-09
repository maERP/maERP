#nullable disable

using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Shared.Wrapper;
using MediatR;
using System.Linq.Dynamic.Core;

namespace maERP.Application.Features.Order.Queries.GetOrders;

public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, PaginatedResult<GetOrdersResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetOrdersHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public GetOrdersHandler(IMapper mapper,
        IAppLogger<GetOrdersHandler> logger, 
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _orderRepository = orderRepository; 
    }

    public async Task<PaginatedResult<GetOrdersResponse>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orderFilterSpec = new OrderFilterSpecification(request.SearchString);

        if (request.OrderBy?.Any() != true)
        {
            return await _orderRepository.Entities
               .Specify(orderFilterSpec)
               .ProjectTo<GetOrdersResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            var ordering = string.Join(",", request.OrderBy);

            return await _orderRepository.Entities
               .Specify(orderFilterSpec)
               .OrderBy(ordering)
               .ProjectTo<GetOrdersResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}