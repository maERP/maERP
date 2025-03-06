using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelUpdate;

public class SalesChannelUpdateValidator : SalesChannelBaseValidator<SalesChannelInputCommand>
{
    private readonly ISalesChannelRepository _salesChannelRepository;

    public SalesChannelUpdateValidator(ISalesChannelRepository salesChannelRepository)
    {
        _salesChannelRepository = salesChannelRepository;
        
        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(s => s)
            .MustAsync(IsUnique).WithMessage("Sales Channel is not unique.");
    }
    
    private async Task<bool> IsUnique(SalesChannelInputCommand command, CancellationToken cancellationToken)
    {
        var salesChannel = new Domain.Entities.SalesChannel
        {
            Name = command.Name
        };

        var test = await _salesChannelRepository.SalesChannelIsUniqueAsync(salesChannel, command.Id);

        return test;
    }
}