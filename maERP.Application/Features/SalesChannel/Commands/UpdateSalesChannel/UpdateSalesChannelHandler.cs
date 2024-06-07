using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.UpdateSalesChannel;

public class UpdateSalesChannelHandler : IRequestHandler<UpdateSalesChannelCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateSalesChannelHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;


    public UpdateSalesChannelHandler(IMapper mapper,
        IAppLogger<UpdateSalesChannelHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _salesChannelRepository = salesChannelRepository;
    }

    public async Task<int> Handle(UpdateSalesChannelCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UpdateSalesChannelValidator(_salesChannelRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(UpdateSalesChannelCommand), request.Id);
            throw new ValidationException("Invalid SalesChannel", validationResult);
        }

        // convert to domain entity object
        var salesChannelToUpdate = _mapper.Map<Domain.Models.SalesChannel>(request);

        // add to database
        await _salesChannelRepository.UpdateAsync(salesChannelToUpdate);

        // return record id
        return salesChannelToUpdate.Id;
    }
}
