using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Country.Commands.CountryUpdate;

public class CountryUpdateHandler : IRequestHandler<CountryUpdateCommand, Result<Guid>>
{
    private readonly IAppLogger<CountryUpdateHandler> _logger;
    private readonly ICountryRepository _countryRepository;

    public CountryUpdateHandler(
        IAppLogger<CountryUpdateHandler> logger,
        ICountryRepository countryRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
    }

    public async Task<Result<Guid>> Handle(CountryUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating country with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new CountryUpdateValidator(_countryRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(CountryUpdateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Get the country for tracking (required for update)
            var countryToUpdate = await _countryRepository.GetByIdAsync(request.Id, true);
            if (countryToUpdate == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Country not found or access denied due to tenant isolation.");

                _logger.LogWarning("Country with ID {Id} not found or access denied due to tenant isolation", request.Id);
                return result;
            }

            // Update the existing entity properties
            countryToUpdate.Name = request.Name;
            countryToUpdate.CountryCode = request.CountryCode;

            // Update in database
            await _countryRepository.UpdateAsync(countryToUpdate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = countryToUpdate.Id;

            _logger.LogInformation("Successfully updated country with ID: {Id}", countryToUpdate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the country: {ex.Message}");

            _logger.LogError("Error updating country: {Message}", ex.Message);
        }

        return result;
    }
}
