using Asp.Versioning;
using maERP.Application.Features.Manufacturer.Commands.ManufacturerCreate;
using maERP.Application.Features.Manufacturer.Commands.ManufacturerDelete;
using maERP.Application.Features.Manufacturer.Commands.ManufacturerUpdate;
using maERP.Application.Features.Manufacturer.Queries.ManufacturerDetail;
using maERP.Application.Features.Manufacturer.Queries.ManufacturerList;
using maERP.Domain.Dtos.Manufacturer;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class ManufacturersController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/<ManufacturersController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<ManufacturerListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var response = await mediator.Send(new ManufacturerListQuery(pageNumber, pageSize, searchString, orderBy));
        return StatusCode((int)response.StatusCode, response);
    }

    // GET: api/v1/<ManufacturersController>/5
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ManufacturerDetailDto>> GetDetails(Guid id)
    {
        var response = await mediator.Send(new ManufacturerDetailQuery { Id = id });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST: api/v1/<ManufacturersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(ManufacturerCreateCommand manufacturerCreateCommand)
    {
        var response = await mediator.Send(manufacturerCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // PUT: api/v1/<ManufacturersController>/5
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ManufacturerDetailDto>> Update(Guid id, ManufacturerUpdateCommand manufacturerUpdateCommand)
    {
        manufacturerUpdateCommand.Id = id;
        var response = await mediator.Send(manufacturerUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // DELETE: api/v1/<ManufacturersController>/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new ManufacturerDeleteCommand { Id = id };
        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}