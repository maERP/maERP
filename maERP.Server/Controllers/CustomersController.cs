using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Shared.Dtos.Customer;
using maERP.Server.Contracts;

using maERP.Server.Models;

namespace maERP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICustomersRepository _repository;

    public CustomersController(IMapper mapper, ICustomersRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    // GET: api/Customers
    [HttpGet("GetAll")]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<CustomerListDto>>> GetCustomers()
    {
        var customers = await _repository.GetAllAsync<CustomerListDto>();
        return Ok(customers);
    }

    // GET: api/Customers/?StartIndex=0&PageSize=25&PageNumber=1
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerListDto>>> GetPagedCustomers([FromQuery] QueryParameters queryParameters)
    {
        var pagedCustomersResult = await _repository.GetAllAsync<CustomerListDto>(queryParameters);
        return Ok(pagedCustomersResult);
    }

    // GET: api/Customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
    {
        var customer = await _repository.GetDetails(id);
        return Ok(customer);
    }

    // PUT: api/Customers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, CustomerDto customerDto)
    {
        if (id != customerDto.Id)
        {
            return BadRequest("Invalid Record Id");
        }

        try
        {
            await _repository.UpdateAsync(id, customerDto);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await CustomerExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Customers
    [HttpPost]
    public async Task<ActionResult<CustomerDto>> PostCustomer(CustomerDto customerDto)
    {
        var customer = await _repository.AddAsync<CustomerDto, CustomerDto>(customerDto);
        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
    }

    // DELETE: api/Customers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        await _repository.DeleteAsync(id);

        return NoContent();
    }

    private async Task<bool> CustomerExists(int id)
    {
        return await _repository.Exists(id);
    }
}