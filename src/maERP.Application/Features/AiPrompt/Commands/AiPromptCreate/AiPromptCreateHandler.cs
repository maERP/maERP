using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptCreate;

public class AiPromptCreateHandler : IRequestHandler<AiPromptCreateCommand, Result<int>>
{
    private readonly IAppLogger<AiPromptCreateHandler> _logger;
    private readonly IAiPromptRepository _aIPromptRepository;

    public AiPromptCreateHandler(
        IAppLogger<AiPromptCreateHandler> logger,
        IAiPromptRepository aIPromptRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _aIPromptRepository = aIPromptRepository ?? throw new ArgumentNullException(nameof(aIPromptRepository));
    }

    public async Task<Result<int>> Handle(AiPromptCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new AI prompt with identifier: {Identifier}", request.Identifier);

        var result = new Result<int>();

        // Validate incoming data
        var validator = new AiPromptCreateValidator(_aIPromptRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in create request for {0}: {1}",
                nameof(AiPromptCreateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Manuelles Mapping statt AutoMapper
            var aIPromptToCreate = new Domain.Entities.AiPrompt
            {
                AiModelId = request.AiModelId,
                Identifier = request.Identifier,
                PromptText = request.PromptText
            };

            // add to database
            await _aIPromptRepository.CreateAsync(aIPromptToCreate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = aIPromptToCreate.Id;

            _logger.LogInformation("Successfully created AI prompt with ID: {Id}", aIPromptToCreate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the AI prompt: {ex.Message}");

            _logger.LogError("Error creating AI prompt: {Message}", ex.Message);
        }

        return result;
    }
}
