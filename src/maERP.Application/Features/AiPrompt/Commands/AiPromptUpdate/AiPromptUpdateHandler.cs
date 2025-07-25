using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptUpdate;

public class AiPromptUpdateHandler : IRequestHandler<AiPromptUpdateCommand, Result<int>>
{
    private readonly IAppLogger<AiPromptUpdateHandler> _logger;
    private readonly IAiPromptRepository _aIPromptRepository;


    public AiPromptUpdateHandler(
        IAppLogger<AiPromptUpdateHandler> logger,
        IAiPromptRepository aIPromptRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _aIPromptRepository = aIPromptRepository ?? throw new ArgumentNullException(nameof(aIPromptRepository));
    }

    public async Task<Result<int>> Handle(AiPromptUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating AI prompt with ID: {Id} and identifier: {Identifier}", request.Id, request.Identifier);

        var result = new Result<int>();

        // Validate incoming data
        var validator = new AiPromptUpdateValidator(_aIPromptRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(AiPromptUpdateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Manuelles Mapping statt AutoMapper
            var aIPromptToUpdate = new Domain.Entities.AiPrompt
            {
                Id = request.Id,
                AiModelId = request.AiModelId,
                Identifier = request.Identifier,
                PromptText = request.PromptText
            };

            // Update in database
            await _aIPromptRepository.UpdateAsync(aIPromptToUpdate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = aIPromptToUpdate.Id;

            _logger.LogInformation("Successfully updated AI prompt with ID: {Id}", aIPromptToUpdate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the AI prompt: {ex.Message}");

            _logger.LogError("Error updating AI prompt: {Message}", ex.Message);
        }

        return result;
    }
}
