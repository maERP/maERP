using Asp.Versioning;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelDelete;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelUpdate;
using maERP.Application.Features.SalesChannel.Queries.SalesChannelDetail;
using maERP.Application.Features.SalesChannel.Queries.SalesChannelList;
using maERP.Domain.Dtos.SalesChannel;
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
public class SalesChannelsController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/<SalesChannelsController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<SalesChannelListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var response = await mediator.Send(new SalesChannelListQuery(pageNumber, pageSize, searchString, orderBy));
        return StatusCode((int)response.StatusCode, response);
    }

    // GET: api/v1/<SalesChannelsController>/5
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SalesChannelDetailDto>> GetDetails(Guid id)
    {
        var response = await mediator.Send(new SalesChannelDetailQuery { Id = id });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST: api/v1/<SalesChannelsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(SalesChannelCreateCommand salesChannelCreateCommand)
    {
        var response = await mediator.Send(salesChannelCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // PUT: api/v1/<SalesChannelsController>/5
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(Guid id, SalesChannelUpdateCommand salesChannelUpdateCommand)
    {
        // Validate ID consistency between URL and request body
        if (salesChannelUpdateCommand.Id != Guid.Empty && salesChannelUpdateCommand.Id != id)
        {
            var errorResult = new Result<Guid>
            {
                Succeeded = false,
                StatusCode = ResultStatusCode.BadRequest,
                Messages = { "ID in URL does not match ID in request body" }
            };
            return BadRequest(errorResult);
        }

        salesChannelUpdateCommand.Id = id;
        var response = await mediator.Send(salesChannelUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // DELETE: api/v1/<SalesChannelController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            return BadRequest(new { Error = "Invalid ID format" });
        }

        var command = new SalesChannelDeleteCommand { Id = guidId };
        var response = await mediator.Send(command);
        return response.ToActionResult();
    }
}