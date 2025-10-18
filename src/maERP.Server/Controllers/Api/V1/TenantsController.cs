using Asp.Versioning;
using maERP.Application.Features.Tenant.Commands.TenantCreate;
using maERP.Application.Mediator;
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
        var userId = User.FindFirst("uid")?.Value;

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
}
