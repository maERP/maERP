using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.AiModel.Commands.AiModelCreate;

using FluentValidation;

/// <summary>
/// Validator for AI model creation commands.
/// Extends AiModelBaseValidator to inherit common validation rules for AI model data
/// and adds specific validation for AI model creation operations.
/// </summary>
public class AiModelCreateValidator : AiModelBaseValidator<AiModelCreateCommand>
{
    /// <summary>
    /// Repository for AI model data operations
    /// </summary>
    private readonly IAiModelRepository _aiModelRepository;
    
    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="aiModelRepository">Repository for AI model data access</param>
    public AiModelCreateValidator(IAiModelRepository aiModelRepository)
    {
        _aiModelRepository = aiModelRepository ?? throw new ArgumentNullException(nameof(aiModelRepository));
            
        // Add rule to check if the AI model name is unique before creating
        RuleFor(q => q)
            .MustAsync(IsUniqueAsync).WithMessage("AiModel with the same name already exists.");
    }

    /// <summary>
    /// Asynchronously checks if an AI model with the same name already exists in the database
    /// </summary>
    /// <param name="command">The AI model creation command to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the AI model name is unique, false otherwise</returns>
    private async Task<bool> IsUniqueAsync(AiModelCreateCommand command, CancellationToken cancellationToken)
    {
        var aiModel = new Domain.Entities.AiModel
        {
            Name = command.Name,
        };
        
        return await _aiModelRepository.IsUniqueAsync(aiModel);
    }
}