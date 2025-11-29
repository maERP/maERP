using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Country;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Country.Queries.CountryDetail;

/// <summary>
/// Handler for processing country detail queries.
/// Implements IRequestHandler from custom mediator to handle CountryDetailQuery requests
/// and return detailed country information wrapped in a Result.
/// </summary>
public class CountryDetailHandler : IRequestHandler<CountryDetailQuery, Result<CountryDetailDto>>
{
    private readonly IAppLogger<CountryDetailHandler> _logger;
    private readonly ICountryRepository _countryRepository;

    public CountryDetailHandler(
        IAppLogger<CountryDetailHandler> logger,
        ICountryRepository countryRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
    }

    public async Task<Result<CountryDetailDto>> Handle(CountryDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving country details for ID: {Id}", request.Id);

        var result = new Result<CountryDetailDto>();

        try
        {
            // Retrieve country with all related details from the repository
            var country = await _countryRepository.GetByIdAsync(request.Id, true);

            // If country not found, return a not found result
            if (country == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Country with ID {request.Id} not found");

                _logger.LogWarning("Country with ID {Id} not found", request.Id);
                return result;
            }

            // Manual mapping from entity to DTO
            var data = new CountryDetailDto
            {
                Id = country.Id,
                Name = country.Name,
                CountryCode = country.CountryCode
            };

            // Set successful result with the country details
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;

            _logger.LogInformation("Country with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during country retrieval
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the country: {ex.Message}");

            _logger.LogError("Error retrieving country: {Message}", ex.Message);
        }

        return result;
    }
}
