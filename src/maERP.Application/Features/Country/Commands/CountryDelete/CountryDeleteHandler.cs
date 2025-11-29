using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Country.Commands.CountryDelete;

public class CountryDeleteHandler : IRequestHandler<CountryDeleteCommand, Result<Guid>>
{
    private readonly IAppLogger<CountryDeleteHandler> _logger;
    private readonly ICountryRepository _countryRepository;

    public CountryDeleteHandler(
        IAppLogger<CountryDeleteHandler> logger,
        ICountryRepository countryRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
    }

    public async Task<Result<Guid>> Handle(CountryDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting country with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new CountryDeleteValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in delete request for {0}: {1}",
                nameof(CountryDeleteCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Get entity from database first
            var countryToDelete = await _countryRepository.GetByIdAsync(request.Id);

            if (countryToDelete == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Country not found");

                _logger.LogWarning("Country with ID: {Id} not found for deletion", request.Id);
                return result;
            }

            // Delete from database
            await _countryRepository.DeleteAsync(countryToDelete);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = countryToDelete.Id;

            _logger.LogInformation("Successfully deleted country with ID: {Id}", countryToDelete.Id);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex)
        {
            // Handle concurrent deletion - country was already deleted by another request
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.NotFound;
            result.Messages.Add("Country not found");

            _logger.LogWarning("Country with ID: {Id} was deleted by another request: {Message}", request.Id, ex.Message);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the country: {ex.Message}");

            _logger.LogError("Error deleting country: {Message}", ex.Message);
        }

        return result;
    }
}
