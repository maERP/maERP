using Asp.Versioning;
using maERP.Application.Features.DemoData.Commands.AllDemoData;
using maERP.Application.Features.DemoData.Commands.AiDemoData;
using maERP.Application.Features.DemoData.Commands.ClearAllData;
using maERP.Application.Features.DemoData.Commands.TenantDemoData;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace maERP.Server.Controllers.Api.V1;

#if DEBUG
[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class DemoDataController(IMediator mediator) : ControllerBase
{
    [HttpPost("all")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> CreateAllDemoData()
    {
        var command = new AllDemoDataCommand();
        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost("ai")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> CreateAiDemoData()
    {
        var command = new AiDemoDataCommand();
        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost("tenants")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> CreateTenantDemoData()
    {
        var command = new TenantDemoDataCommand();
        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("clear")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<string>>> ClearAllData()
    {
        var command = new ClearAllDataCommand();
        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}
#endif