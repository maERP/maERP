using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AIModel.Commands.AIModelDelete;

public class AIModelDeleteHandler : IRequestHandler<AIModelDeleteCommand, int>
{
    private readonly IAppLogger<AIModelDeleteHandler> _logger;
    private readonly IAIModelRepository _aimodelRepository;
    
    public AIModelDeleteHandler(
        IAppLogger<AIModelDeleteHandler> logger,
        IAIModelRepository aimodelRepository)
    {
        _logger = logger;
        _aimodelRepository = aimodelRepository;
    }

    public async Task<int> Handle(AIModelDeleteCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new AIModelDeleteValidator(_aimodelRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(AIModelDeleteCommand), request.Id);
            throw new ValidationException("Invalid AIModel", validationResult);
        }

        // convert to domain entity object
        // var aimodelToDelete = _mapper.Map<Domain.Entities.AIModel>(request);
        var aimodelToDelete = new Domain.Entities.AIModel()
        {
            Id = request.Id
        };
        
        await _aimodelRepository.DeleteAsync(aimodelToDelete);
        
        return aimodelToDelete.Id;
    }
}
