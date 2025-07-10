using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseUpdate;

public class WarehouseUpdateHandler : IRequestHandler<WarehouseUpdateCommand, Result<int>>
{
    private readonly IAppLogger<WarehouseUpdateHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;


    public WarehouseUpdateHandler(
        IAppLogger<WarehouseUpdateHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
    }

    public async Task<Result<int>> Handle(WarehouseUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating warehouse with ID: {Id}, Name: {Name}", request.Id, request.Name);

        var result = new Result<int>();

        // Validate incoming data
        var validator = new WarehouseUpdateValidator(_warehouseRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(WarehouseUpdateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Manuelles Mapping zur Domain-Entität
            var warehouseToUpdate = new Domain.Entities.Warehouse
            {
                Id = request.Id,
                Name = request.Name
            };

            // Update in database
            await _warehouseRepository.UpdateAsync(warehouseToUpdate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = warehouseToUpdate.Id;

            _logger.LogInformation("Successfully updated warehouse with ID: {Id}", warehouseToUpdate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the warehouse: {ex.Message}");

            _logger.LogError("Error updating warehouse: {Message}", ex.Message);
        }

        return result;
    }
}
