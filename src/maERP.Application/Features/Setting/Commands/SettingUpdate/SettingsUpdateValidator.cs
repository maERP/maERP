using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.Setting.Commands.SettingUpdate;

public class SettingUpdateValidator : SettingBaseValidator<SettingUpdateCommand>
{
    private readonly ISettingRepository _settingRepository;

    public SettingUpdateValidator(ISettingRepository settingRepository)
    {
        _settingRepository = settingRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

        RuleFor(s => s)
            .MustAsync(IsUnique).WithMessage("Setting is not unique.");
    }

    private async Task<bool> IsUnique(SettingUpdateCommand command, CancellationToken cancellationToken)
    {
        var setting = new Domain.Entities.Setting()
        {
            Key = command.Key
        };

        var test = await _settingRepository.IsUniqueAsync(setting, command.Id);

        return test;
    }
}