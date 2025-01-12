using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;

public class SalesChannelCreateHandler : IRequestHandler<SalesChannelCreateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<SalesChannelCreateHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;

    public SalesChannelCreateHandler(IMapper mapper,
        IAppLogger<SalesChannelCreateHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _salesChannelRepository = salesChannelRepository;
    }

    public async Task<int> Handle(SalesChannelCreateCommand request, CancellationToken cancellationToken)
    {
        var validator = new SalesChanneLCreateValidator(_salesChannelRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(SalesChannelCreateCommand), request.Name);
            throw new ValidationException("Invalid SalesChannel", validationResult);
        }

        var salesChannelToCreate = _mapper.Map<Domain.Entities.SalesChannel>(request);

        await _salesChannelRepository.CreateAsync(salesChannelToCreate);

        return salesChannelToCreate.Id;
    }
}