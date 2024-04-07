using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Features.SalesChannel.Commands.CreateSalesChannelCommand;

namespace maERP.Application.Features.SalesChannel.Commands.CreateSalesChannelCommand;

public class CreateSalesChannelCommandValidator : AbstractValidator<CreateSalesChannelCommand>
{
    private readonly ISalesChannelRepository _salesChannelRepository;

    public CreateSalesChannelCommandValidator(ISalesChannelRepository salesChannelRepository)
    {
        _salesChannelRepository = salesChannelRepository;

        RuleFor(p => p.TaxRate)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .InclusiveBetween(0, 100).WithMessage("{PropertyName} must between 0 and 100.");
    }

    private async Task<bool> SalesChannelUnique(CreateSalesChannelCommand command, CancellationToken cancellationToken)
    {
        // TODO: Implement unique SalesChannel name validation
        await Task.CompletedTask;
        return true;
    }
}
