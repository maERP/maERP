namespace maERP.Shared.Dtos.User;

public class TokenDto
{
    public string BaseUrl { get; set; } = "";

    public string AccessToken { get; set; } = "";
	public string RefreshToken { get; set; } = "";

    public DateTime AccessTokenExpiration { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}