using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using MediatR;
// using ValidationException = maERP.Application.Exceptions.ValidationException;

namespace maERP.Application.Features.AiModel.Commands.AiModelCreate;

public class AiModelCreateHandler : IRequestHandler<AiModelCreateCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AiModelCreateHandler> _logger;
    private readonly IAiModelRepository _aiModelRepository;

    public AiModelCreateHandler(
        IMapper mapper,
        IAppLogger<AiModelCreateHandler> logger,
        IAiModelRepository aiModelRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _aiModelRepository = aiModelRepository ?? throw new ArgumentNullException(nameof(aiModelRepository));
    }

    public async Task<Result<int>> Handle(AiModelCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new AI model with name: {Name}", request.Name);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new AiModelCreateValidator(_aiModelRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in create request for {0}: {1}", 
                nameof(AiModelCreateCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }
        
        // Validate enum value
        if (!Enum.IsDefined(typeof(AiModelType), request.AiModelType))
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.Add($"Invalid AiModelType value: {request.AiModelType}");
            
            _logger.LogWarning("Invalid AiModelType value in create request: {0}", request.AiModelType);
            
            return result;
        }

        try
        {
            // Map and create entity
            var aiModelToCreate = _mapper.Map<Domain.Entities.AiModel>(request);
            
            // Ensure correct enum mapping
            aiModelToCreate.AiModelType = request.AiModelType;
            
            await _aiModelRepository.CreateAsync(aiModelToCreate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = aiModelToCreate.Id;
            
            _logger.LogInformation("Successfully created AI model with ID: {Id}", aiModelToCreate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the AI model: {ex.Message}");
            
            _logger.LogError("Error creating AI model: {Message}", ex.Message);
        }

        return result;
    }
}