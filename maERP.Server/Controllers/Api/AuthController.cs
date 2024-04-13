using maERP.Application.Contracts.Identity;
using maERP.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authenticationService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
    {
        return Ok(await authenticationService.Login(request));
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        return Ok(await authenticationService.Register(request));
    }
}