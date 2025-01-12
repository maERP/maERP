using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AiModel.Commands.AiModelUpdate;

public class AiModelUpdateHandler : IRequestHandler<AiModelUpdateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AiModelUpdateHandler> _logger;
    private readonly IAiModelRepository _aiModelRepository;


    public AiModelUpdateHandler(IMapper mapper,
        IAppLogger<AiModelUpdateHandler> logger,
        IAiModelRepository aiModelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aiModelRepository = aiModelRepository;
    }

    public async Task<int> Handle(AiModelUpdateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new AiModelUpdateValidator(_aiModelRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(AiModelUpdateCommand), request.Name);
            throw new ValidationException("Invalid AiModel", validationResult);
        }
        
        var aiModelToUpdate = _mapper.Map<Domain.Entities.AiModel>(request);
        
        await _aiModelRepository.UpdateAsync(aiModelToUpdate);

        return aiModelToUpdate.Id;
    }
}
