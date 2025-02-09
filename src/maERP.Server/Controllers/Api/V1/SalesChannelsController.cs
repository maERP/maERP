using Asp.Versioning;
using maERP.Application.Exceptions;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelDelete;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelUpdate;
using maERP.Application.Features.SalesChannel.Queries.SalesChannelDetail;
using maERP.Application.Features.SalesChannel.Queries.SalesChannelList;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class SalesChannelsController(IMediator mediator) : ControllerBase
{
    // GET: api/<SalesChannelsController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<SalesChannelListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var salesChannels = await mediator.Send(new SalesChannelListQuery(pageNumber, pageSize, searchString, orderBy));
        return Ok(salesChannels);
    }

    // GET api/<SalesChannelsController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SalesChannelDetailDto>> GetDetails(int id)
    {
        try 
        {
            var salesChannel = await mediator.Send(new SalesChannelDetailQuery { Id = id });
            return Ok(salesChannel);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    // POST api/<SalesChannelsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(SalesChannelCreateCommand salesChannelCreateCommand)
    {
        var response = await mediator.Send(salesChannelCreateCommand);
        return CreatedAtAction(nameof(GetAll), new { id = response });
    }

    // TODO: fix this
    /*
    // PUT: api/<SalesChannelsController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, SalesChannelUpdateCommand salesChannelUpdateCommand)
    {
        salesChannelUpdateCommand.Id = id;
        await mediator.Send(salesChannelUpdateCommand);
        return NoContent();
    }
    */

    // DELETE api/<SalesChannelController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new SalesChanneLDeleteCommand { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}