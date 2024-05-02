using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.SalesChannel;
using maERP.Application.Exceptions;
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
        var salesChannel = await _salesChannelRepository.GetByIdAsync(request.Id);

        if(salesChannel == null)
        {
            throw new NotFoundException("NotFoundException", "SalesChannel not found.");
        }

        var data = _mapper.Map<SalesChannelDetailDto>(salesChannel);

        _logger.LogInformation("All SalesChanneles are retrieved successfully.");
        return data;
    }
}