using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptCreate;

public class AiPromptCreateHandler : IRequestHandler<AiPromptCreateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AiPromptCreateHandler> _logger;
    private readonly IAiPromptRepository _aIPromptRepository;

    public AiPromptCreateHandler(IMapper mapper,
        IAppLogger<AiPromptCreateHandler> logger,
        IAiPromptRepository aIPromptRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aIPromptRepository = aIPromptRepository;
    }

    public async Task<int> Handle(AiPromptCreateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new AiPromptCreateValidator(_aIPromptRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(AiPromptCreateCommand), request.Identifier);
            throw new ValidationException("Invalid AiPrompt", validationResult);
        }

        // convert to domain entity object
        var aIPromptToCreate = _mapper.Map<Domain.Entities.AiPrompt>(request);

        // add to database
        await _aIPromptRepository.CreateAsync(aIPromptToCreate);

        // return record id
        return aIPromptToCreate.Id;
    }
}
