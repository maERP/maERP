using Asp.Versioning;
using maERP.Application.Features.Order.Queries.OrderList;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelDelete;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelUpdate;
using maERP.Application.Features.SalesChannel.Queries.SalesChannelDetail;
using maERP.Application.Features.SalesChannel.Queries.SalesChannelList;
using maERP.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class SalesChannelsController(IMediator _mediator) : ControllerBase
{
    // GET: api/<SalesChannelsController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<SalesChannelListResponse>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var salesChannels = await _mediator.Send(new SalesChannelListQuery(pageNumber, pageSize, searchString, orderBy));
        return Ok(salesChannels);
    }

    // GET api/<SalesChannelsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SalesChannelDetailResponse>> GetDetails(int id)
    {
        var salesChannel = await _mediator.Send(new SalesChannelDetailQuery { Id = id });
        return Ok(salesChannel);
    }

    // POST api/<SalesChannelsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(SalesChannelCreateCommand salesChannelCreateCommand)
    {
        var response = await _mediator.Send(salesChannelCreateCommand);
        return CreatedAtAction(nameof(GetAll), new { id = response });
    }

    // PUT: api/<SalesChannelsController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, SalesChannelUpdateCommand salesChannelUpdateCommand)
    {
        salesChannelUpdateCommand.Id = id;
        await _mediator.Send(salesChannelUpdateCommand);
        return NoContent();
    }

    // DELETE api/<SalesChannelController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new SalesChanneLDeleteCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}