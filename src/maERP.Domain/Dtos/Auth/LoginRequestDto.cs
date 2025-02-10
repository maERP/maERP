using System.ComponentModel.DataAnnotations;

namespace maERP.Domain.Dtos.Auth;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(15, ErrorMessage = "Your password limited from {2} to {1} characters", MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Url]
    public string Server { get; set; } = string.Empty;

}