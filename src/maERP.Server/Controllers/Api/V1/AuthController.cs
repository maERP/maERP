using Asp.Versioning;
using maERP.Application.Contracts.Identity;
using maERP.Application.Models.Identity;
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
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
    {
        var result = await authenticationService.Login(request);
        
        if (!result.Succeeded)
        {
            return StatusCode((int)result.StatusCode, result.Messages);
        }
        
        return Ok(result.Data);
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        var result = await authenticationService.Register(request);
        
        if (!result.Succeeded)
        {
            return StatusCode((int)result.StatusCode, result.Messages);
        }
        
        return Ok(result.Data);
    }
}