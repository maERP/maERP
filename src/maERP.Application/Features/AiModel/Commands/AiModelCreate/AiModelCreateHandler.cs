using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using MediatR;
// using ValidationException = maERP.Application.Exceptions.ValidationException;

namespace maERP.Application.Features.AiModel.Commands.AiModelCreate;

/// <summary>
/// Handler for processing AI model creation commands.
/// Implements IRequestHandler from MediatR to handle AiModelCreateCommand requests
/// and return the ID of the newly created AI model wrapped in a Result.
/// </summary>
public class AiModelCreateHandler : IRequestHandler<AiModelCreateCommand, Result<int>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<AiModelCreateHandler> _logger;
    
    /// <summary>
    /// Repository for AI model data operations
    /// </summary>
    private readonly IAiModelRepository _aiModelRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="aiModelRepository">Repository for AI model data access</param>
    public AiModelCreateHandler(
        IAppLogger<AiModelCreateHandler> logger,
        IAiModelRepository aiModelRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _aiModelRepository = aiModelRepository ?? throw new ArgumentNullException(nameof(aiModelRepository));
    }

    /// <summary>
    /// Handles the AI model creation request
    /// </summary>
    /// <param name="request">The AI model creation command with model details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the newly created AI model if successful</returns>
    public async Task<Result<int>> Handle(AiModelCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new AI model with name: {Name}", request.Name);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new AiModelCreateValidator(_aiModelRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation error messages
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
        
        // Validate that the AI model type is a valid enum value
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
            // Direct manual mapping without helper class
            var aiModelToCreate = new Domain.Entities.AiModel
            {
                Name = request.Name,
                AiModelType = request.AiModelType,
                ApiUsername = request.ApiUsername,
                ApiPassword = request.ApiPassword,
                ApiKey = request.ApiKey,
                NCtx = request.NCtx
            };
            
            // Add the new AI model to the database
            await _aiModelRepository.CreateAsync(aiModelToCreate);

            // Set successful result with the new AI model ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = aiModelToCreate.Id;
            
            _logger.LogInformation("Successfully created AI model with ID: {Id}", aiModelToCreate.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during AI model creation
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the AI model: {ex.Message}");
            
            _logger.LogError("Error creating AI model: {Message}", ex.Message);
        }

        return result;
    }
}