using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseDelete;

public class WarehouseDeleteHandler : IRequestHandler<WarehouseDeleteCommand, Result<int>>
{
    private readonly IAppLogger<WarehouseDeleteHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;
    
    public WarehouseDeleteHandler(
        IAppLogger<WarehouseDeleteHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
    }

    public async Task<Result<int>> Handle(WarehouseDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting warehouse with ID: {Id}", request.Id);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new WarehouseDeleteValidator(_warehouseRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in delete request for {0}: {1}", 
                nameof(WarehouseDeleteCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Create entity to delete
            var warehouseToDelete = new Domain.Entities.Warehouse
            {
                Id = request.Id
            };
            
            // Delete from database
            await _warehouseRepository.DeleteAsync(warehouseToDelete);
            
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = warehouseToDelete.Id;
            
            _logger.LogInformation("Successfully deleted warehouse with ID: {Id}", warehouseToDelete.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the warehouse: {ex.Message}");
            
            _logger.LogError("Error deleting warehouse: {Message}", ex.Message);
        }

        return result;
    }
}
