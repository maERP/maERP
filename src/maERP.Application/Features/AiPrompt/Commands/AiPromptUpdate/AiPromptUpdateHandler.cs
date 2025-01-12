using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptUpdate;

public class AiPromptUpdateHandler : IRequestHandler<AiPromptUpdateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AiPromptUpdateHandler> _logger;
    private readonly IAiPromptRepository _aIPromptRepository;


    public AiPromptUpdateHandler(IMapper mapper,
        IAppLogger<AiPromptUpdateHandler> logger,
        IAiPromptRepository aIPromptRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aIPromptRepository = aIPromptRepository;
    }

    public async Task<int> Handle(AiPromptUpdateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new AiPromptUpdateValidator(_aIPromptRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(AiPromptUpdateCommand), request.Identifier);
            throw new ValidationException("Invalid AiPrompt", validationResult);
        }
        
        var aIPromptToUpdate = _mapper.Map<Domain.Entities.AiPrompt>(request);
        
        await _aIPromptRepository.UpdateAsync(aIPromptToUpdate);

        return aIPromptToUpdate.Id;
    }
}
