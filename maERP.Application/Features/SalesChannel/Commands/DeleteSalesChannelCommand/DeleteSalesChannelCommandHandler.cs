using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.DeleteSalesChannelCommand;

public class DeleteSalesChannelCommandHandler : IRequestHandler<DeleteSalesChannelCommand, int>
{
    private readonly IAppLogger<DeleteSalesChannelCommandHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;


    public DeleteSalesChannelCommandHandler(
        IAppLogger<DeleteSalesChannelCommandHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _logger = logger;
        _salesChannelRepository = salesChannelRepository;
    }

    public async Task<int> Handle(DeleteSalesChannelCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSalesChannelCommandValidator(_salesChannelRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(CreateSalesChannelCommand), request.Id);
            throw new ValidationException("Invalid SalesChannel", validationResult);
        }

        var salesChannelToDelete = new Domain.Models.SalesChannel
        {
            Id = request.Id
        };

        await _salesChannelRepository.DeleteAsync(salesChannelToDelete);

        return salesChannelToDelete.Id;
    }
}
