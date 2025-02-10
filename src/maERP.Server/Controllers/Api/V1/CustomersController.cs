using Asp.Versioning;
using maERP.Application.Exceptions;
using maERP.Application.Features.Customer.Commands.CustomerCreate;
using maERP.Application.Features.Customer.Commands.CustomerDelete;
using maERP.Application.Features.Customer.Commands.CustomerUpdate;
using maERP.Application.Features.Customer.Queries.CustomerDetail;
using maERP.Application.Features.Customer.Queries.CustomerList;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;
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
    public async Task<ActionResult<PaginatedResult<CustomerListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateEnrollment Descending";
        }

        var customers = await mediator.Send(new CustomerListQuery(pageNumber, pageSize, searchString, orderBy));
        return Ok(customers);
    }

    // GET api/<CustomersController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDetailDto>> GetDetails(int id)
    {
        try 
        {
            var customer = await mediator.Send(new CustomerDetailQuery { Id = id });
            return Ok(customer);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    // POST api/<CustomersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerDetailDto>> Create(CustomerCreateCommand customerCreateCommand)
    {
        var response = await mediator.Send(customerCreateCommand);
        return CreatedAtAction(nameof(GetAll), new { id = response });
    }

    // PUT: api/<CustomersController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CustomerDetailDto>> Update(int id, CustomerUpdateCommand customerUpdateCommand)
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