using Asp.Versioning;
using maERP.Application.Features.TenantEmailSettings.Commands.TenantEmailSettingsDelete;
using maERP.Application.Features.TenantEmailSettings.Commands.TenantEmailSettingsTestSend;
using maERP.Application.Features.TenantEmailSettings.Commands.TenantEmailSettingsUpsert;
using maERP.Application.Features.TenantEmailSettings.Queries.TenantEmailSettingsDetail;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.TenantEmailSettings;
using maERP.Domain.Wrapper;
using maERP.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class TenantEmailSettingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TenantEmailSettingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TenantEmailSettingsDetailDto>> Get()
    {
        var response = await _mediator.Send(new TenantEmailSettingsDetailQuery());
        return response.ToActionResult();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Upsert([FromBody] TenantEmailSettingsUpsertCommand command)
    {
        var response = await _mediator.Send(command);
        return response.ToActionResult();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete()
    {
        var response = await _mediator.Send(new TenantEmailSettingsDeleteCommand());
        return response.ToActionResult();
    }

    [HttpPost("test-send")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> TestSend([FromBody] TenantEmailSettingsTestSendCommand command)
    {
        var response = await _mediator.Send(command);
        return response.ToActionResult();
    }
}
