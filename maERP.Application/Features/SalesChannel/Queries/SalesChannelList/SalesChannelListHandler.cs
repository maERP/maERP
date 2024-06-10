using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.SalesChannelList;

public class SalesChannelListHandler : IRequestHandler<SalesChannelListQuery, List<SalesChannelListResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<SalesChannelListHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;

    public SalesChannelListHandler(IMapper mapper,
        IAppLogger<SalesChannelListHandler> logger, 
        ISalesChannelRepository salesChannelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _salesChannelRepository = salesChannelRepository; 
    }
    public async Task<List<SalesChannelListResponse>> Handle(SalesChannelListQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var salesChanneles = await _salesChannelRepository.GetAllAsync();

        // Convert data objects to DTO objects
        var data = _mapper.Map<List<SalesChannelListResponse>>(salesChanneles);

        // Return list of DTO objects
        _logger.LogInformation("All SalesChannels retrieved successfully.");
        return data;
    }
}