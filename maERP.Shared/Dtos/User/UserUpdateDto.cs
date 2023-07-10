using System.ComponentModel.DataAnnotations;

namespace maERP.Shared.Dtos.User;

public class UserUpdateDto
{
	[Required]
	public virtual string FirstName { get; set; } = string.Empty;

	[Required]
	public virtual string LastName { get; set; } = string.Empty;

    [Required]
	[DataType(DataType.EmailAddress)]
	public virtual string Email { get; set; } = string.Empty;

    [Required]
	[StringLength(15, ErrorMessage = "Your password limited from {2} to {1} characters", MinimumLength = 8)]
	public virtual string Password { get; set; } = string.Empty;
}