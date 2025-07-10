using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.AiModel.Queries.AiModelDetail;

/// <summary>
/// Handler for processing AI model detail queries.
/// Implements IRequestHandler from MediatR to handle AiModelDetailQuery requests
/// and return detailed AI model information wrapped in a Result.
/// </summary>
public class AiModelDetailHandler : IRequestHandler<AiModelDetailQuery, Result<AiModelDetailDto>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<AiModelDetailHandler> _logger;

    /// <summary>
    /// Repository for AI model data operations
    /// </summary>
    private readonly IAiModelRepository _aiModelRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="aiModelRepository">Repository for AI model data access</param>
    public AiModelDetailHandler(
        IAppLogger<AiModelDetailHandler> logger,
        IAiModelRepository aiModelRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _aiModelRepository = aiModelRepository ?? throw new ArgumentNullException(nameof(aiModelRepository));
    }

    /// <summary>
    /// Handles the AI model detail query request
    /// </summary>
    /// <param name="request">The query containing the AI model ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing detailed AI model information if successful</returns>
    public async Task<Result<AiModelDetailDto>> Handle(AiModelDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving AI model details for ID: {Id}", request.Id);

        var result = new Result<AiModelDetailDto>();

        try
        {
            // Retrieve AI model with all related details from the repository
            var aiModel = await _aiModelRepository.GetByIdAsync(request.Id, true);

            // If AI model not found, return a not found result
            if (aiModel == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"AI model with ID {request.Id} not found");

                _logger.LogWarning("AI model with ID {Id} not found", request.Id);
                return result;
            }

            // Manual mapping instead of using AutoMapper
            var data = new AiModelDetailDto
            {
                Id = aiModel.Id,
                AiModelType = aiModel.AiModelType,
                Name = aiModel.Name,
                ApiUsername = aiModel.ApiUsername,
                ApiPassword = aiModel.ApiPassword,
                ApiKey = aiModel.ApiKey,
                NCtx = aiModel.NCtx
            };

            // Set successful result with the AI model details
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;

            _logger.LogInformation("AI model with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during AI model retrieval
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the AI model: {ex.Message}");

            _logger.LogError("Error retrieving AI model: {Message}", ex.Message);
        }

        return result;
    }
}