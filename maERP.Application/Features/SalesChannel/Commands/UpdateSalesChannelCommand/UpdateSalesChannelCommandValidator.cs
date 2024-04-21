using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.SalesChannel.Commands.UpdateSalesChannelCommand;

public class UpdateSalesChannelCommandValidator : AbstractValidator<UpdateSalesChannelCommand>
{
    private readonly ISalesChannelRepository _salesChannelRepository;

    public UpdateSalesChannelCommandValidator(ISalesChannelRepository salesChannelRepository)
    {
        _salesChannelRepository = salesChannelRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.TaxRate)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(s => s)
            .MustAsync(SalesChannelExists).WithMessage("Sales channel not found.");
    }
    
    private async Task<bool> SalesChannelExists(UpdateSalesChannelCommand command, CancellationToken cancellationToken)
    {
        return await _salesChannelRepository.GetByIdAsync(command.Id) != null;
    }
}