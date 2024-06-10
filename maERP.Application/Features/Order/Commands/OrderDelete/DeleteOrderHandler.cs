using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderDelete;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<DeleteOrderHandler> _logger;
    private readonly IOrderRepository _orderRepository;


    public DeleteOrderHandler(IMapper mapper,
        IAppLogger<DeleteOrderHandler> logger,
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<int> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteOrderValidator(_orderRepository);
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
