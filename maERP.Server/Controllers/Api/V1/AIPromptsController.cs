using Asp.Versioning;
using maERP.Application.Features.AIPrompt.Commands.AIPromptCreate;
using maERP.Application.Features.AIPrompt.Commands.AIPromptDelete;
using maERP.Application.Features.AIPrompt.Commands.AIPromptUpdate;
using maERP.Application.Features.AIPrompt.Queries.AIPromptDetail;
using maERP.Application.Features.AIPrompt.Queries.AIPromptList;
using maERP.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class AIPromptsController(IMediator _mediator) : ControllerBase
{
    // GET: api/<AIPromptsController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<AIPromptListResponse>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var aIPrompts = await _mediator.Send(new AIPromptListQuery(pageNumber, pageSize, searchString, orderBy));
        return Ok(aIPrompts);
    }

    // GET api/<AIPromptsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AIPromptDetailResponse>> GetDetails(int id)
    {
        var aIPrompt = await _mediator.Send(new AIPromptDetailQuery { Id = id });
        return Ok(aIPrompt);
    }

    // POST api/<AIPromptsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(AIPromptCreateCommand aIPromptCreateCommand)
    {
        var response = await _mediator.Send(aIPromptCreateCommand);
        return CreatedAtAction(nameof(GetDetails), new { id = response });
    }

    // PUT api/<AIPromptsController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AIPromptDetailResponse>> Update(int id, AIPromptUpdateCommand aIPromptUpdateCommand)
    {
        aIPromptUpdateCommand.Id = id;
        await _mediator.Send(aIPromptUpdateCommand);
        return NoContent();
    }

    // DELETE api/<AIPromptsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new AIPromptDeleteCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
