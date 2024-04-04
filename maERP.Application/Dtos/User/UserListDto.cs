using System.ComponentModel.DataAnnotations;

namespace maERP.Shared.Dtos.User;

public class UserListDto
{
    public virtual string? Id { get; set; }

	[Required]
	public virtual string FirstName { get; set; } = string.Empty;

	[Required]
	public virtual string LastName { get; set; } = string.Empty;

    [Required]
	[DataType(DataType.EmailAddress)]
	public virtual string Email { get; set; } = string.Empty;
}