namespace maERP.Server.Models;

public class AuthResponse
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}