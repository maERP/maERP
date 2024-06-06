using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Order.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateOrderCommandHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IMapper mapper,
        IAppLogger<CreateOrderCommandHandler> logger,
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateOrderCommandValidator(_orderRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(CreateOrderCommand), request.Id);
            throw new ValidationException("Invalid Order", validationResult);
        }

        // convert to domain entity object
        var orderToCreate = _mapper.Map<Domain.Models.Order>(request);

        // add to database
        await _orderRepository.CreateAsync(orderToCreate);

        // return record id
        return orderToCreate.Id;
    }
}