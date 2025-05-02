using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelDelete;

public class SalesChannelDeleteValidator : AbstractValidator<SalesChannelDeleteCommand>
{
    private readonly ISalesChannelRepository _salesChannelRepository;

    public SalesChannelDeleteValidator(ISalesChannelRepository salesChannelRepository)
    {
        _salesChannelRepository = salesChannelRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(s => s)
            .MustAsync(SalesChannelExists).WithMessage("SalesChannel not found.");
    }
    
    private async Task<bool> SalesChannelExists(SalesChannelDeleteCommand command, CancellationToken cancellationToken)
    {
        return await _salesChannelRepository.ExistsAsync(command.Id);
    }
}
