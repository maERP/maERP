using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;

public class SalesChanneLCreateValidator : AbstractValidator<SalesChannelCreateCommand>
{
    private readonly ISalesChannelRepository _salesChannelRepository;

    public SalesChanneLCreateValidator(ISalesChannelRepository salesChannelRepository)
    {
        _salesChannelRepository = salesChannelRepository;
        
        RuleFor(s => s)
            .MustAsync(IsUniqueAsync).WithMessage("SalesChannel with the same name already exists.");
    }
    
    private async Task<bool> IsUniqueAsync(SalesChannelCreateCommand command, CancellationToken cancellationToken)
    {
        var salesChannel = new Domain.Entities.SalesChannel
        {
            Name = command.Name
        };

        return await _salesChannelRepository.SalesChannelIsUniqueAsync(salesChannel);
    }
}
