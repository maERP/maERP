using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.SalesChannel.Commands.DeleteSalesChannel;

public class DeleteSalesChannelValidator : AbstractValidator<DeleteSalesChannelCommand>
{
    private readonly ISalesChannelRepository _salesChannelRepository;

    public DeleteSalesChannelValidator(ISalesChannelRepository salesChannelRepository)
    {
        _salesChannelRepository = salesChannelRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(s => s)
            .MustAsync(SalesChannelExists).WithMessage("SalesChannel not found.");
    }
    
    private async Task<bool> SalesChannelExists(DeleteSalesChannelCommand command, CancellationToken cancellationToken)
    {
        return await _salesChannelRepository.ExistsAsync(command.Id);
    }
}
