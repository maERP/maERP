using System.ComponentModel.DataAnnotations;

namespace maERP.Application.Models.Identity;

public class RegistrationRequest
{
    [Required]
    public string Firstname { get; set; } = string.Empty;

    [Required]
    public string Lastname { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [MinLength(6)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}