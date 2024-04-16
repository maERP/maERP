using Microsoft.Extensions.Localization;
using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;

namespace maERP.SharedUI.Models;

public class LoginFormModel
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required, Url]
    public string Server { get; set; } = string.Empty;
    public bool RememberMe { get; set; } = false;
}

public class LoginServer
{
    [Required, Url]
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
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(_localizer["Die E-Mail Adresse darf nicht leer sein."])
            .Length(2, 100).WithMessage(_localizer["Die E-Mail Adresse muss zwischen 2 und 100 Zeichen lang sein."]);
        RuleFor(p => p.Password)
            .NotEmpty().WithMessage(_localizer["Das Passwort darf nicht leer sein"])
            .MinimumLength(8).WithMessage(_localizer["Das Passwort muss mindestens 8 Zeichen haben."]);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        FluentValidation.Results.ValidationResult? result =
            await ValidateAsync(ValidationContext<LoginFormModel>.CreateWithOptions((LoginFormModel)model,
                x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}