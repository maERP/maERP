using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Warehouse.Commands.CreateWarehouseCommand;

public class CreateTaxClassCommandValidator : AbstractValidator<CreateTaxClassCommand>
{
    private readonly IWarehouseRepository _warehouseRepository;

    public CreateTaxClassCommandValidator(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(q => q)
            .MustAsync(WarehouseUnique).WithMessage("Warehouse with the same name already exists.");
    }

    private async Task<bool> WarehouseUnique(CreateTaxClassCommand command, CancellationToken cancellationToken)
    {
        // TODO: Implement unique warehouse name validation
        await Task.CompletedTask;
        return true;
    }
}
