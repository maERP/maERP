using System.ComponentModel.DataAnnotations;

namespace maERP.Domain.Dtos.Auth;

public class RefreshTokenRequestDto
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
