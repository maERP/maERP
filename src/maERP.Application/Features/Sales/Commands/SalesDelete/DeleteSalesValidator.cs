using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Sales.Commands.SalesDelete;

public class DeleteSalesValidator : AbstractValidator<DeleteSalesCommand>
{
    private readonly ISalesRepository _salesRepository;

    public DeleteSalesValidator(ISalesRepository salesRepository)
    {
        _salesRepository = salesRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

        RuleFor(o => o)
            .MustAsync(SalesExists).WithMessage("Sales not found");
    }

    private async Task<bool> SalesExists(DeleteSalesCommand command, CancellationToken cancellationToken)
    {
        return await _salesRepository.ExistsAsync(command.Id);
    }
}
