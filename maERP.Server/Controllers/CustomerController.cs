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
public class CustomerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _repository;

    public CustomerController(IMapper mapper, ICustomerRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    // GET: api/Customer
    [HttpGet("GetAll")]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<CustomerListDto>>> GetCustomer()
    {
        var customer = await _repository.GetAllAsync<CustomerListDto>();
        return Ok(customer);
    }

    // GET: api/Customer/?StartIndex=0&PageSize=25&PageNumber=1
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerListDto>>> GetPagedCustomer([FromQuery] QueryParameters queryParameters)
    {
        var pagedCustomerResult = await _repository.GetAllAsync<CustomerListDto>(queryParameters);
        return Ok(pagedCustomerResult);
    }

    // GET: api/Customer/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDetailDto>> GetCustomer(int id)
    {
        var customer = await _repository.GetDetails(id);
        return Ok(customer);
    }

    // PUT: api/Customer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, CustomerDetailDto customerDto)
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

    // POST: api/Customer
    [HttpPost]
    public async Task<ActionResult<CustomerDetailDto>> PostCustomer(CustomerDetailDto customerDto)
    {
        var customer = await _repository.AddAsync<CustomerDetailDto, CustomerDetailDto>(customerDto);
        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
    }

    // DELETE: api/Customer/5
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