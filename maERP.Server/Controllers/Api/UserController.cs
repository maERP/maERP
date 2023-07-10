﻿#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using maERP.Server.Repository;
using maERP.Shared.Dtos.User;
using maERP.Server.Models;

namespace maERP.Server.Controllers;

// [Route("api/v{version:apiVersion}/[controller]")]
[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly ILogger _logger;

    public UserController(IUserRepository repository, ILogger<UserController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    // GET: api/User
    [HttpGet("GetAll")]
    [Authorize]
    // [Authorize(Roles = "Admin")]
    // [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IQueryable<User>>> GetUsers()
    {
        var users = await _repository.GetAllAsync();
        return Ok(users);
    }

    // GET: api/User/5
    [HttpGet("{userId}")]
    [Authorize]
    // [Authorize(Roles = "Admin")]
    // [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<UserDetailDto>> GetUserById(string userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        return Ok(user);
    }

    // POST: api/User
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Add([FromBody] UserCreateDto userCreateDto)
    {
        _logger.LogInformation($"Registration Attempt for {userCreateDto.Email}");

        try
        {
            var errors = await _repository.AddAsync(userCreateDto);

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
            _logger.LogError(ex, $"Something went wrong in the {nameof(Add)}" +
                $" - User registration attempt for {userCreateDto.Email}");

            return Problem($"Something went wrong in the {nameof(Add)}");
        }
    }

    // PUT: api/User/5
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Update([FromBody] UserUpdateDto userUpdateDto)
    {
        _logger.LogInformation($"Edit Attempt for {userUpdateDto.Email}");

        try
        {
            var user = await _repository.UpdateAsync(userUpdateDto);

            if (user.Id is not null)
            {
                return Ok();

            }

            return BadRequest(ModelState);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(Update)}" +
                $" - User registration attempt for {userUpdateDto.Email}");

            return Problem($"Something went wrong in the {nameof(Update)}");
        }
    }
}