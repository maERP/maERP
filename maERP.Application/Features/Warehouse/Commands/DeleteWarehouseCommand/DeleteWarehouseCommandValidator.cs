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
    }

    private async Task<bool> WarehouseUnique(DeleteWarehouseCommand command, CancellationToken cancellationToken)
    {
        // TODO: Implement unique warehouse name validation
        await Task.CompletedTask;
        return true;
    }
}
