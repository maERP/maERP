using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AIPrompt.Commands.AIPromptCreate;

public class AIPromptCreateHandler : IRequestHandler<AIPromptCreateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AIPromptCreateHandler> _logger;
    private readonly IAIPromptRepository _aIPromptRepository;

    public AIPromptCreateHandler(IMapper mapper,
        IAppLogger<AIPromptCreateHandler> logger,
        IAIPromptRepository aIPromptRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aIPromptRepository = aIPromptRepository;
    }

    public async Task<int> Handle(AIPromptCreateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new AIPromptCreateValidator(_aIPromptRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(AIPromptCreateCommand), request.Identifier);
            throw new ValidationException("Invalid AIPrompt", validationResult);
        }

        // convert to domain entity object
        var aIPromptToCreate = _mapper.Map<Domain.Entities.AIPrompt>(request);

        // add to database
        await _aIPromptRepository.CreateAsync(aIPromptToCreate);

        // return record id
        return aIPromptToCreate.Id;
    }
}
