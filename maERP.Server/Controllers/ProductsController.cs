using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using maERP.Server.Data;
using maERP.Server.Models.Product;
using AutoMapper;
using maERP.Server.Contracts;
using Microsoft.AspNetCore.OData.Query;
using maERP.Server.Models;

namespace maERP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepository _repository;

        public ProductsController(IMapper mapper, IProductsRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        // GET: api/Products
        [HttpGet("GetAll")]
        // GET: api/Products?$select=id,name&$filter=name eq 'Testprodukt'&$orderby=name
        [EnableQuery] 
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetProduct()
        {
            var result = await _repository.GetAllAsync<GetProductDto>();
            return Ok(result);
        }

        // GET: api/Products/?StartIndex=0&PageSize=25&PageNumber=1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetPagedProduct([FromQuery] QueryParameters queryParameters)
        {
            var pagedResult = await _repository.GetAllAsync<GetProductDto>(queryParameters);
            return Ok(pagedResult);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var result = await _repository.GetDetails(id);
            return Ok(result);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDto updateProductDto)
        {
            if (id != updateProductDto.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            try
            {
                await _repository.UpdateAsync(id, updateProductDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(CreateProductDto createProductDto)
        {
            var product = await _repository.AddAsync<CreateProductDto, GetProductDto>(createProductDto);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _repository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}