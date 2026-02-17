using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Customer.Commands.CustomerCreate;

/// <summary>
/// Server-seitiger Validator für Customer Create Commands.
///
/// Erweitert CustomerBaseValidator (aus maERP.Domain) um Server-spezifische Validierungen:
/// - Address-Validierung (CountryId muss gültig sein)
///
/// WICHTIG:
/// - Basis-Regeln (Feldvalidierungen) sind in CustomerBaseValidator definiert
/// - Client verwendet CustomerClientValidator (nur synchrone Regeln)
/// - Server verwendet diesen Validator (mit DB-Zugriff)
/// - Keine Eindeutigkeitsprüfung auf Firstname+Lastname, da Namensgleichheit möglich ist
/// </summary>
public class CustomerCreateValidator : CustomerBaseValidator<CustomerCreateCommand>
{
    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="customerRepository">Repository for customer data access</param>
    public CustomerCreateValidator(ICustomerRepository customerRepository)
    {
        // Validate each address in the collection
        RuleForEach(c => c.CustomerAddresses)
            .SetValidator(new CustomerAddressBaseValidator());
    }
}
