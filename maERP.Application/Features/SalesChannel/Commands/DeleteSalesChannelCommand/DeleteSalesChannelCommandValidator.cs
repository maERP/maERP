using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.SalesChannel.Commands.DeleteSalesChannelCommand;

public class DeleteSalesChannelCommandValidator : AbstractValidator<DeleteSalesChannelCommand>
{
    private readonly ISalesChannelRepository _salesChannelRepository;

    public DeleteSalesChannelCommandValidator(ISalesChannelRepository salesChannelRepository)
    {
        _salesChannelRepository = salesChannelRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
    }
}
