using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using maERP.Domain.Dtos.AiPrompt;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Queries.AiPromptDetail;

public class AiPromptDetailHandler : IRequestHandler<AiPromptDetailQuery, AiPromptDetailDto>
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
    public async Task<AiPromptDetailDto> Handle(AiPromptDetailQuery request, CancellationToken cancellationToken)
    {
        var aiPrompt = await _aiPromptRepository.GetByIdAsync(request.Id, true);

        if (aiPrompt == null)
        {
            throw new NotFoundException("NotFoundException", "aiPrompt not found.");
        }

        var data = _mapper.Map<AiPromptDetailDto>(aiPrompt);

        _logger.LogInformation("AiPrompt retrieved successfully.");
        return data;
    }
}