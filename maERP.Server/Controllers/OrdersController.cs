using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using maERP.Shared.Models;
using maERP.Shared.Dtos.Order;
using AutoMapper;
using maERP.Server.Contracts;
using Microsoft.AspNetCore.OData.Query;
using maERP.Server.Models;

namespace maERP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrdersRepository _repository;

        public OrdersController(IMapper mapper, IOrdersRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        // GET: api/Orders
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<OrderListDto>>> GetOrders()
        {
            var orders = await _repository.GetAllAsync<OrderListDto>();
            return Ok(orders);
        }

        // GET: api/Orders/?StartIndex=0&PageSize=25&PageNumber=1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderListDto>>> GetPagedOrders([FromQuery] QueryParameters queryParameters)
        {
            var pagedOrdersResult = await _repository.GetAllAsync<OrderListDto>(queryParameters);
            return Ok(pagedOrdersResult);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _repository.GetDetails(id);
            return Ok(order);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrders(int id, OrderDto orderDto)
        {
            if (id != orderDto.Id)
            {
                return BadRequest("Invalid Record Id");
            }

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

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PutOrder(OrderDto orderDto)
        {
            var order = await _repository.AddAsync<OrderDto, OrderDto>(orderDto);
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
    }
}