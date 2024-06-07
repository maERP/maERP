using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Order.Queries.GetOrderDetail;

public class GetOrderDetailHandler : IRequestHandler<GetOrderDetailQuery, GetOrderDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetOrderDetailHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public GetOrderDetailHandler(IMapper mapper,
        IAppLogger<GetOrderDetailHandler> logger,
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _orderRepository = orderRepository;
    }
    public async Task<GetOrderDetailResponse> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetWithDetailsAsync(request.Id);

        if(order == null)
        {
            throw new NotFoundException("NotFoundException", "Order not found.");
        }

        var data = _mapper.Map<GetOrderDetailResponse>(order);

        _logger.LogInformation("All Orderes are retrieved successfully.");
        return data;
    }
}