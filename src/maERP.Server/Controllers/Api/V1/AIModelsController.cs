using Asp.Versioning;
using maERP.Application.Exceptions;
using maERP.Application.Features.AiModel.Commands.AiModelCreate;
using maERP.Application.Features.AiModel.Commands.AiModelDelete;
using maERP.Application.Features.AiModel.Commands.AiModelUpdate;
using maERP.Application.Features.AiModel.Queries.AiModelDetail;
using maERP.Application.Features.AiModel.Queries.AiModelList;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class AiModelsController(IMediator _mediator) : ControllerBase
{
    // GET: api/<AiModelsController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<AiModelListResponse>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var aiModels = await _mediator.Send(new AiModelListQuery(pageNumber, pageSize, searchString, orderBy));
        return Ok(aiModels);
    }

    // GET api/<AiModelsController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AiModelDetailResponse>> GetDetails(int id)
    {
        try 
        {
            var aiModel = await _mediator.Send(new AiModelDetailQuery { Id = id });
            return Ok(aiModel);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    // POST api/<AiModelsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(AiModelCreateCommand aiModelCreateCommand)
    {
        var response = await _mediator.Send(aiModelCreateCommand);
        return CreatedAtAction(nameof(GetDetails), new { id = response });
    }

    // PUT api/<AiModelsController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AiModelDetailResponse>> Update(int id, AiModelUpdateCommand aiModelUpdateCommand)
    {
        aiModelUpdateCommand.Id = id;
        await _mediator.Send(aiModelUpdateCommand);
        return NoContent();
    }

    // DELETE api/<AiModelsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new AiModelDeleteCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
