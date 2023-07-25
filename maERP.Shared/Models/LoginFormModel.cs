using Microsoft.Extensions.Localization;
using FluentValidation;
using FluentValidation.Results;

namespace maERP.Shared.Models;

public class LoginFormModel
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Server { get; set; } = string.Empty;
    public bool RememberMe { get; set; } = false;
}

public class LoginServer
{
    public string Url { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public DateTime LastUsed { get; set; }
}

public class LoginFormModelFluentValidator : AbstractValidator<LoginFormModel>
{
    private readonly IStringLocalizer<LoginFormModelFluentValidator> _localizer;

    public LoginFormModelFluentValidator(IStringLocalizer<LoginFormModelFluentValidator> localizer)
    {
        _localizer = localizer;
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage(_localizer["Your username cannot be empty."])
            .Length(2, 100).WithMessage(_localizer["Username must be between 2 and 100 characters."]);
        RuleFor(p => p.Password)
            .NotEmpty().WithMessage(_localizer["Your password cannot be empty"])
            .MinimumLength(6).WithMessage(_localizer["Your password length must be at least 6 characters."])
            .MaximumLength(16).WithMessage(_localizer["Your password length must not exceed 16 characters."]);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        ValidationResult? result =
            await ValidateAsync(ValidationContext<LoginFormModel>.CreateWithOptions((LoginFormModel)model,
                x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}