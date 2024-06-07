using Asp.Versioning;
using maERP.Application.Features.Customer.Commands.CreateCustomer;
using maERP.Application.Features.Customer.Commands.DeleteCustomer;
using maERP.Application.Features.Customer.Commands.UpdateCustomer;
using maERP.Application.Features.Customer.Queries.GetCustomerDetail;
using maERP.Application.Features.Customer.Queries.GetCustomers;
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
    public async Task<ActionResult<List<GetCustomersResponse>>> Get()
    {
        var customers = await mediator.Send(new GetCustomersQuery());
        return Ok(customers);
    }

    // GET api/<CustomersController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCustomersResponse>> GetDetails(int id)
    {
        var customer = await mediator.Send(new GetCustomerDetailQuery { Id = id });
        return Ok(customer);
    }

    // POST api/<CustomersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateCustomerResponse>> Create(CreateCustomerCommand createCustomerCommand)
    {
        var response = await mediator.Send(createCustomerCommand);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT: api/<CustomersController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<UpdateCustomerResponse>> Update(int id, UpdateCustomerCommand updateCustomerCommand)
    {
        updateCustomerCommand.Id = id;
        await mediator.Send(updateCustomerCommand);
        return NoContent();
    }

    // DELETE api/<CustomerController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteCustomerCommand { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}