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
            .Must(IsUnique).WithMessage("SalesChannel with the same Id already exists.");
    }
    
    private bool IsUnique(CreateSalesChannelCommand command)
    {
        var salesChannel = new maERP.Domain.Models.SalesChannel
        {
            Name = command.Name
        };

        return _salesChannelRepository.IsUnique(salesChannel);
    }
}
