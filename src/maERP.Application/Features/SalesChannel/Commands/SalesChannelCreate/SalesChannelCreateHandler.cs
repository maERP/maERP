using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;

/// <summary>
/// Handler for processing sales channel creation commands.
/// Implements IRequestHandler from MediatR to handle SalesChannelCreateCommand requests
/// and return the ID of the newly created sales channel wrapped in a Result.
/// </summary>
public class SalesChannelCreateHandler : IRequestHandler<SalesChannelCreateCommand, Result<int>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<SalesChannelCreateHandler> _logger;

    /// <summary>
    /// Repository for sales channel data operations
    /// </summary>
    private readonly ISalesChannelRepository _salesChannelRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="salesChannelRepository">Repository for sales channel data access</param>
    public SalesChannelCreateHandler(
        IAppLogger<SalesChannelCreateHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _salesChannelRepository = salesChannelRepository ?? throw new ArgumentNullException(nameof(salesChannelRepository));
    }

    /// <summary>
    /// Handles the sales channel creation request
    /// </summary>
    /// <param name="request">The sales channel creation command with sales channel details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the newly created sales channel if successful</returns>
    public async Task<Result<int>> Handle(SalesChannelCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new sales channel with name: {Name}", request.Name);

        var result = new Result<int>();

        // Validate incoming data
        var validator = new SalesChannelCreateValidator(_salesChannelRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation error messages
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in create request for {0}: {1}",
                nameof(SalesChannelCreateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Map request to domain entity
            var salesChannelToCreate = MapToEntity(request);

            // Add the new sales channel to the database
            await _salesChannelRepository.CreateAsync(salesChannelToCreate);

            // Set successful result with the new sales channel ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = salesChannelToCreate.Id;

            _logger.LogInformation("Successfully created sales channel with ID: {Id}", salesChannelToCreate.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during sales channel creation
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the sales channel: {ex.Message}");

            _logger.LogError("Error creating sales channel: {Message}", ex.Message);
        }

        return result;
    }

    /// <summary>
    /// Maps a sales channel command to a domain entity
    /// </summary>
    /// <param name="command">The sales channel creation command</param>
    /// <returns>A new sales channel entity with properties from the command</returns>
    private Domain.Entities.SalesChannel MapToEntity(SalesChannelCreateCommand command)
    {
        return new Domain.Entities.SalesChannel
        {
            Type = command.SalesChannelType,
            Name = command.Name,
            Url = command.Url,
            Username = command.Username,
            Password = command.Password,
            ImportProducts = command.ImportProducts,
            ImportCustomers = command.ImportCustomers,
            ImportOrders = command.ImportOrders,
            ExportProducts = command.ExportProducts,
            ExportCustomers = command.ExportCustomers,
            ExportOrders = command.ExportOrders,
            WarehouseId = command.WarehouseId
        };
    }
}