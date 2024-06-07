using Asp.Versioning;
using maERP.Application.Features.User.Commands.CreateUserCommand;
using maERP.Application.Features.User.Commands.UpdateUserCommand;
using maERP.Application.Features.User.Queries.GetUserDetails;
using maERP.Application.Features.User.Queries.GetUsers;
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
    public async Task<ActionResult> Get()
    {
        var users = await mediator.Send(new GetUsersQuery());
        return Ok(users);
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> GetDetails(string id)
    {
        var user = await mediator.Send(new GetUserDetailsQuery { Id = id });
        return Ok(user);
    }

    // POST api/<UsersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(CreateUserCommand createUserCommand)
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
    public async Task<ActionResult> Update(int id, UpdateUserCommand updateUserCommand)
    {
        updateUserCommand.Id = id;
        await mediator.Send(updateUserCommand);
        return NoContent();
    }
}