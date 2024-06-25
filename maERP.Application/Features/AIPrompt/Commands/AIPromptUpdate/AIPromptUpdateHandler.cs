using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AIPrompt.Commands.AIPromptUpdate;

public class AIPromptUpdateHandler : IRequestHandler<AIPromptUpdateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AIPromptUpdateHandler> _logger;
    private readonly IAIPromptRepository _aIPromptRepository;


    public AIPromptUpdateHandler(IMapper mapper,
        IAppLogger<AIPromptUpdateHandler> logger,
        IAIPromptRepository aIPromptRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aIPromptRepository = aIPromptRepository;
    }

    public async Task<int> Handle(AIPromptUpdateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new AIPromptUpdateValidator(_aIPromptRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(AIPromptUpdateCommand), request.Identifier);
            throw new ValidationException("Invalid AIPrompt", validationResult);
        }
        
        var aIPromptToUpdate = _mapper.Map<Domain.Entities.AIPrompt>(request);
        
        await _aIPromptRepository.UpdateAsync(aIPromptToUpdate);

        return aIPromptToUpdate.Id;
    }
}
