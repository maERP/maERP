using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelUpdate;

public class SalesChannelUpdateValidator : AbstractValidator<SalesChannelUpdateCommand>
{
    private readonly ISalesChannelRepository _salesChannelRepository;

    public SalesChannelUpdateValidator(ISalesChannelRepository salesChannelRepository)
    {
        _salesChannelRepository = salesChannelRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.Name)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(s => s)
            .MustAsync(IsUnique).WithMessage("Sales Channel is not unique.");
    }
    
    private async Task<bool> IsUnique(SalesChannelUpdateCommand command, CancellationToken cancellationToken)
    {
        var salesChannel = new Domain.Entities.SalesChannel
        {
            Name = command.Name
        };

        var test = await _salesChannelRepository.SalesChannelIsUniqueAsync(salesChannel, command.Id);

        return test;
    }
}