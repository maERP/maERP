using Asp.Versioning;
using maERP.Application.Contracts.Identity;
using maERP.Application.Models.Identity;
using maERP.Domain.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class AuthController(IAuthService authenticationService) : ControllerBase
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
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        var result = await authenticationService.Register(request);
        return StatusCode((int)result.StatusCode, result);
    }
}