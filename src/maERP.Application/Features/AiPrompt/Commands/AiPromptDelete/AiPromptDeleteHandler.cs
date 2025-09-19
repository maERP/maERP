using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptDelete;

public class AiPromptDeleteHandler : IRequestHandler<AiPromptDeleteCommand, Result<Guid>>
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

    public async Task<Result<Guid>> Handle(AiPromptDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting AI prompt with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new AiPromptDeleteValidator();
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
            // Get entity from database first
            var aIPromptToDelete = await _aIPromptRepository.GetByIdAsync(request.Id);

            if (aIPromptToDelete == null)
            {
                _logger.LogWarning("AI prompt with ID: {Id} not found for deletion", request.Id);
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("AI prompt not found");
                return result;
            }

            // Delete from database
            await _aIPromptRepository.DeleteAsync(aIPromptToDelete);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.NoContent;
            result.Data = aIPromptToDelete.Id;

            _logger.LogInformation("Successfully deleted AI prompt with ID: {Id}", aIPromptToDelete.Id);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex)
        {
            // Handle concurrent deletion - prompt was already deleted by another request
            _logger.LogWarning("AI prompt with ID: {Id} was deleted by another request: {Message}", request.Id, ex.Message);

            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.NotFound;
            result.Messages.Add("AI prompt not found");
        }
        catch (InvalidOperationException ex)
        {
            // Repository signals entity was already removed (e.g. concurrent delete)
            _logger.LogWarning(
                "AI prompt with ID: {Id} not found for deletion. Reason: {Reason}",
                request.Id,
                ex.Message);

            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.NotFound;
            result.Messages.Add("AI prompt not found");
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
