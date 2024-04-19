using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.Order;
using MediatR;

namespace maERP.Application.Features.Order.Queries.GetOrderDetailQuery;

public class GetOrderDetailQueryHandler : IRequestHandler<GetOrderDetailQuery, OrderDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetOrderDetailQueryHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public GetOrderDetailQueryHandler(IMapper mapper,
        IAppLogger<GetOrderDetailQueryHandler> logger,
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _orderRepository = orderRepository;
    }
    public async Task<OrderDetailDto> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var order = await _orderRepository.GetByIdAsync(request.Id);

        // Convert data objects to DTO objects
        var data = _mapper.Map<OrderDetailDto>(order);

        // Return list of DTO objects
        _logger.LogInformation("All Orderes are retrieved successfully.");
        return data;
    }
}