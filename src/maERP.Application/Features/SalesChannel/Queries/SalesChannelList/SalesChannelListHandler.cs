using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.SalesChannelList;

public class SalesChannelListHandler : IRequestHandler<SalesChannelListQuery, PaginatedResult<SalesChannelListDto>>
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
    public async Task<PaginatedResult<SalesChannelListDto>> Handle(SalesChannelListQuery request, CancellationToken cancellationToken)
    {
        var salesChannelFilterSpec = new SalesChannelFilterSpecification(request.SearchString);
        
        _logger.LogInformation("Handle SalesChannelListQuery: {0}", request);

        if (request.OrderBy.Any() != true)
        {
            return await _salesChannelRepository.Entities
               .Specify(salesChannelFilterSpec)
               .ProjectTo<SalesChannelListDto>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        var ordering = string.Join(",", request.OrderBy);

        return await _salesChannelRepository.Entities
            .Specify(salesChannelFilterSpec)
            .OrderBy(ordering)
            .ProjectTo<SalesChannelListDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}