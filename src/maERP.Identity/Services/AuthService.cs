using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using maERP.Application.Contracts.Identity;
using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Models.Identity;
using maERP.Domain.Dtos.Auth;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace maERP.Identity.Services;

public class AuthService : maERP.Application.Contracts.Identity.IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserTenantService _userTenantService;
    private readonly IUserTenantRepository _userTenantRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IEmailService _emailService;
    private readonly IServerInfoService _serverInfoService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        JwtSettings jwtSettings,
        SignInManager<ApplicationUser> signInManager,
        IUserTenantService userTenantService,
        IUserTenantRepository userTenantRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IEmailService emailService,
        IServerInfoService serverInfoService,
        ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings;
        _signInManager = signInManager;
        _userTenantService = userTenantService;
        _userTenantRepository = userTenantRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _emailService = emailService;
        _serverInfoService = serverInfoService;
        _logger = logger;
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

        // Load user's available tenants
        var availableTenants = await _userTenantService.GetUserTenantsAsync(user.Id);

        // Get the default tenant ID from UserTenant relationship
        var defaultTenantId = await _userTenantRepository.Entities
            .Where(ut => ut.UserId == user.Id && ut.IsDefault)
            .Select(ut => (Guid?)ut.TenantId)
            .FirstOrDefaultAsync();

        var jwtSecurityToken = await GenerateToken(user, availableTenants, defaultTenantId);
        var refreshIssued = await IssueRefreshTokenAsync(user.Id, family: null, isPersistent: request.RememberMe);

        var response = new LoginResponseDto()
        {
            UserId = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            RefreshToken = refreshIssued.PlaintextToken,
            RefreshTokenExpiresAt = refreshIssued.Entity.ExpiresAt,
            Succeeded = true,
            Message = "Anmeldung erfolgreich.",
            AvailableTenants = availableTenants,
            CurrentTenantId = defaultTenantId
        };

        return Result<LoginResponseDto>.Success(response);
    }

    public async Task<Result<LoginResponseDto>> Register(RegistrationRequest request)
    {
        _logger.LogDebug("🟦 AuthService.Register called");
        _logger.LogDebug("📧 Email: {Email}", request.Email);
        _logger.LogDebug("👤 Firstname: {Firstname}, Lastname: {Lastname}", request.Firstname, request.Lastname);
        _logger.LogDebug("🔑 Password length: {PasswordLength}", request.Password?.Length ?? 0);

        if (!_serverInfoService.IsRegistrationEnabled)
        {
            _logger.LogWarning("⛔ Registration attempt rejected — registration is disabled on this server.");
            return Result<LoginResponseDto>.Fail(ResultStatusCode.Forbidden, "Die Registrierung ist auf diesem Server deaktiviert.");
        }

        var user = new ApplicationUser
        {
            Email = request.Email,
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            UserName = request.Email,
            EmailConfirmed = true
        };

        _logger.LogDebug("👤 Creating ApplicationUser with UserName: {UserName}", user.UserName);

        var result = await _userManager.CreateAsync(user, request.Password!);

        _logger.LogDebug("📊 UserManager.CreateAsync result - Succeeded: {Succeeded}", result.Succeeded);

        if (result.Succeeded)
        {
            _logger.LogDebug("✅ User created successfully with Id: {UserId}", user.Id);
            _logger.LogDebug("🎭 Adding 'User' role to user");

            await _userManager.AddToRoleAsync(user, "User");

            _logger.LogDebug("✅ Role added successfully — issuing JWT for auto-login");

            var availableTenants = await _userTenantService.GetUserTenantsAsync(user.Id);
            var jwtSecurityToken = await GenerateToken(user, availableTenants, defaultTenantId: null);
            // Auto-login after registration: issue a session-length refresh token. The user can
            // re-tick "Eingeloggt bleiben" on the next manual login to upgrade to long-lived.
            var refreshIssued = await IssueRefreshTokenAsync(user.Id, family: null, isPersistent: false);

            return Result<LoginResponseDto>.Success(new LoginResponseDto
            {
                UserId = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                RefreshToken = refreshIssued.PlaintextToken,
                RefreshTokenExpiresAt = refreshIssued.Entity.ExpiresAt,
                Succeeded = true,
                Message = "Registrierung erfolgreich.",
                AvailableTenants = availableTenants,
                CurrentTenantId = null
            });
        }

        _logger.LogDebug("❌ User creation failed with {ErrorCount} errors", result.Errors.Count());

        var stringBuilder = new StringBuilder();
        foreach (var err in result.Errors)
        {
            _logger.LogDebug("❌ Error: {Code} - {Description}", err.Code, err.Description);

            // Map common errors to user-friendly German messages
            switch (err.Code)
            {
                case "DuplicateUserName":
                case "DuplicateEmail":
                    stringBuilder.AppendLine("Diese E-Mail-Adresse ist bereits registriert. Bitte verwenden Sie eine andere E-Mail-Adresse oder melden Sie sich an.");
                    break;
                case "PasswordTooShort":
                    stringBuilder.AppendLine("Das Passwort ist zu kurz. Bitte verwenden Sie mindestens 6 Zeichen.");
                    break;
                case "PasswordRequiresNonAlphanumeric":
                    stringBuilder.AppendLine("Das Passwort muss mindestens ein Sonderzeichen enthalten.");
                    break;
                case "PasswordRequiresDigit":
                    stringBuilder.AppendLine("Das Passwort muss mindestens eine Zahl enthalten.");
                    break;
                case "PasswordRequiresUpper":
                    stringBuilder.AppendLine("Das Passwort muss mindestens einen Großbuchstaben enthalten.");
                    break;
                case "PasswordRequiresLower":
                    stringBuilder.AppendLine("Das Passwort muss mindestens einen Kleinbuchstaben enthalten.");
                    break;
                case "InvalidEmail":
                    stringBuilder.AppendLine("Die E-Mail-Adresse ist ungültig.");
                    break;
                default:
                    stringBuilder.AppendLine(err.Description);
                    break;
            }
        }

        var errorMessage = stringBuilder.ToString().Trim();
        if (string.IsNullOrEmpty(errorMessage))
        {
            errorMessage = "Registrierung fehlgeschlagen.";
        }

        _logger.LogDebug("❌ Final error message: {ErrorMessage}", errorMessage);

        return Result<LoginResponseDto>.Fail(ResultStatusCode.BadRequest, errorMessage);
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user, List<TenantListDto> availableTenants, Guid? defaultTenantId)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        // Serialize available tenants to include in token
        var tenantsClaim = JsonSerializer.Serialize(availableTenants.Select(t => new
        {
            Id = t.Id,
            Name = t.Name
        }));

#nullable disable
        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? throw new InvalidOperationException()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? throw new InvalidOperationException()),
                new Claim("uid", user.Id),
                new Claim("tenantId", defaultTenantId?.ToString() ?? "0"),
                new Claim("availableTenants", tenantsClaim)
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

    public async Task<Result<LoginResponseDto>> RefreshToken(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return Result<LoginResponseDto>.Fail(ResultStatusCode.Unauthorized, "Refresh-Token fehlt.");
        }

        var hash = HashToken(refreshToken);
        var stored = await _refreshTokenRepository.GetByHashAsync(hash);

        if (stored == null)
        {
            _logger.LogWarning("Refresh attempt with unknown token hash");
            return Result<LoginResponseDto>.Fail(ResultStatusCode.Unauthorized, "Refresh-Token ungültig.");
        }

        // Replay detection: a previously-revoked token is being presented again.
        // Treat as compromise — burn the entire family.
        if (stored.RevokedAt.HasValue)
        {
            _logger.LogWarning("Refresh-token replay detected for family {Family} (user {UserId}) — revoking family", stored.Family, stored.UserId);
            await _refreshTokenRepository.RevokeFamilyAsync(stored.Family, DateTime.UtcNow);
            return Result<LoginResponseDto>.Fail(ResultStatusCode.Unauthorized, "Refresh-Token ungültig.");
        }

        if (stored.ExpiresAt <= DateTime.UtcNow)
        {
            return Result<LoginResponseDto>.Fail(ResultStatusCode.Unauthorized, "Refresh-Token abgelaufen.");
        }

        var user = await _userManager.FindByIdAsync(stored.UserId);
        if (user == null)
        {
            return Result<LoginResponseDto>.Fail(ResultStatusCode.Unauthorized, "Benutzer nicht gefunden.");
        }

        var availableTenants = await _userTenantService.GetUserTenantsAsync(user.Id);
        var defaultTenantId = await _userTenantRepository.Entities
            .Where(ut => ut.UserId == user.Id && ut.IsDefault)
            .Select(ut => (Guid?)ut.TenantId)
            .FirstOrDefaultAsync();

        // Rotate: issue successor in same family, mark current as revoked + chained.
        var newRefresh = await IssueRefreshTokenAsync(user.Id, family: stored.Family, isPersistent: stored.IsPersistent);

        stored.RevokedAt = DateTime.UtcNow;
        stored.ReplacedByTokenId = newRefresh.Entity.Id;
        await _refreshTokenRepository.UpdateAsync(stored);

        var jwtSecurityToken = await GenerateToken(user, availableTenants, defaultTenantId);

        var response = new LoginResponseDto()
        {
            UserId = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            RefreshToken = newRefresh.PlaintextToken,
            RefreshTokenExpiresAt = newRefresh.Entity.ExpiresAt,
            Succeeded = true,
            Message = "Token erfolgreich aktualisiert.",
            AvailableTenants = availableTenants,
            CurrentTenantId = defaultTenantId
        };

        return Result<LoginResponseDto>.Success(response);
    }

    public async Task Logout(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken)) return;

        var hash = HashToken(refreshToken);
        var stored = await _refreshTokenRepository.GetByHashAsync(hash);
        if (stored == null) return;

        await _refreshTokenRepository.RevokeFamilyAsync(stored.Family, DateTime.UtcNow);
    }

    private async Task<(RefreshToken Entity, string PlaintextToken)> IssueRefreshTokenAsync(
        string userId, Guid? family, bool isPersistent)
    {
        // 64 bytes = 512 bits of entropy → base64url ~86 chars. Plenty.
        var bytes = RandomNumberGenerator.GetBytes(64);
        var plaintext = Base64UrlEncode(bytes);

        var lifetimeDays = isPersistent
            ? Math.Max(1, _jwtSettings.RefreshTokenExpireDays)
            : Math.Max(1, _jwtSettings.RefreshTokenExpireDaysShort);

        var entity = new RefreshToken
        {
            UserId = userId,
            TokenHash = HashToken(plaintext),
            Family = family ?? Guid.NewGuid(),
            ExpiresAt = DateTime.UtcNow.AddDays(lifetimeDays),
            IsPersistent = isPersistent
        };

        await _refreshTokenRepository.CreateAsync(entity);
        return (entity, plaintext);
    }

    private static string HashToken(string token)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(bytes);
    }

    private static string Base64UrlEncode(byte[] bytes)
        => Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');

    public async Task<Result<ForgotPasswordResponse>> ForgotPassword(ForgotPasswordRequest request)
    {
        _logger.LogDebug("🔑 ForgotPassword called for email: {Email}", request.Email);

        var user = await _userManager.FindByEmailAsync(request.Email);

        // Return success even if user not found (security best practice to prevent user enumeration)
        if (user == null)
        {
            _logger.LogDebug("⚠️ User not found for email: {Email}", request.Email);
            return Result<ForgotPasswordResponse>.Success(new ForgotPasswordResponse
            {
                Succeeded = true,
                Message = "Falls ein Konto mit dieser E-Mail-Adresse existiert, wurde eine E-Mail mit Anweisungen zum Zurücksetzen des Passworts gesendet."
            });
        }

        // Generate password reset token
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        _logger.LogDebug("✅ Password reset token generated for user: {UserId}", user.Id);

        // Send password reset email
        var emailSent = await _emailService.SendPasswordResetEmailAsync(
            user.Email!,
            $"{user.Firstname} {user.Lastname}".Trim(),
            token,
            tenantId: null // Will use current tenant context or default
        );

        if (!emailSent)
        {
            _logger.LogWarning("⚠️ Failed to send password reset email to {Email}, but returning success for security", request.Email);
        }
        else
        {
            _logger.LogInformation("✅ Password reset email sent successfully to {Email}", request.Email);
        }

        return Result<ForgotPasswordResponse>.Success(new ForgotPasswordResponse
        {
            Succeeded = true,
            Message = "Falls ein Konto mit dieser E-Mail-Adresse existiert, wurde eine E-Mail mit Anweisungen zum Zurücksetzen des Passworts gesendet."
        });
    }

    public async Task<Result<ResetPasswordResponse>> ResetPassword(ResetPasswordRequest request)
    {
        _logger.LogDebug("🔄 ResetPassword called for email: {Email}", request.Email);

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            _logger.LogDebug("❌ User not found for email: {Email}", request.Email);
            return Result<ResetPasswordResponse>.Fail(
                ResultStatusCode.BadRequest,
                "Das Zurücksetzen des Passworts ist fehlgeschlagen. Bitte überprüfen Sie Ihre Eingaben."
            );
        }

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        if (result.Succeeded)
        {
            _logger.LogDebug("✅ Password reset successful for user: {UserId}", user.Id);
            return Result<ResetPasswordResponse>.Success(new ResetPasswordResponse
            {
                Succeeded = true,
                Message = "Ihr Passwort wurde erfolgreich zurückgesetzt. Sie können sich jetzt mit Ihrem neuen Passwort anmelden."
            });
        }

        _logger.LogDebug("❌ Password reset failed for user: {UserId}. Errors: {Errors}",
            user.Id,
            string.Join(", ", result.Errors.Select(e => e.Description)));

        var stringBuilder = new StringBuilder();
        foreach (var err in result.Errors)
        {
            switch (err.Code)
            {
                case "InvalidToken":
                    stringBuilder.AppendLine("Der Reset-Token ist ungültig oder abgelaufen. Bitte fsalesn Sie einen neuen Token an.");
                    break;
                case "PasswordTooShort":
                    stringBuilder.AppendLine("Das Passwort ist zu kurz. Bitte verwenden Sie mindestens 6 Zeichen.");
                    break;
                case "PasswordRequiresNonAlphanumeric":
                    stringBuilder.AppendLine("Das Passwort muss mindestens ein Sonderzeichen enthalten.");
                    break;
                case "PasswordRequiresDigit":
                    stringBuilder.AppendLine("Das Passwort muss mindestens eine Zahl enthalten.");
                    break;
                case "PasswordRequiresUpper":
                    stringBuilder.AppendLine("Das Passwort muss mindestens einen Großbuchstaben enthalten.");
                    break;
                case "PasswordRequiresLower":
                    stringBuilder.AppendLine("Das Passwort muss mindestens einen Kleinbuchstaben enthalten.");
                    break;
                default:
                    stringBuilder.AppendLine(err.Description);
                    break;
            }
        }

        var errorMessage = stringBuilder.ToString().Trim();
        if (string.IsNullOrEmpty(errorMessage))
        {
            errorMessage = "Das Zurücksetzen des Passworts ist fehlgeschlagen.";
        }

        return Result<ResetPasswordResponse>.Fail(ResultStatusCode.BadRequest, errorMessage);
    }
}