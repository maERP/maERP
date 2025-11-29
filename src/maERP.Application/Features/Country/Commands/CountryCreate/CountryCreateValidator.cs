using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Country.Commands.CountryCreate;

/// <summary>
/// Validator for country creation commands.
/// Extends CountryBaseValidator to inherit common validation rules for country data
/// and adds specific validation for country creation operations.
/// </summary>
public class CountryCreateValidator : CountryBaseValidator<CountryCreateCommand>
{
    private readonly ICountryRepository _countryRepository;

    public CountryCreateValidator(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;

        // Add rule to check if the country is unique before creating
        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("Country with the same name or code already exists.");
    }

    /// <summary>
    /// Asynchronously checks if a country with the same values already exists in the database
    /// </summary>
    private async Task<bool> IsUniqueAsync(CountryCreateCommand command, CancellationToken cancellationToken)
    {
        var countryToCreate = new Domain.Entities.Country
        {
            Name = command.Name,
            CountryCode = command.CountryCode
        };

        return await _countryRepository.IsUniqueAsync(countryToCreate);
    }
}
