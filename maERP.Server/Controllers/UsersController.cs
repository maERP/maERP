#nullable disable

using maERP.Server.Contracts;
using maERP.Shared.Dtos.User;
using maERP.Shared.Dtos.Warehouse;
using maERP.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace maERP.Server.Controllers;

// [Route("api/v{version:apiVersion}/[controller]")]
[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    private readonly IAuthManager _authManager;
    private readonly ILogger _logger;

    public UsersController(IAuthManager authManager, ILogger<UsersController> logger)
    {
        this._authManager = authManager;
        this._logger = logger;
    }

    // POST: api/Users/login
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
    {
        var authResponse = await _authManager.Login(loginDto);

        if (authResponse == null)
        {
            return Unauthorized();
        }

        return Ok(authResponse);
    }

    // POST: api/Users/refreshtoken
    [HttpPost]
    [Route("refreshtoken")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> RefreshToken([FromBody] LoginResponseDto request)
    {
        var authResponse = await _authManager.VerifyRefreshToken(request);

        if (authResponse == null)
        {
            return Unauthorized();
        }

        return Ok(authResponse);
    }

    // POST: api/Users/register
    [HttpPost]
    [Route("register")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Register([FromBody] ApiUserDto apiUserDto)
    {
        _logger.LogInformation($"Registration Attempt for {apiUserDto.Email}");

        try
        {
            var errors = await _authManager.Register(apiUserDto);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}" +
                $" - User registration attempt for {apiUserDto.Email}");

            return Problem($"Something went wrong in the {nameof(Register)}");
        }
    }

    // GET: api/Users
    [HttpGet("GetAll")]
    [Authorize]
    // [Authorize(Roles = "Admin")]
    // [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IQueryable<ApiUser>>> GetUsers()
    {
        var users = await _authManager.GetAllAsync();
        return Ok(users);
    }

    // GET: api/Users/5
    [HttpGet("{userId}")]
    [Authorize]
    // [Authorize(Roles = "Admin")]
    // [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ApiUserDto>> GetUserById(string userId)
    {
        var user = await _authManager.GetByIdAsync(userId);
        return Ok(user);
    }

    // PUT: api/Users/5
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> EditUser([FromBody] ApiUserDto apiUserDto)
    {
        _logger.LogInformation($"Edit Attempt for {apiUserDto.Email}");

        try
        {
            var user = await _authManager.UpdateAsync (apiUserDto);

            if (user.Id is not null)
            {
                return Ok();
                
            }

            return BadRequest(ModelState);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}" +
                $" - User registration attempt for {apiUserDto.Email}");

            return Problem($"Something went wrong in the {nameof(Register)}");
        }
    }

    // GET: api/Users/Status
    [HttpGet("Status")]
    public async Task<ActionResult> GetStatus()
    {
        await Task.CompletedTask;
        return Ok("ok");
    }
}