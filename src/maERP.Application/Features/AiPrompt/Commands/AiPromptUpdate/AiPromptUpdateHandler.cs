using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptUpdate;

public class AiPromptUpdateHandler : IRequestHandler<AiPromptUpdateCommand, Result<Guid>>
{
    private readonly IAppLogger<AiPromptUpdateHandler> _logger;
    private readonly IAiPromptRepository _aIPromptRepository;
    private readonly IAiModelRepository _aiModelRepository;
    private readonly ITenantContext _tenantContext;

    public AiPromptUpdateHandler(
        IAppLogger<AiPromptUpdateHandler> logger,
        IAiPromptRepository aIPromptRepository,
        IAiModelRepository aiModelRepository,
        ITenantContext tenantContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _aIPromptRepository = aIPromptRepository ?? throw new ArgumentNullException(nameof(aIPromptRepository));
        _aiModelRepository = aiModelRepository ?? throw new ArgumentNullException(nameof(aiModelRepository));
        _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
    }

    public async Task<Result<Guid>> Handle(AiPromptUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating AI prompt with ID: {Id} and identifier: {Identifier}", request.Id, request.Identifier);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new AiPromptUpdateValidator(_aIPromptRepository, _aiModelRepository, _tenantContext);
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
            // Load existing AI prompt from database
            var aIPromptToUpdate = await _aIPromptRepository.GetByIdAsync(request.Id);

            if (aIPromptToUpdate == null)
            {
                _logger.LogWarning("AI prompt with ID {Id} not found for update", request.Id);
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("AI prompt not found.");
                return result;
            }

            // Update properties
            aIPromptToUpdate.AiModelId = request.AiModelId;
            aIPromptToUpdate.Identifier = request.Identifier;
            aIPromptToUpdate.PromptText = request.PromptText;

            // Save changes (entity is already tracked, so just save)
            await _aIPromptRepository.SaveChangesAsync();

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
