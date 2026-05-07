using System.Security.Claims;
using System.Threading.RateLimiting;
using Asp.Versioning;
using maERP.Application.Contracts.Identity;
using maERP.Application.Models.Identity;
using maERP.Domain.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
[EnableRateLimiting("auth")]
public class AuthController(IAuthService authenticationService, ILogger<AuthController> logger) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponseDto>> Login(AuthRequest request)
    {
        var result = await authenticationService.Login(request);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<LoginResponseDto>> Register(RegistrationRequest request)
    {
        logger.LogDebug("🔷 Register endpoint called");
        logger.LogDebug("📧 Email: {Email}", request.Email);
        logger.LogDebug("👤 Firstname: {Firstname}, Lastname: {Lastname}", request.Firstname, request.Lastname);
        logger.LogDebug("🔑 Password provided: {HasPassword}", !string.IsNullOrEmpty(request.Password));

        var result = await authenticationService.Register(request);

        logger.LogDebug("📊 Registration result - StatusCode: {StatusCode}, Succeeded: {Succeeded}",
            result.StatusCode, result.Succeeded);

        if (!result.Succeeded)
        {
            logger.LogDebug("❌ Registration failed - Messages: {Messages}", string.Join(", ", result.Messages));
        }
        else
        {
            logger.LogDebug("✅ Registration succeeded - UserId: {UserId}", result.Data?.UserId);
        }

        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponseDto>> RefreshToken(RefreshTokenRequestDto request)
    {
        var result = await authenticationService.RefreshToken(request.RefreshToken);
        return StatusCode((int)result.StatusCode, result);
    }

    /// <summary>
    /// Revokes the supplied refresh-token (and its rotation chain). Anonymous so the client can
    /// always log out, even after the access token has expired.
    /// </summary>
    [HttpPost("logout")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Logout(RefreshTokenRequestDto request)
    {
        await authenticationService.Logout(request.RefreshToken);
        return NoContent();
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ForgotPasswordResponseDto>> ForgotPassword(ForgotPasswordRequestDto request)
    {
        logger.LogDebug("🔑 ForgotPassword endpoint called for email: {Email}", request.Email);

        var serviceRequest = new Application.Models.Identity.ForgotPasswordRequest
        {
            Email = request.Email
        };

        var result = await authenticationService.ForgotPassword(serviceRequest);

        var response = new ForgotPasswordResponseDto
        {
            Succeeded = result.Data?.Succeeded ?? false,
            Message = result.Data?.Message ?? result.Messages.FirstOrDefault() ?? "Ein Fehler ist aufgetreten."
        };

        logger.LogDebug("📊 ForgotPassword result - StatusCode: {StatusCode}, Succeeded: {Succeeded}",
            result.StatusCode, response.Succeeded);

        return StatusCode((int)result.StatusCode, response);
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ResetPasswordResponseDto>> ResetPassword(ResetPasswordRequestDto request)
    {
        logger.LogDebug("🔄 ResetPassword endpoint called for email: {Email}", request.Email);

        var serviceRequest = new Application.Models.Identity.ResetPasswordRequest
        {
            Email = request.Email,
            Token = request.Token,
            NewPassword = request.NewPassword
        };

        var result = await authenticationService.ResetPassword(serviceRequest);

        var response = new ResetPasswordResponseDto
        {
            Succeeded = result.Data?.Succeeded ?? false,
            Message = result.Data?.Message ?? result.Messages.FirstOrDefault() ?? "Ein Fehler ist aufgetreten."
        };

        logger.LogDebug("📊 ResetPassword result - StatusCode: {StatusCode}, Succeeded: {Succeeded}",
            result.StatusCode, response.Succeeded);

        return StatusCode((int)result.StatusCode, response);
    }
}