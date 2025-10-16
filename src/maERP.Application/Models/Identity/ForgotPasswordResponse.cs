namespace maERP.Application.Models.Identity;

public class ForgotPasswordResponse
{
    public bool Succeeded { get; set; }
    public string Message { get; set; } = string.Empty;
}
