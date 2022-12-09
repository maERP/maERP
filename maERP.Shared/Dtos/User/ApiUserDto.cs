#nullable disable

using System.ComponentModel.DataAnnotations;

namespace maERP.Shared.Dtos.User;

public class ApiUserDto
{
    public string Id { get; set; }

    [Required]
	public string FirstName { get; set; }

	[Required]
	public string LastName { get; set; }

	[Required]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; }

	[Required]
	[StringLength(15, ErrorMessage = "Your password limited from {2} to {1} characters", MinimumLength = 8)]
	public string Password { get; set; }
}