using System;
using maERP.Server.Models;
using maERP.Server.Services;
using maERP.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace maERP.Server.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApiUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly TokenService _tokenService;

    public UserController(UserManager<ApiUser> userManager, ApplicationDbContext context, TokenService tokenService)
    {
        _userManager = userManager;
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _userManager.CreateAsync(
            new ApiUser { UserName = request.Username, Email = request.Email },
            request.Password
        );
        if (result.Succeeded)
        {
            request.Password = "";
            return CreatedAtAction(nameof(Register), new { email = request.Email }, request);
        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }
        return BadRequest(ModelState);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var managedUser = await _userManager.FindByEmailAsync(request.Email);
        if (managedUser == null)
        {
            return BadRequest("Bad credentials");
        }
        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }
        var userInDb = _context.Users.FirstOrDefault(u => u.Email == request.Email);

        if (userInDb is null)
            return Unauthorized();

        var accessToken = _tokenService.CreateToken(userInDb);
        await _context.SaveChangesAsync();

        return Ok(new AuthResponse
        {
            Username = userInDb.UserName,
            Email = userInDb.Email,
            Token = accessToken,
        });
    }
}