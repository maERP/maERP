using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AIModel.Commands.AIModelDelete;

public class AIModelDeleteHandler : IRequestHandler<AIModelDeleteCommand, int>
{
    private readonly IAppLogger<AIModelDeleteHandler> _logger;
    private readonly IAIModelRepository _aiModelRepository;
    
    public AIModelDeleteHandler(
        IAppLogger<AIModelDeleteHandler> logger,
        IAIModelRepository aiModelRepository)
    {
        _logger = logger;
        _aiModelRepository = aiModelRepository;
    }

    public async Task<int> Handle(AIModelDeleteCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new AIModelDeleteValidator(_aiModelRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(AIModelDeleteCommand), request.Id);
            throw new ValidationException("Invalid AIModel", validationResult);
        }

        // convert to domain entity object
        // var aiModelToDelete = _mapper.Map<Domain.Entities.AIModel>(request);
        var aiModelToDelete = new Domain.Entities.AIModel()
        {
            Id = request.Id
        };
        
        await _aiModelRepository.DeleteAsync(aiModelToDelete);
        
        return aiModelToDelete.Id;
    }
}
