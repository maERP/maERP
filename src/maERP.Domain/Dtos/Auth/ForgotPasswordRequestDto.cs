using System.ComponentModel.DataAnnotations;

namespace maERP.Domain.Dtos.Auth;

public class ForgotPasswordRequestDto
{
    [Required(ErrorMessage = "E-Mail-Adresse ist erforderlich.")]
    [EmailAddress(ErrorMessage = "Ungültige E-Mail-Adresse.")]
    public string Email { get; set; } = string.Empty;
}
