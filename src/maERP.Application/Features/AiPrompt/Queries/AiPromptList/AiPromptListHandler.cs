using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Queries.AiPromptList;

// ReSharper disable once UnusedType.Global
public class AiPromptListHandler : IRequestHandler<AiPromptListQuery, PaginatedResult<AiPromptListDto>>
{
    private readonly IAppLogger<AiPromptListHandler> _logger;
    private readonly IAiPromptRepository _aiPromptRepository;

    public AiPromptListHandler(
        IAppLogger<AiPromptListHandler> logger, 
        IAiPromptRepository aiPromptRepository)
    {
        _logger = logger;
        _aiPromptRepository = aiPromptRepository; 
    }
    public async Task<PaginatedResult<AiPromptListDto>> Handle(AiPromptListQuery request, CancellationToken cancellationToken)
    {
        var aiPromptFilterSpec = new AiPromptFilterSpecification(request.SearchString);
        
        _logger.LogInformation("Handle AiPromptListQuery: {0}", request);

        if (request.OrderBy.Any() != true)
        {
            return await _aiPromptRepository.Entities
               .Specify(aiPromptFilterSpec)
               .Select(a => new AiPromptListDto
               {
                   Id = a.Id,
                   Identifier = a.Identifier,
                   PromptText = a.PromptText,
                   DateCreated = a.DateCreated,
                   DateModified = a.DateModified
               })
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        var ordering = string.Join(",", request.OrderBy);

        return await _aiPromptRepository.Entities
            .Specify(aiPromptFilterSpec)
            .OrderBy(ordering)
            .Select(a => new AiPromptListDto
            {
                Id = a.Id,
                Identifier = a.Identifier,
                PromptText = a.PromptText,
                DateCreated = a.DateCreated,
                DateModified = a.DateModified
            })
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}