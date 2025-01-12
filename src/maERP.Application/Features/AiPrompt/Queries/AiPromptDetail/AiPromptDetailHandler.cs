using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Queries.AiPromptDetail;

public class AiPromptDetailHandler : IRequestHandler<AiPromptDetailQuery, AiPromptDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AiPromptDetailHandler> _logger;
    private readonly IAiPromptRepository _aiPromptRepository;

    public AiPromptDetailHandler(IMapper mapper,
        IAppLogger<AiPromptDetailHandler> logger,
        IAiPromptRepository aiPromptRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aiPromptRepository = aiPromptRepository;
    }
    public async Task<AiPromptDetailResponse> Handle(AiPromptDetailQuery request, CancellationToken cancellationToken)
    {
        var aiPrompt = await _aiPromptRepository.GetByIdAsync(request.Id, true);

        if (aiPrompt == null)
        {
            throw new NotFoundException("NotFoundException", "aiPrompt not found.");
        }

        var data = _mapper.Map<AiPromptDetailResponse>(aiPrompt);

        _logger.LogInformation("AiPrompt retrieved successfully.");
        return data;
    }
}