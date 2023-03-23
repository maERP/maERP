using System.ComponentModel.DataAnnotations;

namespace maERP.Shared.Dtos.User;

public class LoginDto
{
	[Required]
	[EmailAddress]
	public string Email { get; set; } = String.Empty;

	[Required]
	[StringLength(15, ErrorMessage = "Your password limited from {2} to {1} characters", MinimumLength = 8)]
	public string Password { get; set; } = String.Empty;

    [Required]
    [Url]
    public string Server { get; set; } = String.Empty;

}