using Asp.Versioning;
using maERP.Application.Features.AiModel.Commands.AiModelCreate;
using maERP.Application.Features.AiModel.Commands.AiModelDelete;
using maERP.Application.Features.AiModel.Commands.AiModelUpdate;
using maERP.Application.Features.AiModel.Queries.AiModelDetail;
using maERP.Application.Features.AiModel.Queries.AiModelList;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class AiModelsController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/<AiModelsController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<AiModelListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var response = await mediator.Send(new AiModelListQuery(pageNumber, pageSize, searchString, orderBy));
        return StatusCode((int)response.StatusCode, response);
    }

    // GET: api/v1/<AiModelsController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AiModelDetailDto>> GetDetails(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            return BadRequest(Result<AiModelDetailDto>.Fail(ResultStatusCode.BadRequest, "Invalid GUID format"));
        }
        
        var response = await mediator.Send(new AiModelDetailQuery { Id = guidId });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST: api/v1/<AiModelsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(AiModelCreateCommand aiModelCreateCommand)
    {
        var response = await mediator.Send(aiModelCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // PUT: api/v1/<AiModelsController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AiModelDetailDto>> Update(string id, AiModelUpdateCommand aiModelUpdateCommand)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            return BadRequest(Result<Guid>.Fail(ResultStatusCode.BadRequest, "Invalid GUID format"));
        }
        
        aiModelUpdateCommand.Id = guidId;
        var response = await mediator.Send(aiModelUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // DELETE: api/v1/<AiModelsController>/5
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
        
        var command = new AiModelDeleteCommand { Id = guidId };
        await mediator.Send(command);
        return NoContent();
    }
}
