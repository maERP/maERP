#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using maERP.Shared.Dtos.User;
using maERP.Shared.Models;

namespace maERP.Server.Repository;

public interface IAuthManager
{
    Task<LoginResponseDto> Login(LoginDto userDto);
    Task<string> CreateRefreshToken();
    Task<LoginResponseDto> VerifyRefreshToken(RefreshTokenDto request);
    Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
    Task<ApiUser> UpdateAsync(ApiUserDto userDto);
    Task<IQueryable<ApiUser>> GetAllAsync();
    Task<ApiUserDto> GetByIdAsync(string userId);
}

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

    public async Task<IQueryable<ApiUser>> GetAllAsync()
    {
        await Task.CompletedTask;
        return _userManager.Users;
    }

    public async Task<ApiUserDto> GetByIdAsync(string userId)
    {
        await Task.CompletedTask;
        var user = _userManager.Users.Where(x => x.Id == userId).First<ApiUser>();
        var userDto = _mapper.Map<ApiUserDto>(user);
        return userDto;
    }

    public async Task<ApiUser> UpdateAsync(ApiUserDto userDto)
    {
        var updateUser = _mapper.Map<ApiUser>(userDto);
        updateUser.UserName = userDto.Email;

        var localUser = await _userManager.FindByEmailAsync(updateUser.Email);

        if (localUser.Id is not null)
        {
            await _userManager.UpdateAsync(updateUser);

            return updateUser;
        }

        throw new Exceptions.NotFoundException("User not found", "User not found");
    }

    public async Task<LoginResponseDto> Login(LoginDto loginDto)
    {
        _user = await _userManager.FindByEmailAsync(loginDto.Email);
        bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

        if (_user == null || isValidUser == false)
        {
            return null;
        }

        var accessToken = await CreateAccessToken();
        var refreshToken = await CreateRefreshToken();
        var accessTokenExpiration = DateTime.Now.AddMinutes(Convert.ToInt32(
            _configuration["JwtSettings:DurationInMinutes"]
        ));

        return new LoginResponseDto
        {
            Succeeded = true,
            Token = new TokenDto
            {
                AccessToken = accessToken,
                AccessTokenExpiration = accessTokenExpiration,
                RefreshToken = refreshToken,
                BaseUrl = loginDto.Server
            }
        };
    }

    private async Task<string> CreateAccessToken()
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

    public async Task<LoginResponseDto> VerifyRefreshToken(RefreshTokenDto request)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.AccessToken);

        var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;

        _user = await _userManager.FindByEmailAsync(username);

        if (_user == null || _user.Id != request.UserId)
        {
            return null;
        }

        var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(
            _user, "maERP.Server", "RefreshToken", request.RefreshToken);

        if (isValidRefreshToken)
        {
            var accessToken = await CreateAccessToken();
            var refreshToken = await CreateRefreshToken();
            var accessTokenExpiration = DateTime.Now.AddMinutes(Convert.ToInt32(
            _configuration["JwtSettings:DurationInMinutes"]
        ));

            return new LoginResponseDto
            {
                UserId = _user.Id,
                Token = new TokenDto
                {
                    AccessToken = accessToken,
                    AccessTokenExpiration = accessTokenExpiration,
                    RefreshToken = refreshToken
                }
            };
        }

        await _userManager.UpdateSecurityStampAsync(_user);

        return null;
    }
}