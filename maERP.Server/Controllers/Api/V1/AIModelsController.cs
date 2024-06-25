using Asp.Versioning;
using maERP.Application.Features.AIModel.Commands.AIModelCreate;
using maERP.Application.Features.AIModel.Commands.AIModelDelete;
using maERP.Application.Features.AIModel.Commands.AIModelUpdate;
using maERP.Application.Features.AIModel.Queries.AIModelDetail;
using maERP.Application.Features.AIModel.Queries.AIModelList;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class AIModelsController(IMediator _mediator) : ControllerBase
{
    // GET: api/<AIModelsController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<AIModelListResponse>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var aiModels = await _mediator.Send(new AIModelListQuery(pageNumber, pageSize, searchString, orderBy));
        return Ok(aiModels);
    }

    // GET api/<AIModelsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AIModelDetailResponse>> GetDetails(int id)
    {
        var aiModel = await _mediator.Send(new AIModelDetailQuery { Id = id });
        return Ok(aiModel);
    }

    // POST api/<AIModelsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(AIModelCreateCommand aiModelCreateCommand)
    {
        var response = await _mediator.Send(aiModelCreateCommand);
        return CreatedAtAction(nameof(GetDetails), new { id = response });
    }

    // PUT api/<AIModelsController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AIModelDetailResponse>> Update(int id, AIModelUpdateCommand aiModelUpdateCommand)
    {
        aiModelUpdateCommand.Id = id;
        await _mediator.Send(aiModelUpdateCommand);
        return NoContent();
    }

    // DELETE api/<AIModelsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new AIModelDeleteCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
