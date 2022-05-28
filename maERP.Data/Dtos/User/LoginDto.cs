#nullable disable

using System.ComponentModel.DataAnnotations;

namespace maERP.Data.Dtos.User
{
    public class LoginDto
    {
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[StringLength(15, ErrorMessage = "Your password limited from {2} to {1} characters", MinimumLength = 8)]
		public string Password { get; set; }
	}
}