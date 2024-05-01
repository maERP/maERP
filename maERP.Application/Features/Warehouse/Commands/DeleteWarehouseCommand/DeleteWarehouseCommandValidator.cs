using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Warehouse.Commands.DeleteWarehouseCommand;

public class DeleteWarehouseCommandValidator : AbstractValidator<DeleteWarehouseCommand>
{
    private readonly IWarehouseRepository _warehouseRepository;

    public DeleteWarehouseCommandValidator(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
        
        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(w => w)
            .MustAsync(WarehouseExists).WithMessage("Warehouse not found");
        
        // TODO: Implement check if warehouse is not used in a sales channel
    }
    
    private async Task<bool> WarehouseExists(DeleteWarehouseCommand command, CancellationToken cancellationToken)
    {
        return await _warehouseRepository.ExistsAsync(command.Id);
    }
}
