using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.Order;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Order.Queries.GetOrderDetails;

public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetOrderDetailsHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public GetOrderDetailsHandler(IMapper mapper,
        IAppLogger<GetOrderDetailsHandler> logger,
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _orderRepository = orderRepository;
    }
    public async Task<OrderDetailDto> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetWithDetailsAsync(request.Id);

        if(order == null)
        {
            throw new NotFoundException("NotFoundException", "Order not found.");
        }

        var data = _mapper.Map<OrderDetailDto>(order);

        _logger.LogInformation("All Orderes are retrieved successfully.");
        return data;
    }
}