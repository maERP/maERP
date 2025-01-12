using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptDelete;

public class AiPromptDeleteHandler : IRequestHandler<AiPromptDeleteCommand, int>
{
    private readonly IAppLogger<AiPromptDeleteHandler> _logger;
    private readonly IAiPromptRepository _aIPromptRepository;
    
    public AiPromptDeleteHandler(
        IAppLogger<AiPromptDeleteHandler> logger,
        IAiPromptRepository aIPromptRepository)
    {
        _logger = logger;
        _aIPromptRepository = aIPromptRepository;
    }

    public async Task<int> Handle(AiPromptDeleteCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new AiPromptDeleteValidator(_aIPromptRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(AiPromptDeleteCommand), request.Id);
            throw new ValidationException("Invalid AiPrompt", validationResult);
        }

        // convert to domain entity object
        // var aIPromptToDelete = _mapper.Map<Domain.Entities.AiPrompt>(request);
        var aIPromptToDelete = new Domain.Entities.AiPrompt()
        {
            Id = request.Id
        };
        
        await _aIPromptRepository.DeleteAsync(aIPromptToDelete);
        
        return aIPromptToDelete.Id;
    }
}
