#nullable disable

namespace maERP.Data.Dtos.User
{
	public class LoginResponseDto
	{
		public string UserId { get; set; }
		public string Token { get; set; }
		public string RefreshToken { get; set; }
	}
}