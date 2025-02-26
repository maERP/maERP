using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptDelete;

public class AiPromptDeleteHandler : IRequestHandler<AiPromptDeleteCommand, Result<int>>
{
    private readonly IAppLogger<AiPromptDeleteHandler> _logger;
    private readonly IAiPromptRepository _aIPromptRepository;
    
    public AiPromptDeleteHandler(
        IAppLogger<AiPromptDeleteHandler> logger,
        IAiPromptRepository aIPromptRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _aIPromptRepository = aIPromptRepository ?? throw new ArgumentNullException(nameof(aIPromptRepository));
    }

    public async Task<Result<int>> Handle(AiPromptDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting AI prompt with ID: {Id}", request.Id);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new AiPromptDeleteValidator(_aIPromptRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in delete request for {0}: {1}", 
                nameof(AiPromptDeleteCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Create entity to delete
            var aIPromptToDelete = new Domain.Entities.AiPrompt
            {
                Id = request.Id
            };
            
            // Delete from database
            await _aIPromptRepository.DeleteAsync(aIPromptToDelete);
            
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = aIPromptToDelete.Id;
            
            _logger.LogInformation("Successfully deleted AI prompt with ID: {Id}", aIPromptToDelete.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the AI prompt: {ex.Message}");
            
            _logger.LogError("Error deleting AI prompt: {Message}", ex.Message);
        }

        return result;
    }
}
