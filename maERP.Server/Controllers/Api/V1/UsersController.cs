using MediatR;
using Microsoft.AspNetCore.Mvc;
using maERP.Application.Features.User.Commands.CreateUserCommand;
using maERP.Application.Features.User.Commands.UpdateUserCommand;
using maERP.Application.Features.User.Queries.GetUserDetailQuery;
using maERP.Application.Features.User.Queries.GetUsersQuery;
using Asp.Versioning;
using maERP.Application.Dtos.User;
using Microsoft.AspNetCore.Authorization;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
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
    public async Task<ActionResult> Update(UpdateUserCommand updateUserCommand)
    {
        await mediator.Send(updateUserCommand);
        return NoContent();
    }
}