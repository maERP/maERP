using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;

public class TaxClassUpdateHandler : IRequestHandler<TaxClassUpdateCommand, Result<Guid>>
{
    private readonly IAppLogger<TaxClassUpdateHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;


    public TaxClassUpdateHandler(
        IAppLogger<TaxClassUpdateHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _taxClassRepository = taxClassRepository ?? throw new ArgumentNullException(nameof(taxClassRepository));
    }

    public async Task<Result<Guid>> Handle(TaxClassUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating tax class with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new TaxClassUpdateValidator(_taxClassRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;

            // Check if the validation error is about tax class not found
            if (validationResult.Errors.Any(e => e.ErrorMessage.Contains("TaxClass not found")))
            {
                result.StatusCode = ResultStatusCode.NotFound;
            }
            else
            {
                result.StatusCode = ResultStatusCode.BadRequest;
            }

            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(TaxClassUpdateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Check if the tax class exists and belongs to the current tenant
            var existingTaxClass = await _taxClassRepository.GetByIdAsync(request.Id, true);
            if (existingTaxClass == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("TaxClass not found or access denied due to tenant isolation.");

                _logger.LogWarning("TaxClass with ID {Id} not found or access denied due to tenant isolation", request.Id);
                return result;
            }

            // Manually map to domain entity
            var taxClassToUpdate = new Domain.Entities.TaxClass
            {
                Id = request.Id,
                TaxRate = request.TaxRate
            };

            // Update in database
            await _taxClassRepository.UpdateAsync(taxClassToUpdate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = taxClassToUpdate.Id;

            _logger.LogInformation("Successfully updated tax class with ID: {Id}", taxClassToUpdate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the tax class: {ex.Message}");

            _logger.LogError("Error updating tax class: {Message}", ex.Message);
        }

        return result;
    }
}
