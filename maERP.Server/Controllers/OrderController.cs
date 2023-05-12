using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Server.Models;
using maERP.Server.Repository;
using maERP.Shared.Dtos.Order;

namespace maERP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _repository;

    public OrderController(IMapper mapper, IOrderRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    // GET: api/Order
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<OrderListDto>>> GetOrder()
    {
        var order = await _repository.GetAllAsync<OrderListDto>();
        return Ok(order);
    }

    // GET: api/Order/?StartIndex=0&PageSize=25&PageNumber=1
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderListDto>>> GetPagedOrder([FromQuery] QueryParameters queryParameters)
    {
        var pagedOrderResult = await _repository.GetAllAsync<OrderListDto>(queryParameters);
        return Ok(pagedOrderResult);
    }

    // GET: api/Order/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDetailDto>> GetOrder(uint id)
    {
        var order = await _repository.GetDetails(id);
        return Ok(order);
    }

    // PUT: api/Order/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(uint id, OrderDetailDto orderDto)
    {
        try
        {
            await _repository.UpdateAsync(id, orderDto);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await OrderExists(id))
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

    // POST: api/Order
    [HttpPost]
    public async Task<ActionResult<OrderDetailDto>> PutOrder(OrderDetailDto orderDto)
    {
        var order = await _repository.AddAsync<OrderDetailDto, OrderDetailDto>(orderDto);
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    // DELETE: api/Order/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(uint id)
    {
        await _repository.DeleteAsync(id);

        return NoContent();
    }

    private async Task<bool> OrderExists(uint id)
    {
        return await _repository.Exists(id);
    }
}