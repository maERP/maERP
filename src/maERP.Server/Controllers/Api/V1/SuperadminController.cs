using Asp.Versioning;
using maERP.Application.Features.Superadmin.Commands.SuperadminCreate;
using maERP.Application.Features.Superadmin.Commands.SuperadminDelete;
using maERP.Application.Features.Superadmin.Commands.SuperadminUpdate;
using maERP.Application.Features.Superadmin.Queries.SuperadminDetail;
using maERP.Application.Features.Superadmin.Queries.SuperadminList;
using maERP.Application.Features.Superadmin.UserTenants.Commands.AssignUserToTenant;
using maERP.Application.Features.Superadmin.UserTenants.Commands.RemoveUserFromTenant;
using maERP.Application.Features.Superadmin.UserTenants.Queries.GetUserTenants;
using maERP.Application.Features.Superadmin.Users.Commands.UserCreate;
using maERP.Application.Features.Superadmin.Users.Commands.UserDelete;
using maERP.Application.Features.Superadmin.Users.Commands.UserUpdate;
using maERP.Application.Features.Superadmin.Users.Queries.UserDetail;
using maERP.Application.Features.Superadmin.Users.Queries.UserList;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/superadmin")]
public class SuperadminController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Get a paginated list of all tenants
    /// </summary>
    /// <param name="pageNumber">Page number (default: 0)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <param name="searchString">Search string for filtering tenants</param>
    /// <param name="orderBy">Order by clause</param>
    /// <returns>Paginated list of tenants</returns>
    [HttpGet("tenants")]
    [Authorize(Roles = "Superadmin")]
    public async Task<ActionResult<PaginatedResult<TenantListDto>>> GetTenants(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "Name Ascending";
        }

        var response = await mediator.Send(new SuperadminListQuery(pageNumber, pageSize, searchString, orderBy));
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Get details of a specific tenant
    /// </summary>
    /// <param name="id">Tenant ID</param>
    /// <returns>Tenant details</returns>
    [HttpGet("tenants/{id:guid}")]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TenantDetailDto>> GetTenantDetails(Guid id)
    {
        var response = await mediator.Send(new SuperadminDetailQuery(id));
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Create a new tenant
    /// </summary>
    /// <param name="tenantCreateCommand">Tenant creation data</param>
    /// <returns>Created tenant ID</returns>
    [HttpPost("tenants")]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<int>>> CreateTenant(SuperadminCreateCommand tenantCreateCommand)
    {
        var response = await mediator.Send(tenantCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Update an existing tenant
    /// </summary>
    /// <param name="id">Tenant ID</param>
    /// <param name="tenantUpdateCommand">Tenant update data</param>
    /// <returns>Updated tenant ID</returns>
    [HttpPut("tenants/{id:guid}")]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Result<int>>> UpdateTenant(Guid id, SuperadminUpdateCommand tenantUpdateCommand)
    {
        tenantUpdateCommand.Id = id;
        var response = await mediator.Send(tenantUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Delete a tenant
    /// </summary>
    /// <param name="id">Tenant ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("tenants/{id:guid}")]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteTenant(Guid id)
    {
        var command = new SuperadminDeleteCommand(id);
        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Get a paginated list of all users
    /// </summary>
    /// <param name="pageNumber">Page number (default: 0)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <param name="searchString">Search string for filtering users</param>
    /// <param name="orderBy">Order by clause</param>
    /// <returns>Paginated list of users</returns>
    [HttpGet("users")]
    public async Task<ActionResult<PaginatedResult<UserListDto>>> GetUsers(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var response = await mediator.Send(new UserListQuery(pageNumber, pageSize, searchString, orderBy));
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Get details of a specific user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>User details</returns>
    [HttpGet("users/{id:minlength(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDetailDto>> GetUserDetails(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest("User ID cannot be empty");
        }

        var response = await mediator.Send(new UserDetailQuery { Id = id });
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="userCreateCommand">User creation data</param>
    /// <returns>Created user identifier</returns>
    [HttpPost("users")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> CreateUser(UserCreateCommand userCreateCommand)
    {
        var response = await mediator.Send(userCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Update an existing user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="userUpdateCommand">User update payload</param>
    [HttpPut("users/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateUser(string id, UserUpdateCommand userUpdateCommand)
    {
        if (!string.IsNullOrWhiteSpace(userUpdateCommand.Id) &&
            !string.Equals(userUpdateCommand.Id, id, StringComparison.OrdinalIgnoreCase))
        {
            var mismatchResult = Result<string>.Fail(ResultStatusCode.BadRequest, "User ID in the payload must match the route parameter.");
            return BadRequest(mismatchResult);
        }

        userUpdateCommand.Id = id;
        var response = await mediator.Send(userUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Placeholder delete route without identifier
    /// </summary>
    [HttpDelete("users")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteUser()
    {
        return NotFound();
    }

    /// <summary>
    /// Delete a specific user
    /// </summary>
    /// <param name="id">User ID</param>
    [HttpDelete("users/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteUser(string id)
    {
        var command = new UserDeleteCommand { Id = id };
        var response = await mediator.Send(command);
        if (response.StatusCode == ResultStatusCode.NoContent)
        {
            return NoContent();
        }

        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Get tenant assignments for a specific user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>List of tenant assignments for the user</returns>
    [HttpGet("users/{id}/tenants")]
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

        var response = await mediator.Send(new GetUserTenantsQuery(id));
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Assign a user to a tenant
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="command">Assignment command</param>
    /// <returns>Assignment ID</returns>
    [HttpPost("users/{id}/tenants")]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<int>>> AssignUserToTenant(string id, AssignUserToTenantCommand command)
    {
        command.UserId = id;
        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Remove a user from a tenant
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("users/{id}/tenants/{tenantId}")]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RemoveUserFromTenant(string id, string tenantId)
    {
        if (!Guid.TryParse(tenantId, out var parsedTenantId) || parsedTenantId == Guid.Empty)
        {
            var invalidResult = Result<bool>.Fail(ResultStatusCode.BadRequest, "Invalid tenant identifier.");
            return BadRequest(invalidResult);
        }

        var command = new RemoveUserFromTenantCommand
        {
            UserId = id,
            TenantId = parsedTenantId
        };

        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}
