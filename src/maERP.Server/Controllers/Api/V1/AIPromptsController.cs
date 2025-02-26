using Asp.Versioning;
using maERP.Application.Features.AiPrompt.Commands.AiPromptCreate;
using maERP.Application.Features.AiPrompt.Commands.AiPromptDelete;
using maERP.Application.Features.AiPrompt.Commands.AiPromptUpdate;
using maERP.Application.Features.AiPrompt.Queries.AiPromptDetail;
using maERP.Application.Features.AiPrompt.Queries.AiPromptList;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class AiPromptsController(IMediator mediator) : ControllerBase
{
    // GET: api/<AiPromptsController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<AiPromptListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var response = await mediator.Send(new AiPromptListQuery(pageNumber, pageSize, searchString, orderBy));
        return StatusCode((int)response.StatusCode, response);
    }

    // GET api/<AiPromptsController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AiPromptDetailDto>> GetDetails(int id)
    {
        var response = await mediator.Send(new AiPromptDetailQuery { Id = id });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST api/<AiPromptsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(AiPromptCreateCommand aIPromptCreateCommand)
    {
        var response = await mediator.Send(aIPromptCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // PUT api/<AiPromptsController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AiPromptDetailDto>> Update(int id, AiPromptUpdateCommand aIPromptUpdateCommand)
    {
        aIPromptUpdateCommand.Id = id;
        var response = await mediator.Send(aIPromptUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // DELETE api/<AiPromptsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new AiPromptDeleteCommand { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}
