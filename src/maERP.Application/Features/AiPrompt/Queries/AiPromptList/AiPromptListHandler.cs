using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Queries.AiPromptList;

// ReSharper disable once UnusedType.Global
public class AiPromptListHandler : IRequestHandler<AiPromptListQuery, PaginatedResult<AiPromptListResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AiPromptListHandler> _logger;
    private readonly IAiPromptRepository _aiPromptRepository;

    public AiPromptListHandler(IMapper mapper,
        IAppLogger<AiPromptListHandler> logger, 
        IAiPromptRepository aiPromptRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aiPromptRepository = aiPromptRepository; 
    }
    public async Task<PaginatedResult<AiPromptListResponse>> Handle(AiPromptListQuery request, CancellationToken cancellationToken)
    {
        var aiPromptFilterSpec = new AiPromptFilterSpecification(request.SearchString);
        
        _logger.LogInformation("Handle AiPromptListQuery: {0}", request);

        if (request.OrderBy.Any() != true)
        {
            return await _aiPromptRepository.Entities
               .Specify(aiPromptFilterSpec)
               .ProjectTo<AiPromptListResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            var ordering = string.Join(",", request.OrderBy);

            return await _aiPromptRepository.Entities
               .Specify(aiPromptFilterSpec)
               .OrderBy(ordering)
               .ProjectTo<AiPromptListResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}