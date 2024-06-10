using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderCreate;

public class OrderCreateHandler : IRequestHandler<OrderCreateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<OrderCreateHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderCreateHandler(IMapper mapper,
        IAppLogger<OrderCreateHandler> logger,
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<int> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new OrderCreateValidator(_orderRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(OrderCreateCommand), request.Id);
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