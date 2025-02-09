using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using maERP.Domain.Dtos.AiModel;
using MediatR;

namespace maERP.Application.Features.AiModel.Queries.AiModelDetail;

public class AiModelDetailHandler : IRequestHandler<AiModelDetailQuery, AiModelDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AiModelDetailHandler> _logger;
    private readonly IAiModelRepository _aiModelRepository;

    public AiModelDetailHandler(IMapper mapper,
        IAppLogger<AiModelDetailHandler> logger,
        IAiModelRepository aiModelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aiModelRepository = aiModelRepository;
    }
    public async Task<AiModelDetailDto> Handle(AiModelDetailQuery request, CancellationToken cancellationToken)
    {
        var aiModel = await _aiModelRepository.GetByIdAsync(request.Id, true);

        if (aiModel == null)
        {
            throw new NotFoundException("NotFoundException", "aiModel not found.");
        }

        var data = _mapper.Map<AiModelDetailDto>(aiModel);

        _logger.LogInformation("AiModel retrieved successfully.");
        return data;
    }
}