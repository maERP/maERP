using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Country.Commands.CountryCreate;

/// <summary>
/// Handler for processing country creation commands.
/// Implements IRequestHandler from custom mediator to handle CountryCreateCommand requests
/// and return the ID of the newly created country wrapped in a Result.
/// </summary>
public class CountryCreateHandler : IRequestHandler<CountryCreateCommand, Result<Guid>>
{
    private readonly IAppLogger<CountryCreateHandler> _logger;
    private readonly ICountryRepository _countryRepository;

    public CountryCreateHandler(
        IAppLogger<CountryCreateHandler> logger,
        ICountryRepository countryRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
    }

    public async Task<Result<Guid>> Handle(CountryCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new country with name: {Name}, code: {CountryCode}",
            request.Name, request.CountryCode);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new CountryCreateValidator(_countryRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation error messages
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in create request for {0}: {1}",
                nameof(CountryCreateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Manual mapping to domain entity
            var countryToCreate = new Domain.Entities.Country
            {
                Name = request.Name,
                CountryCode = request.CountryCode
            };

            // Add the new country to the database
            await _countryRepository.CreateAsync(countryToCreate);

            // Set successful result with the new country ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = countryToCreate.Id;

            _logger.LogInformation("Successfully created country with ID: {Id}", countryToCreate.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during country creation
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the country: {ex.Message}");

            _logger.LogError("Error creating country: {Message}", ex.Message);
        }

        return result;
    }
}
