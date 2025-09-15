using Asp.Versioning;
using maERP.Application.Features.User.Commands.UserCreate;
using maERP.Application.Features.User.Commands.UserDelete;
using maERP.Application.Features.User.Commands.UserUpdate;
using maERP.Application.Features.User.Queries.UserDetail;
using maERP.Application.Features.User.Queries.UserList;
using maERP.Application.Features.UserTenant.Commands.AssignUserToTenant;
using maERP.Application.Features.UserTenant.Commands.RemoveUserFromTenant;
using maERP.Application.Features.UserTenant.Queries.GetUserTenants;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
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

    // GET: api/v1/<UsersController>
    [HttpGet]
    [Authorize(Roles = "Superadmin")]
    public async Task<ActionResult<PaginatedResult<UserListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var response = await _mediator.Send(new UserListQuery(pageNumber, pageSize, searchString, orderBy));
        return StatusCode((int)response.StatusCode, response);
    }

    // GET api/UsersController>/5
    [HttpGet("{id}")]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDetailDto>> GetDetails(string id)
    {
        var response = await _mediator.Send(new UserDetailQuery { Id = id });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST: api/v1/<UsersController>
    [HttpPost]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Create(UserCreateCommand userCreateCommand)
    {
        var response = await _mediator.Send(userCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // PUT: api/v1/<UsersController>/5
    [HttpPut("{id}")]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(string id, UserUpdateCommand userUpdateCommand)
    {
        userUpdateCommand.Id = id;
        var response = await _mediator.Send(userUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // DELETE: api/v1/<UsersController>/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(string id)
    {
        var command = new UserDeleteCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Get tenant assignments for a specific user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>List of tenant assignments for the user</returns>
    [HttpGet("{id}/tenants")]
    [Authorize(Roles = "Superadmin,Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<List<UserTenantAssignmentDto>>>> GetUserTenants(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("User ID cannot be empty");
        }

        var response = await _mediator.Send(new GetUserTenantsQuery(id));
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Assign a user to a tenant
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="command">Assignment command</param>
    /// <returns>Assignment ID</returns>
    [HttpPost("{id}/tenants")]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<int>>> AssignUserToTenant(string id, AssignUserToTenantCommand command)
    {
        command.UserId = id;
        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Remove a user from a tenant
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}/tenants/{tenantId:guid}")]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RemoveUserFromTenant(string id, Guid tenantId)
    {
        var command = new RemoveUserFromTenantCommand
        {
            UserId = id,
            TenantId = tenantId
        };

        var response = await _mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}
