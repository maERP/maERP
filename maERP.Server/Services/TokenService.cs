using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using maERP.Shared.Dtos.User;

namespace maERP.Server.Services;

public interface ITokenService
{
    string CreateToken(IdentityUser user);
    JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
        DateTime expiration) =>
        new(
            "maERP",
            "maERP",
            claims,
            expires: expiration,
            signingCredentials: credentials
        );
    List<Claim> CreateClaims(IdentityUser user);
    SigningCredentials CreateSigningCredentials();
}

public class TokenService : ITokenService
{
    private const int ExpirationMinutes = 30;

    public string CreateToken(IdentityUser user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtToken(
            CreateClaims(user),
            CreateSigningCredentials(),
            expiration
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
        DateTime expiration) =>
        new(
            "maERP",
            "maERP",
            claims,
            expires: expiration,
            signingCredentials: credentials
        );

    public List<Claim> CreateClaims(IdentityUser user)
    {
        try
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "TokenFromMaERP"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                };
            return claims;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(
                // Encoding.UTF8.GetBytes("!SomethingSecret!")
                Encoding.UTF8.GetBytes("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")
            ),
            SecurityAlgorithms.HmacSha256
        );
    }
}