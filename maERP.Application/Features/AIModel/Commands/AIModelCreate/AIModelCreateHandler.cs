using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AIModel.Commands.AIModelCreate;

public class AIModelCreateHandler : IRequestHandler<AIModelCreateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AIModelCreateHandler> _logger;
    private readonly IAIModelRepository _aiModelRepository;

    public AIModelCreateHandler(IMapper mapper,
        IAppLogger<AIModelCreateHandler> logger,
        IAIModelRepository aiModelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aiModelRepository = aiModelRepository;
    }

    public async Task<int> Handle(AIModelCreateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new AIModelCreateValidator(_aiModelRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(AIModelCreateCommand), request.Name);
            throw new ValidationException("Invalid AIModel", validationResult);
        }

        // convert to domain entity object
        var aiModelToCreate = _mapper.Map<Domain.Entities.AIModel>(request);

        // add to database
        await _aiModelRepository.CreateAsync(aiModelToCreate);

        // return record id
        return aiModelToCreate.Id;
    }
}
