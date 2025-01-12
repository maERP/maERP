using Asp.Versioning;
using maERP.Application.Features.User.Commands.UserCreate;
using maERP.Application.Features.User.Commands.UserUpdate;
using maERP.Application.Features.User.Queries.UserDetail;
using maERP.Application.Features.User.Queries.UserList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    // GET: api/<UsersController>
    [HttpGet]
    public async Task<ActionResult<List<UserListResponse>>> Get()
    {
        var users = await mediator.Send(new UserListQuery());
        return Ok(users);
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDetailResponse>> GetDetails(string id)
    {
        var user = await mediator.Send(new UserDetailQuery { Id = id });
        return Ok(user);
    }

    // POST api/<UsersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(UserCreateCommand userCreateCommand)
    {
        var response = await mediator.Send(userCreateCommand);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT: api/<UsersController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, UserUpdateCommand userUpdateCommand)
    {
        userUpdateCommand.Id = id;
        await mediator.Send(userUpdateCommand);
        return NoContent();
    }
}