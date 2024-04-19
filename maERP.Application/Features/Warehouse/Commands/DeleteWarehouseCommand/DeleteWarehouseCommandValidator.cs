using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Warehouse.Commands.DeleteWarehouseCommand;

public class DeleteWarehouseCommandValidator : AbstractValidator<DeleteWarehouseCommand>
{
    private readonly IWarehouseRepository _warehouseRepository;

    public DeleteWarehouseCommandValidator(IWarehouseRepository warehouseRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        // TODO: Implement check if warehouse is not used in a sales channel
    }
}
