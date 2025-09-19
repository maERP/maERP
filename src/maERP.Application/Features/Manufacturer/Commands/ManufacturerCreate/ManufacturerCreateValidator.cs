using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Manufacturer.Commands.ManufacturerCreate;

/// <summary>
/// Validator for manufacturer creation commands.
/// Extends ManufacturerBaseValidator to inherit common validation rules for manufacturer data
/// and adds specific validation for manufacturer creation operations.
/// </summary>
public class ManufacturerCreateValidator : ManufacturerBaseValidator<ManufacturerCreateCommand>
{
    /// <summary>
    /// Repository for manufacturer data operations
    /// </summary>
    private readonly IManufacturerRepository _manufacturerRepository;

    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="manufacturerRepository">Repository for manufacturer data access</param>
    public ManufacturerCreateValidator(IManufacturerRepository manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository;

        // Add rule to check if the manufacturer name is unique before creating
        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("Manufacturer with the same name already exists.")
            .When(q => !string.IsNullOrEmpty(q.Name));
    }

    /// <summary>
    /// Asynchronously checks if a manufacturer with the same name already exists in the database
    /// </summary>
    /// <param name="command">The manufacturer creation command to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the manufacturer name is unique, false otherwise</returns>
    private async Task<bool> IsUniqueAsync(ManufacturerCreateCommand command, CancellationToken cancellationToken)
    {
        var manufacturer = new Domain.Entities.Manufacturer
        {
            Name = command.Name,
        };

        return await _manufacturerRepository.IsUniqueAsync(manufacturer);
    }
}