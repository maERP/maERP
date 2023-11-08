using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Shared.Dtos.Customer;
using maERP.Server.Models;
using maERP.Shared.Dtos;
using maERP.Server.Contracts;

namespace maERP.Server.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _repository;

    public CustomersController(IMapper mapper, ICustomerRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    // GET: api/Customers
    [HttpGet("GetAll")]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<CustomerListDto>>> GetCustomer()
    {
        var customer = await _repository.GetAllAsync<CustomerListDto>();
        return Ok(customer);
    }

    // GET: api/Customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDetailDto>> GetCustomer(int id)
    {
        var customer = await _repository.GetByIdAsync(id);
        return Ok(customer);
    }

    // PUT: api/SalesChannels/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, [FromBody] CustomerUpdateDto customerUpdateDto)
    {
        if (await _repository.Exists(id) == true)
        {
            await _repository.UpdateAsync<CustomerUpdateDto>(id, customerUpdateDto);
        }
        else
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Customers
    [HttpPost]
    public async Task<ActionResult<CustomerDetailDto>> PostCustomer(CustomerDetailDto customerDto)
    {
        var customer = await _repository.AddAsync<CustomerDetailDto, CustomerDetailDto>(customerDto);
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