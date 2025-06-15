using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.TaxClassDetail;

/// <summary>
/// Handler for processing tax class detail queries.
/// Implements IRequestHandler from MediatR to handle TaxClassDetailQuery requests
/// and return detailed tax class information wrapped in a Result.
/// </summary>
public class TaxClassDetailHandler : IRequestHandler<TaxClassDetailQuery, Result<TaxClassDetailDto>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<TaxClassDetailHandler> _logger;

    /// <summary>
    /// Repository for tax class data operations
    /// </summary>
    private readonly ITaxClassRepository _taxClassRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="taxClassRepository">Repository for tax class data access</param>
    public TaxClassDetailHandler(
        IAppLogger<TaxClassDetailHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _taxClassRepository = taxClassRepository ?? throw new ArgumentNullException(nameof(taxClassRepository));
    }

    /// <summary>
    /// Handles the tax class detail query request
    /// </summary>
    /// <param name="request">The query containing the tax class ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing detailed tax class information if successful</returns>
    public async Task<Result<TaxClassDetailDto>> Handle(TaxClassDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving tax class details for ID: {Id}", request.Id);

        var result = new Result<TaxClassDetailDto>();

        try
        {
            // Retrieve tax class with all related details from the repository
            var taxClass = await _taxClassRepository.GetByIdAsync(request.Id, true);

            // If tax class not found, return a not found result
            if (taxClass == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Tax class with ID {request.Id} not found");

                _logger.LogWarning("Tax class with ID {Id} not found", request.Id);
                return result;
            }

            // Manual mapping from entity to DTO
            var data = new TaxClassDetailDto
            {
                Id = taxClass.Id,
                TaxRate = taxClass.TaxRate
            };

            // Set successful result with the tax class details
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;

            _logger.LogInformation("Tax class with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during tax class retrieval
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the tax class: {ex.Message}");

            _logger.LogError("Error retrieving tax class: {Message}", ex.Message);
        }

        return result;
    }
}