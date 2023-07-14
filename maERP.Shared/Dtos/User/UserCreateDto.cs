using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace maERP.Shared.Dtos.User;

public class UserCreateDto
{
	[Required]
	public virtual string FirstName { get; set; } = string.Empty;

	[Required]
	public virtual string LastName { get; set; } = string.Empty;

    [Required]
	[DataType(DataType.EmailAddress)]
	public virtual string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(15, ErrorMessage = "Das Passwort muss {2} bis {1} Zeichen haben.", MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessage = "Die Passwörter stimmen nicht überein.")]
    [JsonIgnore]
    public string PasswordConfirm { get; set; } = string.Empty;
}