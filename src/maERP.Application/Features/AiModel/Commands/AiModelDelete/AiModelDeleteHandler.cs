using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.AiModel.Commands.AiModelDelete;

public class AiModelDeleteHandler : IRequestHandler<AiModelDeleteCommand, Result<int>>
{
    private readonly IAppLogger<AiModelDeleteHandler> _logger;
    private readonly IAiModelRepository _aiModelRepository;

    public AiModelDeleteHandler(
        IAppLogger<AiModelDeleteHandler> logger,
        IAiModelRepository aiModelRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _aiModelRepository = aiModelRepository ?? throw new ArgumentNullException(nameof(aiModelRepository));
    }

    public async Task<Result<int>> Handle(AiModelDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting AI model with ID: {Id}", request.Id);

        var result = new Result<int>();

        // Validate incoming data
        var validator = new AiModelDeleteValidator(_aiModelRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in delete request for {0}: {1}",
                nameof(AiModelDeleteCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Create entity to delete
            var aiModelToDelete = new Domain.Entities.AiModel
            {
                Id = request.Id
            };

            // Delete from database
            await _aiModelRepository.DeleteAsync(aiModelToDelete);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = aiModelToDelete.Id;

            _logger.LogInformation("Successfully deleted AI model with ID: {Id}", aiModelToDelete.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the AI model: {ex.Message}");

            _logger.LogError("Error deleting AI model: {Message}", ex.Message);
        }

        return result;
    }
}
