using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Application.Dtos.Order;

namespace maERP.Server.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{
    /*
    private readonly IMapper _mapper;
    private readonly IOrderRepository _repository;

    public OrdersController(IMapper mapper, IOrderRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    // GET: api/Orders
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<OrderListDto>>> GetOrder()
    {
        var order = await _repository.GetAllAsync<OrderListDto>();
        return Ok(order);
    }

    // GET: api/Orders/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDetailDto>> GetOrder(int id)
    {
        var order = await _repository.GetByIdAsync(id);
        return Ok(order);
    }

    // PUT: api/Orders/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(int id, [FromBody] OrderUpdateDto orderUpdateDto)
    {
        if (await _repository.Exists(id) == true)
        {
            await _repository.UpdateAsync<OrderUpdateDto>(id, orderUpdateDto);
        }
        else
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Orders
    [HttpPost]
    public async Task<ActionResult<OrderDetailDto>> PutOrder(OrderDetailDto orderDto)
    {
        var order = await _repository.AddAsync<OrderDetailDto, OrderDetailDto>(orderDto);
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    // DELETE: api/Orders/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        await _repository.DeleteAsync(id);

        return NoContent();
    }

    private async Task<bool> OrderExists(int id)
    {
        return await _repository.Exists(id);
    }
    */
}