using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AIModel.Queries.AIModelDetail;

public class AIModelDetailHandler : IRequestHandler<AIModelDetailQuery, AIModelDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AIModelDetailHandler> _logger;
    private readonly IAIModelRepository _aiModelRepository;

    public AIModelDetailHandler(IMapper mapper,
        IAppLogger<AIModelDetailHandler> logger,
        IAIModelRepository aiModelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aiModelRepository = aiModelRepository;
    }
    public async Task<AIModelDetailResponse> Handle(AIModelDetailQuery request, CancellationToken cancellationToken)
    {
        var aiModel = await _aiModelRepository.GetByIdAsync(request.Id, true);

        if (aiModel == null)
        {
            throw new NotFoundException("NotFoundException", "aiModel not found.");
        }

        var data = _mapper.Map<AIModelDetailResponse>(aiModel);

        _logger.LogInformation("AIModel retrieved successfully.");
        return data;
    }
}