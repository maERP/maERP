using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiModel.Commands.AiModelUpdate;

public class AiModelUpdateHandler : IRequestHandler<AiModelUpdateCommand, Result<int>>
{
    private readonly IAppLogger<AiModelUpdateHandler> _logger;
    private readonly IAiModelRepository _aiModelRepository;


    public AiModelUpdateHandler(
        IAppLogger<AiModelUpdateHandler> logger,
        IAiModelRepository aiModelRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _aiModelRepository = aiModelRepository ?? throw new ArgumentNullException(nameof(aiModelRepository));
    }

    public async Task<Result<int>> Handle(AiModelUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating AI model with ID: {Id} and name: {Name}", request.Id, request.Name);

        var result = new Result<int>();

        // Validate incoming data
        var validator = new AiModelUpdateValidator(_aiModelRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(AiModelUpdateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Direktes manuelles Mapping ohne Helper-Klasse
            var aiModelToUpdate = new Domain.Entities.AiModel
            {
                Id = request.Id,
                Name = request.Name,
                AiModelType = request.AiModelType,
                ApiUsername = request.ApiUsername,
                ApiPassword = request.ApiPassword,
                ApiKey = request.ApiKey,
                NCtx = request.NCtx
            };

            // Update in database
            await _aiModelRepository.UpdateAsync(aiModelToUpdate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = aiModelToUpdate.Id;

            _logger.LogInformation("Successfully updated AI model with ID: {Id}", aiModelToUpdate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the AI model: {ex.Message}");

            _logger.LogError("Error updating AI model: {Message}", ex.Message);
        }

        return result;
    }
}
