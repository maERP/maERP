using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Queries.AiPromptDetail;

public class AiPromptDetailHandler : IRequestHandler<AiPromptDetailQuery, Result<AiPromptDetailDto>>
{
    private readonly IAppLogger<AiPromptDetailHandler> _logger;
    private readonly IAiPromptRepository _aiPromptRepository;

    public AiPromptDetailHandler(
        IAppLogger<AiPromptDetailHandler> logger,
        IAiPromptRepository aiPromptRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _aiPromptRepository = aiPromptRepository ?? throw new ArgumentNullException(nameof(aiPromptRepository));
    }
    
    public async Task<Result<AiPromptDetailDto>> Handle(AiPromptDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving AI prompt details for ID: {Id}", request.Id);
        
        var result = new Result<AiPromptDetailDto>();
        
        try
        {
            var aiPrompt = await _aiPromptRepository.GetByIdAsync(request.Id, true);

            if (aiPrompt == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"AI prompt with ID {request.Id} not found");
                
                _logger.LogWarning("AI prompt with ID {Id} not found", request.Id);
                return result;
            }

            // Manuelles Mapping statt AutoMapper
            var data = new AiPromptDetailDto
            {
                Id = aiPrompt.Id,
                AiModelId = aiPrompt.AiModelId,
                Identifier = aiPrompt.Identifier,
                PromptText = aiPrompt.PromptText
            };

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;
            
            _logger.LogInformation("AI prompt with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the AI prompt: {ex.Message}");
            
            _logger.LogError("Error retrieving AI prompt: {Message}", ex.Message);
        }
        
        return result;
    }
}