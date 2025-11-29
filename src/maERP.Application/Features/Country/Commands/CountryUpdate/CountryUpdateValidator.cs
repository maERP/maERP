using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Country.Commands.CountryUpdate;

public class CountryUpdateValidator : CountryBaseValidator<CountryUpdateCommand>
{
    private readonly ICountryRepository _countryRepository;

    public CountryUpdateValidator(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

        RuleFor(t => t)
            .MustAsync(IsUniqueAsync).WithMessage("Country with the same name or code already exists.");
    }

    private async Task<bool> IsUniqueAsync(CountryUpdateCommand command, CancellationToken cancellationToken)
    {
        var country = new Domain.Entities.Country
        {
            Name = command.Name,
            CountryCode = command.CountryCode
        };

        return await _countryRepository.IsUniqueAsync(country, command.Id);
    }
}
