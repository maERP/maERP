using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.SalesChannel.Commands.UpdateSalesChannelCommand;

public class UpdateSalesChannelCommandValidator : AbstractValidator<UpdateSalesChannelCommand>
{
    private readonly ISalesChannelRepository _salesChannelRepository;

    public UpdateSalesChannelCommandValidator(ISalesChannelRepository salesChannelRepository)
    {
        _salesChannelRepository = salesChannelRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.Name)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(s => s)
            .Must(IsUnique).WithMessage("Sales channel not found.");
    }
    
    private bool IsUnique(UpdateSalesChannelCommand command)
    {
        var salesChannel = new maERP.Domain.Models.SalesChannel
        {
            Name = command.Name
        };

        return _salesChannelRepository.IsUnique(salesChannel, command.Id);
    }
}