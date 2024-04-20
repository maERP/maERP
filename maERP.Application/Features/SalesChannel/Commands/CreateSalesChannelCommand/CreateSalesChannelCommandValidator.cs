using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.SalesChannel.Commands.CreateSalesChannelCommand;

public class CreateSalesChannelCommandValidator : AbstractValidator<CreateSalesChannelCommand>
{
    private readonly ISalesChannelRepository _salesChannelRepository;

    public CreateSalesChannelCommandValidator(ISalesChannelRepository salesChannelRepository)
    {
        _salesChannelRepository = salesChannelRepository;

        RuleFor(p => p.Url)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }

    private async Task<bool> SalesChannelUnique(CreateSalesChannelCommand command, CancellationToken cancellationToken)
    {
        // TODO: Implement unique SalesChannel name validation
        await Task.CompletedTask;
        return true;
    }
}
