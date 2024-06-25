using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AIPrompt.Queries.AIPromptList;

// ReSharper disable once UnusedType.Global
public class AIPromptListHandler : IRequestHandler<AIPromptListQuery, PaginatedResult<AIPromptListResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AIPromptListHandler> _logger;
    private readonly IAIPromptRepository _aiPromptRepository;

    public AIPromptListHandler(IMapper mapper,
        IAppLogger<AIPromptListHandler> logger, 
        IAIPromptRepository aiPromptRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aiPromptRepository = aiPromptRepository; 
    }
    public async Task<PaginatedResult<AIPromptListResponse>> Handle(AIPromptListQuery request, CancellationToken cancellationToken)
    {
        var aiPromptFilterSpec = new AIPromptFilterSpecification(request.SearchString);
        
        _logger.LogInformation("Handle AIPromptListQuery: {0}", request);

        if (request.OrderBy.Any() != true)
        {
            return await _aiPromptRepository.Entities
               .Specify(aiPromptFilterSpec)
               .ProjectTo<AIPromptListResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            var ordering = string.Join(",", request.OrderBy);

            return await _aiPromptRepository.Entities
               .Specify(aiPromptFilterSpec)
               .OrderBy(ordering)
               .ProjectTo<AIPromptListResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}