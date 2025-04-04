using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassCreate;

/// <summary>
/// Validator for tax class creation commands.
/// Extends TaxClassBaseValidator to inherit common validation rules for tax class data
/// and adds specific validation for tax class creation operations.
/// </summary>
public class TaxClassCreateValidator : TaxClassBaseValidator<TaxClassCreateCommand>
{
    /// <summary>
    /// Repository for tax class data operations
    /// </summary>
    private readonly ITaxClassRepository _taxClassRepository;

    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="taxClassRepository">Repository for tax class data access</param>
    public TaxClassCreateValidator(ITaxClassRepository taxClassRepository)
    {
        _taxClassRepository = taxClassRepository;

        // Add rule to check if the tax class rate is unique before creating
        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("TaxClass with the same tax rate already exists.");
    }

    /// <summary>
    /// Asynchronously checks if a tax class with the same tax rate already exists in the database
    /// </summary>
    /// <param name="command">The tax class creation command to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the tax class rate is unique, false otherwise</returns>
    private async Task<bool> IsUniqueAsync(TaxClassCreateCommand command, CancellationToken cancellationToken)
    {
        var taxClassToCreate = new Domain.Entities.TaxClass
        {
            TaxRate = command.TaxRate
        };

        return await _taxClassRepository.IsUniqueAsync(taxClassToCreate);
    }
}