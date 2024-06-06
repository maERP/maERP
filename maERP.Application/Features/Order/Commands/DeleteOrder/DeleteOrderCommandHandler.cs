using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Order.Commands.DeleteOrder;

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
        var validator = new DeleteOrderCommandValidator(_orderRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(DeleteOrderCommand), request.Id);
            throw new ValidationException("Invalid Order", validationResult);
        }

        var orderToDelete = new Domain.Models.Order
        {
            Id = request.Id
        };

        await _orderRepository.DeleteAsync(orderToDelete);

        return orderToDelete.Id;
    }
}
