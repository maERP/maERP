using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Manufacturer.Commands.ManufacturerCreate;

/// <summary>
/// Handler for processing manufacturer creation commands.
/// Implements IRequestHandler from MediatR to handle ManufacturerCreateCommand requests
/// and return the ID of the newly created manufacturer wrapped in a Result.
/// </summary>
public class ManufacturerCreateHandler : IRequestHandler<ManufacturerCreateCommand, Result<Guid>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<ManufacturerCreateHandler> _logger;

    /// <summary>
    /// Repository for manufacturer data operations
    /// </summary>
    private readonly IManufacturerRepository _manufacturerRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="manufacturerRepository">Repository for manufacturer data access</param>
    public ManufacturerCreateHandler(
        IAppLogger<ManufacturerCreateHandler> logger,
        IManufacturerRepository manufacturerRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
    }

    /// <summary>
    /// Handles the manufacturer creation request
    /// </summary>
    /// <param name="request">The manufacturer creation command with manufacturer details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the newly created manufacturer if successful</returns>
    public async Task<Result<Guid>> Handle(ManufacturerCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new manufacturer with name: {Name}", request.Name);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new ManufacturerCreateValidator(_manufacturerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation error messages
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in create request for {0}: {1}",
                nameof(ManufacturerCreateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Manual mapping to domain entity
            var manufacturerToCreate = new Domain.Entities.Manufacturer
            {
                Name = request.Name,
                Street = request.Street,
                City = request.City,
                State = request.State,
                Country = request.Country,
                ZipCode = request.ZipCode,
                Phone = request.Phone,
                Email = request.Email,
                Website = request.Website,
                Logo = request.Logo
            };

            // Add the new manufacturer to the database
            await _manufacturerRepository.CreateAsync(manufacturerToCreate);

            // Set successful result with the new manufacturer ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = manufacturerToCreate.Id;

            _logger.LogInformation("Successfully created manufacturer with ID: {Id}", manufacturerToCreate.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during manufacturer creation
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the manufacturer: {ex.Message}");

            _logger.LogError("Error creating manufacturer: {Message}", ex.Message);
        }

        return result;
    }
}