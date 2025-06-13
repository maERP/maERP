using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using maERP.Application.Contracts.Identity;
using maERP.Application.Models.Identity;
using maERP.Domain.Dtos.Auth;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace maERP.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly SignInManager<ApplicationUser> _signInManager;
    
    public AuthService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
    }
    
    public async Task<Result<LoginResponseDto>> Login(AuthRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return Result<LoginResponseDto>.Fail(ResultStatusCode.Unauthorized, "Ungültige Anmeldedaten.");
        }
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
        {
            return Result<LoginResponseDto>.Fail(ResultStatusCode.Unauthorized, "Ungültige Anmeldedaten.");
        }

        var jwtSecurityToken = await GenerateToken(user);

        var response = new LoginResponseDto()
        {
            UserId = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Succeeded = true,
            Message = "Anmeldung erfolgreich."
        };

        return Result<LoginResponseDto>.Success(response);
    }
    
    public async Task<Result<RegistrationResponse>> Register(RegistrationRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            UserName = request.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Employee");
            return Result<RegistrationResponse>.Success(new RegistrationResponse { UserId = user.Id });
        }

        var stringBuilder = new StringBuilder();
        foreach(var err in result.Errors)
        {
            stringBuilder.AppendFormat("{0}\n", err.Description);
        }

        return Result<RegistrationResponse>.Fail(ResultStatusCode.BadRequest, $"Registrierung fehlgeschlagen. {stringBuilder}");
    }
    
    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        
        var roleClaims = roles.Select(q =>new Claim(ClaimTypes.Role, q)).ToList();
        
#nullable disable
        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? throw new InvalidOperationException()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? throw new InvalidOperationException()),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
#nullable restore

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials
        );

        return jwtSecurityToken;
    }
}