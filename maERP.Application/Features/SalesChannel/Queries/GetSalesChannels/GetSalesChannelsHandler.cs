using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.SalesChannel;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.GetSalesChannelsQuery;

public class GetSalesChannelsHandler : IRequestHandler<GetSalesChannelsQuery, List<SalesChannelListDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetSalesChannelsHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;

    public GetSalesChannelsHandler(IMapper mapper,
        IAppLogger<GetSalesChannelsHandler> logger, 
        ISalesChannelRepository salesChannelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _salesChannelRepository = salesChannelRepository; 
    }
    public async Task<List<SalesChannelListDto>> Handle(GetSalesChannelsQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var salesChanneles = await _salesChannelRepository.GetAllAsync();

        // Convert data objects to DTO objects
        var data = _mapper.Map<List<SalesChannelListDto>>(salesChanneles);

        // Return list of DTO objects
        _logger.LogInformation("All SalesChannels retrieved successfully.");
        return data;
    }
}