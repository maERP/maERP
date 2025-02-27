using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseCreate;

public class WarehouseCreateValidator : WarehouseBaseValidator<WarehouseCreateCommand>
{
    private readonly IWarehouseRepository _warehouseRepository;
    
    public WarehouseCreateValidator(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;

        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("Warehouse with the same name already exists.");
    }

    private async Task<bool> IsUniqueAsync(WarehouseCreateCommand command, CancellationToken cancellationToken)
    {
        var warehouse = new Domain.Entities.Warehouse
        {
            Name = command.Name,
        };
        
        return await _warehouseRepository.IsUniqueAsync(warehouse);
    }
}