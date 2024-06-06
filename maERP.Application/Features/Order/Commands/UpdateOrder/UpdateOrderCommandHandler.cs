using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Order.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateOrderCommandHandler> _logger;
    private readonly IOrderRepository _orderRepository;


    public UpdateOrderCommandHandler(IMapper mapper,
        IAppLogger<UpdateOrderCommandHandler> logger,
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<int> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateOrderCommandValidator(_orderRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(UpdateOrderCommand), request.Id);
            throw new ValidationException("Invalid Order", validationResult);
        }

        var orderToUpdate = _mapper.Map<Domain.Models.Order>(request);

        await _orderRepository.UpdateAsync(orderToUpdate);

        return orderToUpdate.Id;
    }
}
