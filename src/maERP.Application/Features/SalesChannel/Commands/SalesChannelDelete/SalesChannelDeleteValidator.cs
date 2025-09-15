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
            .NotNull();
    }
}
