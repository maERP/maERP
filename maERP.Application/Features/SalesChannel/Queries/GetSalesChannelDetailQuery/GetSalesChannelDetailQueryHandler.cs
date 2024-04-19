using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.SalesChannel;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.GetSalesChannelDetailQuery;

public class GetSalesChannelDetailQueryHandler : IRequestHandler<GetSalesChannelDetailQuery, SalesChannelDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetSalesChannelDetailQueryHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;

    public GetSalesChannelDetailQueryHandler(IMapper mapper,
        IAppLogger<GetSalesChannelDetailQueryHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _salesChannelRepository = salesChannelRepository;
    }
    public async Task<SalesChannelDetailDto> Handle(GetSalesChannelDetailQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var salesChannel = await _salesChannelRepository.GetByIdAsync(request.Id);

        // Convert data objects to DTO objects
        var data = _mapper.Map<SalesChannelDetailDto>(salesChannel);

        // Return list of DTO objects
        _logger.LogInformation("All SalesChanneles are retrieved successfully.");
        return data;
    }
}