using Asp.Versioning;
using maERP.Application.Features.Tenant.Commands.TenantCreate;
using maERP.Application.Features.Tenant.Commands.TenantDelete;
using maERP.Application.Features.Tenant.Commands.TenantUpdate;
using maERP.Application.Features.Tenant.Queries.TenantDetail;
using maERP.Application.Features.Tenant.Queries.TenantList;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class TenantsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Get a paginated list of all tenants
    /// </summary>
    /// <param name="pageNumber">Page number (default: 0)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <param name="searchString">Search string for filtering tenants</param>
    /// <param name="orderBy">Order by clause</param>
    /// <returns>Paginated list of tenants</returns>
    [HttpGet]
    [Authorize(Roles = "Superadmin")]
    public async Task<ActionResult<PaginatedResult<TenantListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "Name Ascending";
        }

        var response = await mediator.Send(new TenantListQuery(pageNumber, pageSize, searchString, orderBy));
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Get details of a specific tenant
    /// </summary>
    /// <param name="id">Tenant ID</param>
    /// <returns>Tenant details</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TenantDetailDto>> GetDetails(int id)
    {
        var response = await mediator.Send(new TenantDetailQuery(id));
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Create a new tenant
    /// </summary>
    /// <param name="tenantCreateCommand">Tenant creation data</param>
    /// <returns>Created tenant ID</returns>
    [HttpPost]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<int>>> Create(TenantCreateCommand tenantCreateCommand)
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
    [HttpPut("{id}")]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Result<int>>> Update(int id, TenantUpdateCommand tenantUpdateCommand)
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
    [HttpDelete("{id}")]
    [Authorize(Roles = "Superadmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new TenantDeleteCommand(id);
        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}