using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.GetSalesChannelDetail;

public class GetSalesChannelDetailHandler : IRequestHandler<GetSalesChannelDetailQuery, GetSalesChannelDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetSalesChannelDetailHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;

    public GetSalesChannelDetailHandler(IMapper mapper,
        IAppLogger<GetSalesChannelDetailHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _salesChannelRepository = salesChannelRepository;
    }
    public async Task<GetSalesChannelDetailResponse> Handle(GetSalesChannelDetailQuery request, CancellationToken cancellationToken)
    {
        var salesChannel = await _salesChannelRepository.GetByIdAsync(request.Id, true);

        if(salesChannel == null)
        {
            throw new NotFoundException("NotFoundException", "SalesChannel not found.");
        }

        var data = _mapper.Map<GetSalesChannelDetailResponse>(salesChannel);

        _logger.LogInformation("All SalesChanneles are retrieved successfully.");
        return data;
    }
}