using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Customer.Commands.CustomerUpdate;

/// <summary>
/// Server-seitiger Validator für Customer Update Commands.
///
/// Erweitert CustomerBaseValidator (aus maERP.Domain) um Server-spezifische Validierungen:
/// - ID-Validierung (nicht Guid.Empty)
/// - Existenz-Prüfung (Customer muss vorhanden sein)
/// - Address-Validierung (CountryId muss gültig sein)
///
/// WICHTIG:
/// - Basis-Regeln (Feldvalidierungen) sind in CustomerBaseValidator definiert
/// - Client verwendet CustomerClientValidator (nur synchrone Regeln)
/// - Server verwendet diesen Validator (mit DB-Zugriff)
/// - Keine Eindeutigkeitsprüfung auf Firstname+Lastname, da Namensgleichheit möglich ist
/// </summary>
public class CustomerUpdateValidator : CustomerBaseValidator<CustomerUpdateCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerUpdateValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        // Add ID validation for Zero-GUID first
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty).WithMessage("Customer ID cannot be empty.");

        // Only check existence if ID is valid
        RuleFor(c => c)
            .MustAsync(CustomerExists).WithMessage("Customer not found")
            .When(c => c.Id != Guid.Empty);

        // Validate each address in the collection
        RuleForEach(c => c.CustomerAddresses)
            .SetValidator(new CustomerAddressBaseValidator());
    }

    private async Task<bool> CustomerExists(CustomerUpdateCommand command, CancellationToken cancellationToken)
    {
        return await _customerRepository.ExistsGloballyAsync(command.Id);
    }
}
