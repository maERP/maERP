using Asp.Versioning;
using maERP.Application.Features.Customer.Commands.CustomerCreate;
using maERP.Application.Features.Customer.Commands.CustomerDelete;
using maERP.Application.Features.Customer.Commands.CustomerUpdate;
using maERP.Application.Features.Customer.Queries.CustomerDetail;
using maERP.Application.Features.Customer.Queries.CustomerList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class CustomersController(IMediator mediator) : ControllerBase
{
    // GET: api/<CustomersController>
    [HttpGet]
    public async Task<ActionResult<List<CustomerListResponse>>> Get()
    {
        var customers = await mediator.Send(new CustomerListQuery());
        return Ok(customers);
    }

    // GET api/<CustomersController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDetailResponse>> GetDetails(int id)
    {
        var customer = await mediator.Send(new CustomerDetailQuery { Id = id });
        return Ok(customer);
    }

    // POST api/<CustomersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerCreateResponse>> Create(CustomerCreateCommand customerCreateCommand)
    {
        var response = await mediator.Send(customerCreateCommand);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT: api/<CustomersController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CustomerUpdateResponse>> Update(int id, CustomerUpdateCommand customerUpdateCommand)
    {
        customerUpdateCommand.Id = id;
        await mediator.Send(customerUpdateCommand);
        return NoContent();
    }

    // DELETE api/<CustomerController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new CustomerDeleteCommand { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}