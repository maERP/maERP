#nullable disable

using maERP;

namespace maERP.Application.Dtos.Auth;

public class LoginResponseDto
{
    public string UserId { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public string AccessToken { get; set; }
}