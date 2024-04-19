using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Features.SalesChannel.Commands.DeleteSalesChannelCommand;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.DeleteSalesChannelCommand;

public class DeleteSalesChannelCommandHandler : IRequestHandler<DeleteSalesChannelCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<DeleteSalesChannelCommandHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;


    public DeleteSalesChannelCommandHandler(IMapper mapper,
        IAppLogger<DeleteSalesChannelCommandHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _salesChannelRepository = salesChannelRepository;
    }

    public async Task<int> Handle(DeleteSalesChannelCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new DeleteSalesChannelCommandValidator(_salesChannelRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(SalesChannel.Commands.CreateSalesChannelCommand), request.Id);
            throw new Exceptions.ValidationException("Invalid SalesChannel", validationResult);
        }

        // convert to domain entity object
        var salesChannelToDelete = _mapper.Map<Domain.Models.SalesChannel>(request);

        // add to database
        await _salesChannelRepository.CreateAsync(salesChannelToDelete);

        // return record id
        return salesChannelToDelete.Id;
    }
}
