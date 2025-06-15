using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderDelete;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Result<int>>
{
    private readonly IAppLogger<DeleteOrderHandler> _logger;
    private readonly IOrderRepository _orderRepository;


    public DeleteOrderHandler(IAppLogger<DeleteOrderHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<Result<int>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting order with ID: {Id}", request.Id);

        var result = new Result<int>();

        // Validate incoming data
        var validator = new DeleteOrderValidator(_orderRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in delete request for {0}: {1}",
                nameof(DeleteOrderCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Create entity to delete
            var orderToDelete = new Domain.Entities.Order
            {
                Id = request.Id
            };

            // Delete from database
            await _orderRepository.DeleteAsync(orderToDelete);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = orderToDelete.Id;

            _logger.LogInformation("Successfully deleted order with ID: {Id}", orderToDelete.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the order: {ex.Message}");

            _logger.LogError("Error deleting order: {Message}", ex.Message);
        }

        return result;
    }
}
