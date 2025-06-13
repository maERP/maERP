using Asp.Versioning;
using maERP.Application.Features.DemoData.Commands.AllDemoData;
using maERP.Application.Features.DemoData.Commands.AiDemoData;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

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
}