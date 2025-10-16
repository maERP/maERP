using System.ComponentModel.DataAnnotations;

namespace maERP.Domain.Dtos.Auth;

public class ResetPasswordRequestDto
{
    [Required(ErrorMessage = "E-Mail-Adresse ist erforderlich.")]
    [EmailAddress(ErrorMessage = "Ungültige E-Mail-Adresse.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Token ist erforderlich.")]
    public string Token { get; set; } = string.Empty;

    [Required(ErrorMessage = "Neues Passwort ist erforderlich.")]
    [StringLength(100, ErrorMessage = "Das Passwort muss zwischen {2} und {1} Zeichen lang sein.", MinimumLength = 6)]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Passwort-Bestätigung ist erforderlich.")]
    [Compare("NewPassword", ErrorMessage = "Die Passwörter stimmen nicht überein.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
