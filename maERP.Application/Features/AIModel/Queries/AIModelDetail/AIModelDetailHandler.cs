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
    private readonly IAIModelRepository _aimodelRepository;

    public AIModelDetailHandler(IMapper mapper,
        IAppLogger<AIModelDetailHandler> logger,
        IAIModelRepository aimodelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aimodelRepository = aimodelRepository;
    }
    public async Task<AIModelDetailResponse> Handle(AIModelDetailQuery request, CancellationToken cancellationToken)
    {
        var aimodel = await _aimodelRepository.GetByIdAsync(request.Id, true);

        if (aimodel == null)
        {
            throw new NotFoundException("NotFoundException", "aimodel not found.");
        }

        var data = _mapper.Map<AIModelDetailResponse>(aimodel);

        _logger.LogInformation("AIModel retrieved successfully.");
        return data;
    }
}