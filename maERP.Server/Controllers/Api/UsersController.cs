#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using maERP.Shared.Dtos.User;
using Asp.Versioning;
using maERP.Server.Contracts;

namespace maERP.Server.Controllers;

// [Route("api/v{version:apiVersion}/[controller]")]
[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
// [Authorize(Roles = "Administrator")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository repository, IMapper mapper, ILogger<UsersController> logger)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    // GET: api/Users
    [HttpGet("GetAll")]
    public async Task<ActionResult<IQueryable<UserListDto>>> GetUsers()
    {
        var users = await _repository.GetAllAsync();
        return Ok(users);
    }

    // GET: api/Users/5
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDetailDto>> GetUserById(string userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        return Ok(user);
    }

    // POST: api/Users
    [HttpPost]
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

    /*
    // PUT: api/Users
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Update([FromBody] UserUpdateDto userUpdateDto)
    {
        _logger.LogInformation($"Edit Attempt for {userUpdateDto.Email}");

        try
        {
            var applicationUser = _mapper.Map<ApplicationUser>(userUpdateDto);

            var user = await _repository.UpdateAsync(applicationUser);

            if (user.Id is not null)
            {
                return Ok();
            }

            return BadRequest(ModelState);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(Update)}" +
                $" - User update attempt for {userUpdateDto.Email}");

            return Problem($"Something went wrong in THE {nameof(Update)}");
        }
    }
    */

    // PUT: api/SalesChannels/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(string id, [FromBody] UserUpdateDto userUpdateDto)
    {
        if (await _repository.Exists(id) == true)
        {
            await _repository.UpdateWithDetailsAsync(id, userUpdateDto);
        }
        else
        {
            return NotFound();
        }

        return NoContent();
    }
}