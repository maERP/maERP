using Asp.Versioning;
using maERP.Application.Exceptions;
using maERP.Application.Features.User.Commands.UserCreate;
using maERP.Application.Features.User.Commands.UserDelete;
using maERP.Application.Features.User.Commands.UserUpdate;
using maERP.Application.Features.User.Queries.UserDetail;
using maERP.Application.Features.User.Queries.UserList;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<UsersController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<UserListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var users = await _mediator.Send(new UserListQuery(pageNumber, pageSize, searchString, orderBy));
        return Ok(users);
    }

    // GET api/UsersController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDetailDto>> GetDetails(string id)
    {
        try 
        {
            var user = await _mediator.Send(new UserDetailQuery { Id = id });
            return Ok(user);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    // POST api/<UsersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Create(UserCreateCommand userCreateCommand)
    {
        var response = await _mediator.Send(userCreateCommand);
        return CreatedAtAction(nameof(GetDetails), new { id = response });
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(string id, UserUpdateCommand userUpdateCommand)
    {
        userUpdateCommand.Id = id;
        await _mediator.Send(userUpdateCommand);
        return NoContent();
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(string id)
    {
        var command = new UserDeleteCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
