using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelDelete;

public class SalesChannelDeleteHandler : IRequestHandler<SalesChanneLDeleteCommand, int>
{
    private readonly IAppLogger<SalesChannelDeleteHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;


    public SalesChannelDeleteHandler(
        IAppLogger<SalesChannelDeleteHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _logger = logger;
        _salesChannelRepository = salesChannelRepository;
    }

    public async Task<int> Handle(SalesChanneLDeleteCommand request, CancellationToken cancellationToken)
    {
        var validator = new SalesChannelDeleteValidator(_salesChannelRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(SalesChanneLDeleteCommand), request.Id);
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
