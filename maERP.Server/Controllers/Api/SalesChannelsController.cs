using maERP.Application.Dtos.SalesChannel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using maERP.Application.Features.SalesChannel.Commands.CreateSalesChannelCommand;
using maERP.Application.Features.SalesChannel.Commands.DeleteSalesChannelCommand;
using maERP.Application.Features.SalesChannel.Commands.UpdateSalesChannelCommand;
using maERP.Application.Features.SalesChannel.Queries.GetAllSalesChannelsQuery;
using maERP.Application.Features.SalesChannel.Queries.GetSalesChannelDetailQuery;

namespace maERP.Server.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class SalesChannelsController(IMediator mediator) : ControllerBase
{
    // GET: api/<SalesChannelsController>
    [HttpGet]
    public async Task<List<SalesChannelListDto>> Get()
    {
        var salesChannels = await mediator.Send(new GetSalesChannelsQuery());
        return salesChannels;
    }

    // GET api/<SalesChannelsController>/5
    [HttpGet("{id}")]
    public async Task<SalesChannelDetailDto> GetDetails(int id)
    {
        return await mediator.Send(new GetSalesChannelDetailQuery() { Id = id });
    }

    // POST api/<SalesChannelsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateSalesChannelCommand createSalesChannelCommand)
    {
        var response = await mediator.Send(createSalesChannelCommand);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT: api/<SalesChannelsController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateSalesChannelCommand updateSalesChannelCommand)
    {
        await mediator.Send(updateSalesChannelCommand);
        return NoContent();
    }

    // DELETE api/<SalesChannelController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteSalesChannelCommand { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}