using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderUpdate;

public class OrderUpdateHandler : IRequestHandler<OrderUpdateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<OrderUpdateHandler> _logger;
    private readonly IOrderRepository _orderRepository;


    public OrderUpdateHandler(IMapper mapper,
        IAppLogger<OrderUpdateHandler> logger,
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<int> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
    {
        var validator = new OrderUpdateValidator(_orderRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(OrderUpdateCommand), request.Id);
            throw new ValidationException("Invalid Order", validationResult);
        }

        var orderToUpdate = _mapper.Map<Domain.Entities.Order>(request);

        await _orderRepository.UpdateAsync(orderToUpdate);

        return orderToUpdate.Id;
    }
}
