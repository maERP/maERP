﻿using Asp.Versioning;
using maERP.Application.Features.SalesChannel.Commands.CreateSalesChannel;
using maERP.Application.Features.SalesChannel.Commands.DeleteSalesChannel;
using maERP.Application.Features.SalesChannel.Commands.UpdateSalesChannel;
using maERP.Application.Features.SalesChannel.Queries.GetSalesChannelDetail;
using maERP.Application.Features.SalesChannel.Queries.GetSalesChannels;
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
    public async Task<List<GetSalesChannelsResponse>> Get()
    {
        var salesChannels = await mediator.Send(new GetSalesChannelsQuery());
        return salesChannels;
    }

    // GET api/<SalesChannelsController>/5
    [HttpGet("{id}")]
    public async Task<GetSalesChannelDetailResponse> GetDetails(int id)
    {
        return await mediator.Send(new GetSalesChannelDetailQuery { Id = id });
    }

    // POST api/<SalesChannelsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(CreateSalesChannelCommand createSalesChannelCommand)
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
    public async Task<ActionResult> Update(int id, UpdateSalesChannelCommand updateSalesChannelCommand)
    {
        updateSalesChannelCommand.Id = id;
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