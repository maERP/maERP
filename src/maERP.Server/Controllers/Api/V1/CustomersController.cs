using Asp.Versioning;
using maERP.Application.Features.Customer.Commands.CustomerCreate;
using maERP.Application.Features.Customer.Commands.CustomerDelete;
using maERP.Application.Features.Customer.Commands.CustomerUpdate;
using maERP.Application.Features.Customer.Queries.CustomerDetail;
using maERP.Application.Features.Customer.Queries.CustomerList;
using maERP.Domain.Dtos.Customer;
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
public class CustomersController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/<CustomersController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedResult<CustomerListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateEnrollment Descending";
        }

        var response = await mediator.Send(new CustomerListQuery(pageNumber, pageSize, searchString, orderBy));
        return response.ToActionResult();
    }

    // GET: api/v1/<CustomersController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDetailDto>> GetDetails(int id)
    {
        var response = await mediator.Send(new CustomerDetailQuery { Id = id });
        return response.ToActionResult();
    }

    // POST: api/v1/<CustomersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerDetailDto>> Create(CustomerCreateCommand customerCreateCommand)
    {
        var response = await mediator.Send(customerCreateCommand);
        return response.ToActionResult();
    }

    // PUT: api/v1/<CustomersController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, CustomerUpdateCommand customerUpdateCommand)
    {
        // Validate ID consistency between URL and body if ID is provided in body and differs
        if (customerUpdateCommand.Id != 0 && customerUpdateCommand.Id != id)
        {
            var errorResponse = ProblemDetailsResult.BadRequest(
                "Invalid Request", 
                $"ID in URL ({id}) must match ID in request body ({customerUpdateCommand.Id})",
                "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                $"/api/v1/customers/{id}"
            );
            return errorResponse.ToActionResult();
        }

        customerUpdateCommand.Id = id;
        var response = await mediator.Send(customerUpdateCommand);
        return response.ToActionResult();
    }

    // DELETE: api/v1/<CustomerController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new CustomerDeleteCommand { Id = id };
        var response = await mediator.Send(command);
        return response.ToActionResult();
    }
}