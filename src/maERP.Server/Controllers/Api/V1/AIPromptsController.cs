using Asp.Versioning;
using maERP.Application.Features.AiPrompt.Commands.AiPromptCreate;
using maERP.Application.Features.AiPrompt.Commands.AiPromptDelete;
using maERP.Application.Features.AiPrompt.Commands.AiPromptUpdate;
using maERP.Application.Features.AiPrompt.Queries.AiPromptDetail;
using maERP.Application.Features.AiPrompt.Queries.AiPromptList;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using maERP.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class AiPromptsController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/<AiPromptsController>
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

    // GET: api/v1/<AiPromptsController>/5
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AiPromptDetailDto>> GetDetails(Guid id)
    {
        var response = await mediator.Send(new AiPromptDetailQuery { Id = id });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST: api/v1/<AiPromptsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(AiPromptCreateCommand aIPromptCreateCommand)
    {
        var response = await mediator.Send(aIPromptCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // PUT: api/v1/<AiPromptsController>/5
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AiPromptDetailDto>> Update(Guid id, AiPromptUpdateCommand aIPromptUpdateCommand)
    {
        // Validate ID is not zero
        if (id == Guid.Empty)
        {
            var invalidIdResponse = ProblemDetailsResult.BadRequest(
                "Invalid Request", 
                $"AiPrompt ID must be greater than zero",
                "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                $"/api/v1/aiprompts/{id}"
            );
            return invalidIdResponse.ToActionResult();
        }

        // Validate ID consistency between URL and body if ID is provided in body and differs
        if (aIPromptUpdateCommand.Id != Guid.Empty && aIPromptUpdateCommand.Id != id)
        {
            var errorResponse = ProblemDetailsResult.BadRequest(
                "Invalid Request", 
                $"ID in URL ({id}) must match ID in request body ({aIPromptUpdateCommand.Id})",
                "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                $"/api/v1/aiprompts/{id}"
            );
            return errorResponse.ToActionResult();
        }

        aIPromptUpdateCommand.Id = id;
        var response = await mediator.Send(aIPromptUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // DELETE: api/v1/<AiPromptsController>/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Result<int>>> Delete(Guid id)
    {
        var command = new AiPromptDeleteCommand { Id = id };
        var response = await mediator.Send(command);
        return response.ToActionResult();
    }
}
