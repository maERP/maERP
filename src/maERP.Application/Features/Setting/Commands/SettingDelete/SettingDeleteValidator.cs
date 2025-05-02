using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.Setting.Commands.SettingDelete;

public class SettingDeleteValidator : AbstractValidator<SettingDeleteCommand>
{
    private readonly ISettingRepository _settingRepository;

    public SettingDeleteValidator(ISettingRepository settingRepository)
    {
        _settingRepository = settingRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(s => s)
            .MustAsync(SettingExists).WithMessage("Setting not found.");
    }
    
    private async Task<bool> SettingExists(SettingDeleteCommand command, CancellationToken cancellationToken)
    {
        return await _settingRepository.ExistsAsync(command.Id);
    }
}
