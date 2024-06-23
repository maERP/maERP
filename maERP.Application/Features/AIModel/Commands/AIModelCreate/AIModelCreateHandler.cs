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
    private readonly IAIModelRepository _aimodelRepository;

    public AIModelCreateHandler(IMapper mapper,
        IAppLogger<AIModelCreateHandler> logger,
        IAIModelRepository aimodelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aimodelRepository = aimodelRepository;
    }

    public async Task<int> Handle(AIModelCreateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new AIModelCreateValidator(_aimodelRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(AIModelCreateCommand), request.Name);
            throw new ValidationException("Invalid AIModel", validationResult);
        }

        // convert to domain entity object
        var aimodelToCreate = _mapper.Map<Domain.Entities.AIModel>(request);

        // add to database
        await _aimodelRepository.CreateAsync(aimodelToCreate);

        // return record id
        return aimodelToCreate.Id;
    }
}
