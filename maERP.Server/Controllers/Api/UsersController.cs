using maERP.Application.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using maERP.Application.Features.User.Commands.CreateUserCommand;
using maERP.Application.Features.User.Commands.DeleteUserCommand;
using maERP.Application.Features.User.Commands.UpdateUserCommand;
using maERP.Application.Features.User.Queries.GetUserDetailQuery;
using maERP.Application.Features.User.Queries.GetUsersQuery;

namespace maERP.Server.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IMediator mediator) : ControllerBase
{
    // GET: api/<UsersController>
    [HttpGet]
    public async Task<List<UserListDto>> Get()
    {
        var users = await mediator.Send(new GetUsersQuery());
        return users;
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public async Task<UserDetailDto> GetDetails(string id)
    {
        return await mediator.Send(new GetUserDetailQuery() { Id = id });
    }

    // POST api/<UsersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateUserCommand createUserCommand)
    {
        var response = await mediator.Send(createUserCommand);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT: api/<UsersController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateUserCommand updateUserCommand)
    {
        await mediator.Send(updateUserCommand);
        return NoContent();
    }

    // DELETE api/<UserController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteUserCommand { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}