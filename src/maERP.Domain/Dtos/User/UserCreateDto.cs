using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace maERP.Domain.Dtos.User;

public class UserCreateDto
{
    [Required]
    public string Firstname { get; set; } = string.Empty;

    [Required]
    public string Lastname { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(15, ErrorMessage = "Das Passwort muss {2} bis {1} Zeichen haben.", MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    // [JsonIgnore]
    [Compare("Password", ErrorMessage = "Die Passwörter stimmen nicht überein.")]
    public string PasswordConfirm { get; set; } = string.Empty;

    // Default tenant ID (required for initial creation)
    [Required]
    public Guid DefaultTenantId { get; set; }

    // Additional tenant assignments
    public List<Guid> AdditionalTenantIds { get; set; } = new List<Guid>();
}