using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AIPrompt.Commands.AIPromptDelete;

public class AIPromptDeleteHandler : IRequestHandler<AIPromptDeleteCommand, int>
{
    private readonly IAppLogger<AIPromptDeleteHandler> _logger;
    private readonly IAIPromptRepository _aIPromptRepository;
    
    public AIPromptDeleteHandler(
        IAppLogger<AIPromptDeleteHandler> logger,
        IAIPromptRepository aIPromptRepository)
    {
        _logger = logger;
        _aIPromptRepository = aIPromptRepository;
    }

    public async Task<int> Handle(AIPromptDeleteCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new AIPromptDeleteValidator(_aIPromptRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(AIPromptDeleteCommand), request.Id);
            throw new ValidationException("Invalid AIPrompt", validationResult);
        }

        // convert to domain entity object
        // var aIPromptToDelete = _mapper.Map<Domain.Entities.AIPrompt>(request);
        var aIPromptToDelete = new Domain.Entities.AIPrompt()
        {
            Id = request.Id
        };
        
        await _aIPromptRepository.DeleteAsync(aIPromptToDelete);
        
        return aIPromptToDelete.Id;
    }
}
