using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassCreate;

/// <summary>
/// Handler for processing tax class creation commands.
/// Implements IRequestHandler from MediatR to handle TaxClassCreateCommand requests
/// and return the ID of the newly created tax class wrapped in a Result.
/// </summary>
public class TaxClassCreateHandler : IRequestHandler<TaxClassCreateCommand, Result<int>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<TaxClassCreateHandler> _logger;

    /// <summary>
    /// Repository for tax class data operations
    /// </summary>
    private readonly ITaxClassRepository _taxClassRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="taxClassRepository">Repository for tax class data access</param>
    public TaxClassCreateHandler(
        IAppLogger<TaxClassCreateHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _taxClassRepository = taxClassRepository ?? throw new ArgumentNullException(nameof(taxClassRepository));
    }

    /// <summary>
    /// Handles the tax class creation request
    /// </summary>
    /// <param name="request">The tax class creation command with tax class details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the newly created tax class if successful</returns>
    public async Task<Result<int>> Handle(TaxClassCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new tax class with tax rate: {TaxRate}", request.TaxRate);

        var result = new Result<int>();

        // Validate incoming data
        var validator = new TaxClassCreateValidator(_taxClassRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation error messages
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in create request for {0}: {1}",
                nameof(TaxClassCreateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Manual mapping to domain entity
            var taxClassToCreate = new Domain.Entities.TaxClass
            {
                TaxRate = request.TaxRate
            };

            // Add the new tax class to the database
            await _taxClassRepository.CreateAsync(taxClassToCreate);

            // Set successful result with the new tax class ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = taxClassToCreate.Id;

            _logger.LogInformation("Successfully created tax class with ID: {Id}", taxClassToCreate.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during tax class creation
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the tax class: {ex.Message}");

            _logger.LogError("Error creating tax class: {Message}", ex.Message);
        }

        return result;
    }
}