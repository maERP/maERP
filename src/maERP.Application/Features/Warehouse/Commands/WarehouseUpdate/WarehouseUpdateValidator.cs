﻿using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseUpdate;

public class WarehouseUpdateValidator : AbstractValidator<WarehouseUpdateCommand>
{
    private readonly IWarehouseRepository _warehouseRepository;
    
    public WarehouseUpdateValidator(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
        
        RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} must not be null.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(p => p.Name)
            .NotNull()
            .MinimumLength(3).WithMessage("{PropertyName} must be longer than 3.")
            .MaximumLength(255).WithMessage("{PropertyName} too long");

        RuleFor(w => w)
            .MustAsync(WarehouseExists).WithMessage("Warehouse not found")
            .MustAsync(IsUniqueAsync).WithMessage("Warehouse with the same name already exists.");
    }
    
    private async Task<bool> WarehouseExists(WarehouseUpdateCommand command, CancellationToken cancellationToken)
    {
        return await _warehouseRepository.GetByIdAsync(command.Id, true) != null;
    }
    
    private async Task<bool> IsUniqueAsync(WarehouseUpdateCommand command, CancellationToken cancellationToken)
    {
        var warehouse = new Domain.Entities.Warehouse
        {
            Name = command.Name,
        };

        return await _warehouseRepository.IsUniqueAsync(warehouse, command.Id);
    }
}