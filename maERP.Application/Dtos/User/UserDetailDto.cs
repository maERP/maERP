using System.ComponentModel.DataAnnotations;

namespace maERP.Application.Dtos.User;

public class UserDetailDto
{
    public virtual string? Id { get; set; }

    [Required]
    public virtual string Firstname { get; set; } = string.Empty;

    [Required]
    public virtual string Lastname { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.EmailAddress)]
    public virtual string Email { get; set; } = string.Empty;
}