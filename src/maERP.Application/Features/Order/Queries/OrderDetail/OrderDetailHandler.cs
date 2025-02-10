using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using maERP.Domain.Dtos.Order;
using MediatR;

namespace maERP.Application.Features.Order.Queries.OrderDetail;

public class OrderDetailHandler : IRequestHandler<OrderDetailQuery, OrderDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<OrderDetailHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderDetailHandler(IMapper mapper,
        IAppLogger<OrderDetailHandler> logger,
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _orderRepository = orderRepository;
    }
    public async Task<OrderDetailDto> Handle(OrderDetailQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetWithDetailsAsync(request.Id);

        if(order == null)
        {
            throw new NotFoundException("NotFoundException", "Order not found.");
        }

        var data = _mapper.Map<OrderDetailDto>(order);

        _logger.LogInformation("Order retrieved successfully.");
        return data;
    }
}