using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseCreate;

/// <summary>
/// Validator for warehouse creation commands.
/// Extends WarehouseBaseValidator to inherit common validation rules for warehouse data
/// and adds specific validation for warehouse creation operations.
/// </summary>
public class WarehouseCreateValidator : WarehouseBaseValidator<WarehouseCreateCommand>
{
    /// <summary>
    /// Repository for warehouse data operations
    /// </summary>
    private readonly IWarehouseRepository _warehouseRepository;
    
    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="warehouseRepository">Repository for warehouse data access</param>
    public WarehouseCreateValidator(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;

        // Add rule to check if the warehouse name is unique before creating
        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("Warehouse with the same name already exists.");
    }

    /// <summary>
    /// Asynchronously checks if a warehouse with the same name already exists in the database
    /// </summary>
    /// <param name="command">The warehouse creation command to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the warehouse name is unique, false otherwise</returns>
    private async Task<bool> IsUniqueAsync(WarehouseCreateCommand command, CancellationToken cancellationToken)
    {
        var warehouse = new Domain.Entities.Warehouse
        {
            Name = command.Name,
        };
        
        return await _warehouseRepository.IsUniqueAsync(warehouse);
    }
}