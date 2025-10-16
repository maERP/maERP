namespace maERP.Domain.Dtos.Auth;

public class ForgotPasswordResponseDto
{
    public bool Succeeded { get; set; }
    public string Message { get; set; } = string.Empty;
}
