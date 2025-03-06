using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseUpdate;

public class WarehouseUpdateValidator : WarehouseBaseValidator<WarehouseInputCommand>
{
    private readonly IWarehouseRepository _warehouseRepository;
    
    public WarehouseUpdateValidator(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
        
        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} must not be null.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(w => w)
            .MustAsync(WarehouseExists).WithMessage("Warehouse not found")
            .MustAsync(IsUniqueAsync).WithMessage("Warehouse with the same name already exists.");
    }
    
    private async Task<bool> WarehouseExists(WarehouseInputCommand command, CancellationToken cancellationToken)
    {
        return await _warehouseRepository.GetByIdAsync(command.Id, true) != null;
    }
    
    private async Task<bool> IsUniqueAsync(WarehouseInputCommand command, CancellationToken cancellationToken)
    {
        var warehouse = new Domain.Entities.Warehouse
        {
            Name = command.Name,
        };

        return await _warehouseRepository.IsUniqueAsync(warehouse, command.Id);
    }
}