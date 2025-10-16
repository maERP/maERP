using Asp.Versioning;
using maERP.Application.Features.Superadmin.Users.Commands.UserCreate;
using maERP.Application.Features.Superadmin.Users.Commands.UserDelete;
using maERP.Application.Features.Superadmin.Users.Commands.UserUpdate;
using maERP.Application.Features.Superadmin.Users.Queries.UserDetail;
using maERP.Application.Features.Superadmin.Users.Queries.UserList;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using maERP.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/<UsersController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedResult<UserListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "Lastname Ascending";
        }

        var response = await mediator.Send(new UserListQuery(pageNumber, pageSize, searchString, orderBy));
        return response.ToActionResult();
    }

    // GET: api/v1/<UsersController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDetailDto>> GetDetails(string id)
    {
        var response = await mediator.Send(new UserDetailQuery { Id = id });
        return response.ToActionResult();
    }

    // POST: api/v1/<UsersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Create(UserCreateCommand userCreateCommand)
    {
        var response = await mediator.Send(userCreateCommand);
        return response.ToActionResult();
    }

    // PUT: api/v1/<UsersController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(string id, UserUpdateCommand userUpdateCommand)
    {
        // Validate ID consistency between URL and body if ID is provided in body and differs
        if (!string.IsNullOrEmpty(userUpdateCommand.Id) && userUpdateCommand.Id != id)
        {
            var errorResponse = ProblemDetailsResult.BadRequest(
                "Invalid Request",
                $"ID in URL ({id}) must match ID in request body ({userUpdateCommand.Id})",
                "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                $"/api/v1/users/{id}"
            );
            return errorResponse.ToActionResult();
        }

        userUpdateCommand.Id = id;
        var response = await mediator.Send(userUpdateCommand);
        return response.ToActionResult();
    }

    // DELETE: api/v1/<UsersController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(string id)
    {
        var command = new UserDeleteCommand { Id = id };
        var response = await mediator.Send(command);
        return response.ToActionResult();
    }
}
