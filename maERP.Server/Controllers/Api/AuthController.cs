using maERP.Server.Contracts;
using maERP.Shared.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;
    
    public AuthController(IAuthService authenticationService)
    {
        this._authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
    {
        return Ok(await _authenticationService.Login(request));
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        return Ok(await _authenticationService.Register(request));
    }
}