namespace maERP.Domain.Dtos.Auth;

public class ResetPasswordResponseDto
{
    public bool Succeeded { get; set; }
    public string Message { get; set; } = string.Empty;
}
