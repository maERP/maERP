using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderUpdate;

public class OrderUpdateHandler : IRequestHandler<OrderUpdateCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<OrderUpdateHandler> _logger;
    private readonly IOrderRepository _orderRepository;


    public OrderUpdateHandler(IMapper mapper,
        IAppLogger<OrderUpdateHandler> logger,
        IOrderRepository orderRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<Result<int>> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating order with ID: {Id}", request.Id);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new OrderUpdateValidator(_orderRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in update request for {0}: {1}", 
                nameof(OrderUpdateCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Map to domain entity
            var orderToUpdate = _mapper.Map<Domain.Entities.Order>(request);
            
            // Update in database
            await _orderRepository.UpdateAsync(orderToUpdate);
            
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = orderToUpdate.Id;
            
            _logger.LogInformation("Successfully updated order with ID: {Id}", orderToUpdate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the order: {ex.Message}");
            
            _logger.LogError("Error updating order: {Message}", ex.Message);
        }

        return result;
    }
}
