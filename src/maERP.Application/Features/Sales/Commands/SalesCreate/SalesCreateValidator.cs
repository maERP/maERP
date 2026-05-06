using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;
using FluentValidation;

namespace maERP.Application.Features.Sales.Commands.SalesCreate;

/// <summary>
/// Validator for sales creation commands.
/// Extends SalesBaseValidator to inherit common validation rules for sales data
/// and adds specific validation for sales creation operations.
/// </summary>
public class SalesCreateValidator : SalesBaseValidator<SalesCreateCommand>
{
    /// <summary>
    /// Repository for sales data operations
    /// </summary>
    private readonly ISalesRepository _salesRepository;

    /// <summary>
    /// Repository for customer data operations
    /// </summary>
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="salesRepository">Repository for sales data access</param>
    /// <param name="customerRepository">Repository for customer data access</param>
    public SalesCreateValidator(ISalesRepository salesRepository, ICustomerRepository customerRepository)
    {
        _salesRepository = salesRepository;
        _customerRepository = customerRepository;

        // Add validation rule to ensure customer exists and belongs to current tenant
        RuleFor(x => x.CustomerId)
            .MustAsync(CustomerExistsAndBelongsToCurrentTenant)
            .WithMessage("Der angegebene Kunde existiert nicht oder gehört nicht zu Ihrem Tenant.");

        /*
        // Commented out uniqueness check - would be used if saless needed to be unique
        RuleFor(q => q)
            .MustAsync(SalesUniqueAsync).WithMessage("Sales with the same values already exists.");
        */
    }

    /// <summary>
    /// Asynchronously checks if a customer with the specified ID exists and belongs to the current tenant.
    /// Uses EF Core Global Query Filter to ensure tenant isolation.
    /// </summary>
    /// <param name="customerId">The customer ID to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the customer exists and belongs to current tenant, false otherwise</returns>
    private async Task<bool> CustomerExistsAndBelongsToCurrentTenant(int customerId, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByCustomerIdAsync(customerId);
        return customer != null; // EF Core Global Query Filter ensures tenant isolation
    }

    /// <summary>
    /// Asynchronously checks if an sales with the same values already exists in the database.
    /// Currently not used but kept for potential future use.
    /// </summary>
    /// <param name="command">The sales creation command to validate</param>
    /// <returns>True if the sales is unique, false otherwise</returns>
    // ReSharper disable once UnusedMember.Local
    private async Task<bool> SalesUniqueAsync(SalesCreateCommand command)
    {
        var salesToCreate = new Domain.Entities.Sales
        {
            Id = command.Id
        };

        return await _salesRepository.IsUniqueAsync(salesToCreate);
    }
}