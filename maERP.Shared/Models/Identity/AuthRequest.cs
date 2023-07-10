using System.ComponentModel.DataAnnotations;

namespace maERP.Shared.Models.Identity;

public class AuthRequest
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}