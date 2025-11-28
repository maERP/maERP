using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Customer.Commands.CustomerCreate;

/// <summary>
/// Server-seitiger Validator für Customer Create Commands.
///
/// Erweitert CustomerBaseValidator (aus maERP.Domain) um Server-spezifische Validierungen:
/// - Datenbankbasierte Eindeutigkeitsprüfungen (Async mit Repository-Zugriff)
/// - Foreign Key Validierungen
/// - Address-Validierung (CountryId muss gültig sein)
///
/// WICHTIG:
/// - Basis-Regeln (Feldvalidierungen) sind in CustomerBaseValidator definiert
/// - Client verwendet CustomerClientValidator (nur synchrone Regeln)
/// - Server verwendet diesen Validator (mit DB-Zugriff)
/// </summary>
public class CustomerCreateValidator : CustomerBaseValidator<CustomerCreateCommand>
{
    /// <summary>
    /// Repository for customer data operations
    /// </summary>
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="customerRepository">Repository for customer data access</param>
    public CustomerCreateValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        // Add rule to check if the customer is unique before creating
        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("Customer with the same values already exists.");

        // Validate each address in the collection
        RuleForEach(c => c.CustomerAddresses)
            .SetValidator(new CustomerAddressBaseValidator());
    }

    /// <summary>
    /// Asynchronously checks if a customer with the same values already exists in the database
    /// </summary>
    /// <param name="command">The customer creation command to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the customer is unique, false otherwise</returns>
    private async Task<bool> IsUniqueAsync(CustomerCreateCommand command, CancellationToken cancellationToken)
    {
        var customer = new Domain.Entities.Customer
        {
            Firstname = command.Firstname,
            Lastname = command.Lastname
        };

        return await _customerRepository.IsUniqueAsync(customer);
    }
}
