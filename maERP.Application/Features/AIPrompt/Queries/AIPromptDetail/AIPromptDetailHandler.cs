using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AIPrompt.Queries.AIPromptDetail;

public class AIPromptDetailHandler : IRequestHandler<AIPromptDetailQuery, AIPromptDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AIPromptDetailHandler> _logger;
    private readonly IAIPromptRepository _aiPromptRepository;

    public AIPromptDetailHandler(IMapper mapper,
        IAppLogger<AIPromptDetailHandler> logger,
        IAIPromptRepository aiPromptRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aiPromptRepository = aiPromptRepository;
    }
    public async Task<AIPromptDetailResponse> Handle(AIPromptDetailQuery request, CancellationToken cancellationToken)
    {
        var aiPrompt = await _aiPromptRepository.GetByIdAsync(request.Id, true);

        if (aiPrompt == null)
        {
            throw new NotFoundException("NotFoundException", "aiPrompt not found.");
        }

        var data = _mapper.Map<AIPromptDetailResponse>(aiPrompt);

        _logger.LogInformation("AIPrompt retrieved successfully.");
        return data;
    }
}