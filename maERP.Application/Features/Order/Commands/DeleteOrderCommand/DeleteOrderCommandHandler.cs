using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.Order.Commands.DeleteOrderCommand;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<DeleteOrderCommandHandler> _logger;
    private readonly IOrderRepository _orderRepository;


    public DeleteOrderCommandHandler(IMapper mapper,
        IAppLogger<DeleteOrderCommandHandler> logger,
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<int> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new DeleteOrderCommandValidator(_orderRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(CreateOrderCommand), request.Id);
            throw new Exceptions.ValidationException("Invalid Order", validationResult);
        }

        // convert to domain entity object
        var orderToDelete = _mapper.Map<Domain.Models.Order>(request);

        // add to database
        await _orderRepository.CreateAsync(orderToDelete);

        // return record id
        return orderToDelete.Id;
    }
}
