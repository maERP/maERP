using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Warehouse.Commands.UpdateWarehouseCommand;

public class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
{
    private readonly IWarehouseRepository _warehouseRepository;
    
    public UpdateWarehouseCommandValidator(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
        
        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(p => p.Name)
            .NotNull()
            .MinimumLength(3).WithMessage("{PropertyName} must be longer than 3.")
            .MaximumLength(255).WithMessage("{PropertyName} too long");
        
        RuleFor(w => w)
            .MustAsync(WarehouseExists).WithMessage("Warehouse not found");
    }
    
    private async Task<bool> WarehouseExists(UpdateWarehouseCommand command, CancellationToken cancellationToken)
    {
        return await _warehouseRepository.GetByIdAsync(command.Id) != null;
    }
}