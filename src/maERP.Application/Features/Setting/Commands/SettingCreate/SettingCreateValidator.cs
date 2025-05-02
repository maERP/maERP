using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Setting.Commands.SettingCreate;

/// <summary>
/// Validator for setting creation commands.
/// Extends SettingBaseValidator to inherit common validation rules for setting data
/// and adds specific validation for setting creation operations.
/// </summary>
public class SettingCreateValidator : SettingBaseValidator<SettingCreateCommand>
{
    /// <summary>
    /// Repository for setting data operations
    /// </summary>
    private readonly ISettingRepository _settingRepository;

    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="settingRepository">Repository for setting data access</param>
    public SettingCreateValidator(ISettingRepository settingRepository)
    {
        _settingRepository = settingRepository;
        
        // Add rule to check if the setting key is unique before creating
        RuleFor(s => s)
            .MustAsync(IsUniqueAsync).WithMessage("Setting with the same name already exists.");
    }
    
    /// <summary>
    /// Asynchronously checks if a setting with the same name already exists in the database
    /// </summary>
    /// <param name="command">The setting creation command to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the setting name is unique, false otherwise</returns>
    private async Task<bool> IsUniqueAsync(SettingCreateCommand command, CancellationToken cancellationToken)
    {
        var setting = new Domain.Entities.Setting()
        {
            Key = command.Key,
            Value = command.Value
        };

        return await _settingRepository.IsUniqueAsync(setting);
    }
}