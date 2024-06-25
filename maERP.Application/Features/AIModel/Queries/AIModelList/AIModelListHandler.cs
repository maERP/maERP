using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AIModel.Queries.AIModelList;

// ReSharper disable once UnusedType.Global
public class AIModelListHandler : IRequestHandler<AIModelListQuery, PaginatedResult<AIModelListResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AIModelListHandler> _logger;
    private readonly IAIModelRepository _aiModelRepository;

    public AIModelListHandler(IMapper mapper,
        IAppLogger<AIModelListHandler> logger, 
        IAIModelRepository aiModelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aiModelRepository = aiModelRepository; 
    }
    public async Task<PaginatedResult<AIModelListResponse>> Handle(AIModelListQuery request, CancellationToken cancellationToken)
    {
        var aiModelFilterSpec = new AIModelFilterSpecification(request.SearchString);
        
        _logger.LogInformation("Handle AIModelListQuery: {0}", request);

        if (request.OrderBy.Any() != true)
        {
            return await _aiModelRepository.Entities
               .Specify(aiModelFilterSpec)
               .ProjectTo<AIModelListResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            var ordering = string.Join(",", request.OrderBy);

            return await _aiModelRepository.Entities
               .Specify(aiModelFilterSpec)
               .OrderBy(ordering)
               .ProjectTo<AIModelListResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}