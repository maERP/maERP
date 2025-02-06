#nullable disable

namespace maERP.Domain.Dtos.Auth;

public class LoginResponseDto
{
    public string UserId { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public string AccessToken { get; set; }
}