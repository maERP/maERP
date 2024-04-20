using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Order.Commands.UpdateOrderCommand;

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
        // Validate incoming data
        var validator = new UpdateOrderCommandValidator(_orderRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(CreateOrderCommand), request.Id);
            throw new ValidationException("Invalid Order", validationResult);
        }

        // convert to domain entity object
        var orderToUpdate = _mapper.Map<Domain.Models.Order>(request);

        // add to database
        await _orderRepository.UpdateAsync(orderToUpdate);

        // return record id
        return orderToUpdate.Id;
    }
}
