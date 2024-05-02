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
            .MustAsync(IsUniqueAsync).WithMessage("SalesChannel with the same Id already exists.");
    }
    
    private async Task<bool> IsUniqueAsync(CreateSalesChannelCommand command, CancellationToken cancellationToken)
    {
        var salesChannel = new Domain.Models.SalesChannel
        {
            Name = command.Name
        };

        return await _salesChannelRepository.IsUniqueAsync(salesChannel);
    }
}
