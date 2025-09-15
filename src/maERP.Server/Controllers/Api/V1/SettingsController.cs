using Asp.Versioning;
using maERP.Application.Features.Setting.Commands.SettingCreate;
using maERP.Application.Features.Setting.Commands.SettingDelete;
using maERP.Application.Features.Setting.Commands.SettingUpdate;
using maERP.Application.Features.Setting.Queries.SettingDetail;
using maERP.Application.Features.Setting.Queries.SettingList;
using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class SettingsController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/<SettingsController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<SettingListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var response = await mediator.Send(new SettingListQuery(pageNumber, pageSize, searchString, orderBy));
        return StatusCode((int)response.StatusCode, response);
    }

    // GET: api/v1/<SettingsController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SettingDetailDto>> GetDetails(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            return BadRequest(Result<SettingDetailDto>.Fail(ResultStatusCode.BadRequest, "Invalid GUID format"));
        }

        var response = await mediator.Send(new SettingDetailQuery { Id = guidId });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST: api/v1/<SettingsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(SettingCreateCommand settingCreateCommand)
    {
        var response = await mediator.Send(settingCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // PUT: api/v1/<SettingsController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Result<Guid>>> Update(string id, SettingUpdateCommand settingUpdateCommand)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            return BadRequest(Result<Guid>.Fail(ResultStatusCode.BadRequest, "Invalid GUID format"));
        }

        // Validate that URL ID matches the ID in the request body (if provided)
        if (settingUpdateCommand.Id != Guid.Empty && settingUpdateCommand.Id != guidId)
        {
            return BadRequest(Result<Guid>.Fail(ResultStatusCode.BadRequest, "ID in URL does not match ID in request body"));
        }

        settingUpdateCommand.Id = guidId;
        var response = await mediator.Send(settingUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // DELETE: api/v1/<SettingsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            return BadRequest(Result.Fail("Invalid GUID format"));
        }

        var command = new SettingDeleteCommand { Id = guidId };
        var result = await mediator.Send(command);

        // For successful deletes, return NoContent without body
        if (result.Succeeded && result.StatusCode == ResultStatusCode.NoContent)
        {
            return NoContent();
        }

        return StatusCode((int)result.StatusCode, result);
    }
}
