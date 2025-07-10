using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.AiModel.Queries.AiModelList;

// ReSharper disable once UnusedType.Global
public class AiModelListHandler : IRequestHandler<AiModelListQuery, PaginatedResult<AiModelListDto>>
{
    private readonly IAppLogger<AiModelListHandler> _logger;
    private readonly IAiModelRepository _aiModelRepository;

    public AiModelListHandler(
        IAppLogger<AiModelListHandler> logger,
        IAiModelRepository aiModelRepository)
    {
        _logger = logger;
        _aiModelRepository = aiModelRepository;
    }

    public async Task<PaginatedResult<AiModelListDto>> Handle(AiModelListQuery request, CancellationToken cancellationToken)
    {
        var aiModelFilterSpec = new AiModelFilterSpecification(request.SearchString);

        _logger.LogInformation("Handle AiModelListQuery: {0}", request);

        if (request.OrderBy.Any() != true)
        {
            return await _aiModelRepository.Entities
               .Specify(aiModelFilterSpec)
               .Select(a => new AiModelListDto
               {
                   Id = a.Id,
                   AiModelType = (int)a.AiModelType,
                   Name = a.Name,
                   NCtx = a.NCtx
               })
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        var ordering = string.Join(",", request.OrderBy);

        return await _aiModelRepository.Entities
            .Specify(aiModelFilterSpec)
            .OrderBy(ordering)
            .Select(a => new AiModelListDto
            {
                Id = a.Id,
                AiModelType = (int)a.AiModelType,
                Name = a.Name,
                NCtx = a.NCtx
            })
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}