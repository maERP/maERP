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
        
        RuleFor(s => s)
            .MustAsync(SalesChannelExists).WithMessage("SalesChannel with the same Id already exists.");
    }
    
    private async Task<bool> SalesChannelExists(CreateSalesChannelCommand command, CancellationToken cancellationToken)
    {
        // TODO fix fixed integer
        return await _salesChannelRepository.GetByIdAsync(1) == null;
    }
}
