using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiModel.Queries.AiModelList;

// ReSharper disable once UnusedType.Global
public class AiModelListHandler : IRequestHandler<AiModelListQuery, PaginatedResult<AiModelListDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AiModelListHandler> _logger;
    private readonly IAiModelRepository _aiModelRepository;

    public AiModelListHandler(IMapper mapper,
        IAppLogger<AiModelListHandler> logger, 
        IAiModelRepository aiModelRepository)
    {
        _mapper = mapper;
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
               .ProjectTo<AiModelListDto>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        var ordering = string.Join(",", request.OrderBy);

        return await _aiModelRepository.Entities
            .Specify(aiModelFilterSpec)
            .OrderBy(ordering)
            .ProjectTo<AiModelListDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}