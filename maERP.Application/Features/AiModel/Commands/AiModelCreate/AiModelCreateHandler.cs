using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AiModel.Commands.AiModelCreate;

public class AiModelCreateHandler : IRequestHandler<AiModelCreateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AiModelCreateHandler> _logger;
    private readonly IAiModelRepository _aiModelRepository;

    public AiModelCreateHandler(IMapper mapper,
        IAppLogger<AiModelCreateHandler> logger,
        IAiModelRepository aiModelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aiModelRepository = aiModelRepository;
    }

    public async Task<int> Handle(AiModelCreateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new AiModelCreateValidator(_aiModelRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(AiModelCreateCommand), request.Name);
            throw new ValidationException("Invalid AiModel", validationResult);
        }

        // convert to domain entity object
        var aiModelToCreate = _mapper.Map<Domain.Entities.AiModel>(request);

        // add to database
        await _aiModelRepository.CreateAsync(aiModelToCreate);

        // return record id
        return aiModelToCreate.Id;
    }
}
