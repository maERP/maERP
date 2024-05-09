using System.ComponentModel.DataAnnotations;

namespace maERP.Application.Dtos.User;

public class UserListDto
{
    public string? Id { get; set; }

    [Required]
    public string Firstname { get; set; } = string.Empty;

    [Required]
    public string Lastname { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
}