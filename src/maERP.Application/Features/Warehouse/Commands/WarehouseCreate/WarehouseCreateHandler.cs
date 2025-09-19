using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseCreate;

/// <summary>
/// Handler for processing warehouse creation commands.
/// Implements IRequestHandler from MediatR to handle WarehouseCreateCommand requests
/// and return the ID of the newly created warehouse wrapped in a Result.
/// </summary>
public class WarehouseCreateHandler : IRequestHandler<WarehouseCreateCommand, Result<Guid>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<WarehouseCreateHandler> _logger;

    /// <summary>
    /// Repository for warehouse data operations
    /// </summary>
    private readonly IWarehouseRepository _warehouseRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="warehouseRepository">Repository for warehouse data access</param>
    public WarehouseCreateHandler(
        IAppLogger<WarehouseCreateHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
    }

    /// <summary>
    /// Handles the warehouse creation request
    /// </summary>
    /// <param name="request">The warehouse creation command with warehouse details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the newly created warehouse if successful</returns>
    public async Task<Result<Guid>> Handle(WarehouseCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new warehouse with name: {Name}", request.Name);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new WarehouseCreateValidator(_warehouseRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation error messages
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in create request for {0}: {1}",
                nameof(WarehouseCreateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Manual mapping to domain entity
            var warehouseToCreate = new Domain.Entities.Warehouse
            {
                Name = request.Name
            };

            // Add the new warehouse to the database
            await _warehouseRepository.CreateAsync(warehouseToCreate);

            // Set successful result with the new warehouse ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = warehouseToCreate.Id;

            _logger.LogInformation("Successfully created warehouse with ID: {Id}", warehouseToCreate.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during warehouse creation
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the warehouse: {ex.Message}");

            _logger.LogError("Error creating warehouse: {Message}", ex.Message);
        }

        return result;
    }
}
