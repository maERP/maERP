#nullable disable

using AutoMapper;
using maERP.Server.Contracts;
using maERP.Server.Data;
using maERP.Server.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace maERP.Server.Repository
{
	public class AuthManager : IAuthManager
	{
		private readonly IMapper _mapper;
		private readonly UserManager<ApiUser> _userManager;
		private readonly IConfiguration _configuration;
		private ApiUser _user;

		public AuthManager(IMapper mapper, UserManager<ApiUser> userManager,
			IConfiguration configuration)
		{
			this._mapper = mapper;
			this._userManager = userManager;
			this._configuration = configuration;
		}

		public async Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto)
		{
			var _user = _mapper.Map<ApiUser>(userDto);
			_user.UserName = userDto.Email;

			var result = await _userManager.CreateAsync(_user, userDto.Password);

			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(_user, "User");
			}

			return result.Errors;
		}

		public async Task<AuthResponseDto> Login(LoginDto loginDto)
		{
			var _user = await _userManager.FindByEmailAsync(loginDto.Email);
			bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);
	
			if(_user == null || isValidUser == false)
            {
				return null;
            }

			var token = await GenerateToken();

			return new AuthResponseDto
			{
				Token = token,
				UserId = _user.Id
			};
		}

		private async Task<string> GenerateToken()
        {
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
				_configuration["JwtSettings:Key"]
			));

			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var roles = await _userManager.GetRolesAsync(_user);
			var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
			var userClaims = await _userManager.GetClaimsAsync(_user);

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
				new Claim(JwtRegisteredClaimNames.Email, _user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim("uid", _user.Id),
			}
			.Union(userClaims).Union(roleClaims);

			var token = new JwtSecurityToken(
					issuer: _configuration["JwtSettings:Issuer"],
					audience: _configuration["JwtSettings:Audience"],
					claims: claims,
					expires: DateTime.Now.AddMinutes(Convert.ToInt32(
						_configuration["JwtSettings:DurationInMinutes"]
					)),
					signingCredentials: credentials
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
        }

		public async Task<string> CreateRefreshToken()
        {
			await _userManager.RemoveAuthenticationTokenAsync(
				_user, "maERP.Server", "RefreshToken");

			var newRefreshToken = await _userManager.GenerateUserTokenAsync(
				_user, "maERP.Server", "RefreshToken");

			await _userManager.SetAuthenticationTokenAsync(
				_user, "maERP.Server", "RefreshToken", newRefreshToken);

			return newRefreshToken;
        }

        public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
        {
			var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);

			var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;

			_user = await _userManager.FindByEmailAsync(username);

			if(_user == null || _user.Id != request.UserId)
            {
				return null;
            }

			var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(
				_user, "maERP.Server", "RefreshToken", request.RefreshToken);

			if(isValidRefreshToken)
            {
				var token = await GenerateToken();
				return new AuthResponseDto
				{
					Token = token,
					UserId = _user.Id,
					RefreshToken = await CreateRefreshToken()
				};
            }

			await _userManager.UpdateSecurityStampAsync(_user);

			return null;
		}
    }
}