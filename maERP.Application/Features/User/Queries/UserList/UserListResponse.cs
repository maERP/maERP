using System.ComponentModel.DataAnnotations;

namespace maERP.Application.Features.User.Queries.UserList;

public class UserListResponse
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