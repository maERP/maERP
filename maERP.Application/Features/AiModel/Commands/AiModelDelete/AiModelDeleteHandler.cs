using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AiModel.Commands.AiModelDelete;

public class AiModelDeleteHandler : IRequestHandler<AiModelDeleteCommand, int>
{
    private readonly IAppLogger<AiModelDeleteHandler> _logger;
    private readonly IAiModelRepository _aiModelRepository;
    
    public AiModelDeleteHandler(
        IAppLogger<AiModelDeleteHandler> logger,
        IAiModelRepository aiModelRepository)
    {
        _logger = logger;
        _aiModelRepository = aiModelRepository;
    }

    public async Task<int> Handle(AiModelDeleteCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new AiModelDeleteValidator(_aiModelRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(AiModelDeleteCommand), request.Id);
            throw new ValidationException("Invalid AiModel", validationResult);
        }

        // convert to domain entity object
        // var aiModelToDelete = _mapper.Map<Domain.Entities.AiModel>(request);
        var aiModelToDelete = new Domain.Entities.AiModel()
        {
            Id = request.Id
        };
        
        await _aiModelRepository.DeleteAsync(aiModelToDelete);
        
        return aiModelToDelete.Id;
    }
}
