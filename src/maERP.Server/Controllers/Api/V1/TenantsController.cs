using Asp.Versioning;
using maERP.Application.Features.Tenant.Commands.TenantCreate;
using maERP.Application.Features.Tenant.Commands.TenantUpdate;
using maERP.Application.Features.Tenant.Commands.TenantUserAdd;
using maERP.Application.Features.Tenant.Queries.TenantDetail;
using maERP.Application.Features.Tenant.Queries.TenantList;
using maERP.Application.Features.Tenant.Queries.TenantUserSearch;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/tenants")]
public class TenantsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Get list of tenants assigned to the current user
    /// </summary>
    /// <param name="pageNumber">Page number (default: 0, zero-based)</param>
    /// <param name="pageSize">Page size (default: 10, max: 100)</param>
    /// <param name="searchString">Search string to filter tenants</param>
    /// <param name="orderBy">Order by fields (comma-separated)</param>
    /// <returns>Paginated list of tenants</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PaginatedResult<TenantListDto>>> GetTenants(
        [FromQuery] int pageNumber = 0,
        [FromQuery] int pageSize = 10,
        [FromQuery] string searchString = "",
        [FromQuery] string orderBy = "")
    {
        // Get the current user's ID from the authenticated claims
        // Try "uid" claim first (JWT), then fall back to NameIdentifier (Test/Standard)
        var userId = User.FindFirst("uid")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new PaginatedResult<TenantListDto>(new List<TenantListDto>())
            {
                Succeeded = false,
                StatusCode = ResultStatusCode.Unauthorized,
                Messages = new List<string> { "User ID not found in token" }
            });
        }

        var query = new TenantListQuery(userId, pageNumber, pageSize, searchString, orderBy);
        var response = await mediator.Send(query);

        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Get detailed information about a specific tenant
    /// </summary>
    /// <param name="id">Tenant ID</param>
    /// <returns>Detailed tenant information</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<TenantDetailDto>>> GetTenant(Guid id)
    {
        // Get the current user's ID from the authenticated claims
        // Try "uid" claim first (JWT), then fall back to NameIdentifier (Test/Standard)
        var userId = User.FindFirst("uid")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new Result<TenantDetailDto>
            {
                Succeeded = false,
                StatusCode = ResultStatusCode.Unauthorized,
                Messages = new List<string> { "User ID not found in token" }
            });
        }

        var query = new TenantDetailQuery { Id = id, UserId = userId };
        var response = await mediator.Send(query);

        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Create a new tenant and automatically assign the current user to it
    /// </summary>
    /// <param name="command">Tenant creation data</param>
    /// <returns>Created tenant ID</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<Guid>>> CreateTenant([FromBody] TenantCreateCommand command)
    {
        // Get the current user's ID from the authenticated claims
        // Try "uid" claim first (JWT), then fall back to NameIdentifier (Test/Standard)
        var userId = User.FindFirst("uid")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new Result<Guid>
            {
                Succeeded = false,
                StatusCode = ResultStatusCode.Unauthorized,
                Messages = new List<string> { "User ID not found in token" }
            });
        }

        // Set the user ID on the command
        command.UserId = userId;

        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Update an existing tenant (requires RoleManageTenant permission)
    /// </summary>
    /// <param name="id">Tenant ID</param>
    /// <param name="command">Tenant update data</param>
    /// <returns>Updated tenant ID</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<Guid>>> UpdateTenant(Guid id, [FromBody] TenantUpdateCommand command)
    {
        // Get the current user's ID from the authenticated claims
        // Try "uid" claim first (JWT), then fall back to NameIdentifier (Test/Standard)
        var userId = User.FindFirst("uid")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new Result<Guid>
            {
                Succeeded = false,
                StatusCode = ResultStatusCode.Unauthorized,
                Messages = new List<string> { "User ID not found in token" }
            });
        }

        // Set the user ID and tenant ID on the command
        command.UserId = userId;
        command.TenantId = id;

        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Search for a user by email address to add to a tenant.
    /// Requires RoleManageUser permission on the tenant.
    /// </summary>
    /// <param name="id">Tenant ID</param>
    /// <param name="email">Email address to search for</param>
    /// <returns>User details if found</returns>
    [HttpGet("{id:guid}/users/search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<UserListDto?>>> SearchUser(Guid id, [FromQuery] string email)
    {
        var userId = User.FindFirst("uid")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new Result<UserListDto?>
            {
                Succeeded = false,
                StatusCode = ResultStatusCode.Unauthorized,
                Messages = new List<string> { "User ID not found in token" }
            });
        }

        var query = new TenantUserSearchQuery
        {
            TenantId = id,
            CurrentUserId = userId,
            Email = email
        };

        var response = await mediator.Send(query);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Add a user to a tenant by email address.
    /// Requires RoleManageUser permission on the tenant.
    /// </summary>
    /// <param name="id">Tenant ID</param>
    /// <param name="command">User add command containing email and options</param>
    /// <returns>Success status</returns>
    [HttpPost("{id:guid}/users")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<bool>>> AddUser(Guid id, [FromBody] TenantUserAddCommand command)
    {
        var userId = User.FindFirst("uid")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new Result<bool>
            {
                Succeeded = false,
                StatusCode = ResultStatusCode.Unauthorized,
                Messages = new List<string> { "User ID not found in token" }
            });
        }

        command.CurrentUserId = userId;
        command.TenantId = id;

        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}
