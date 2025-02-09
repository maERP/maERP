using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using maERP.Domain.Dtos.SalesChannel;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.SalesChannelDetail;

public class SalesChannelDetailHandler : IRequestHandler<SalesChannelDetailQuery, SalesChannelDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<SalesChannelDetailHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;

    public SalesChannelDetailHandler(IMapper mapper,
        IAppLogger<SalesChannelDetailHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _salesChannelRepository = salesChannelRepository;
    }
    public async Task<SalesChannelDetailDto> Handle(SalesChannelDetailQuery request, CancellationToken cancellationToken)
    {
        var salesChannel = await _salesChannelRepository.GetByIdAsync(request.Id, true);

        if(salesChannel == null)
        {
            throw new NotFoundException("NotFoundException", "SalesChannel not found.");
        }

        var data = _mapper.Map<SalesChannelDetailDto>(salesChannel);

        _logger.LogInformation("SalesChannel retrieved successfully.");
        return data;
    }
}