using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Shared.Dtos.Product;
using maERP.Server.Models;
using maERP.Server.Repository;
using maERP.Shared.Models;
using maERP.Shared.Dtos;

namespace maERP.Server.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly IProductSalesChannelRepository _productSalesChannelRepository;
    private readonly ISalesChannelRepository _salesChannelRepository;
    private readonly ITaxClassRepository _taxClassRepository;

    public ProductsController(
        IMapper mapper,
        IProductRepository productRepository,
        IProductSalesChannelRepository productSalesChannelRepository,
        ISalesChannelRepository salesChannelRepository,
        ITaxClassRepository taxClassRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _productSalesChannelRepository = productSalesChannelRepository;
        _salesChannelRepository = salesChannelRepository;
        _taxClassRepository = taxClassRepository;
    }

    // GET: api/Products
    [HttpGet("GetAll")]
    // GET: api/Products?$select=id,name&$filter=name eq 'Testprodukt'&$orderby=name
    [EnableQuery] 
    public async Task<ActionResult<IEnumerable<ProductListDto>>> GetProduct()
    {
        var result = await _productRepository.GetAllAsync<ProductListDto>();
        return Ok(result);
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDetailDto>> GetProduct(int id)
    {
        var result = await _productRepository.GetByIdAsync(id);

        if(result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult<ProductDetailDto>> PostProduct(ProductCreateDto productCreateDto)
    {
        if(productCreateDto.ProductSalesChannel != null)
        {
            foreach(var salesChannel in productCreateDto.ProductSalesChannel)
            {
                if(!await _productSalesChannelRepository.Exists(salesChannel.Id))
                {
                    return BadRequest("SalesChannel does not exist");
                }
            }
        }

        if (!await _taxClassRepository.Exists(productCreateDto.TaxClass.Id))
        {
            return BadRequest("TaxClass does not exist");
        }

        var product = new Product();

        _mapper.Map(productCreateDto, product);

        var taxClass = await _taxClassRepository.GetByIdAsync(productCreateDto.TaxClass.Id);

        product.TaxClass = taxClass;

        //product = await _productRepository.AddAsync<ProductCreateDto, Product>(productCreateDto);
        product = await _productRepository.AddAsync(product);

        if (productCreateDto.ProductSalesChannel?.Count > 0)
        {
            product.ProductSalesChannel = new List<ProductSalesChannel>();

            foreach (var productSalesChannel in product.ProductSalesChannel)
            {
                var salesChannel = await _salesChannelRepository.GetByIdAsync(productSalesChannel.Id);

                await _productSalesChannelRepository.AddAsync(new ProductSalesChannel
                {
                    Product = product,
                    SalesChannel = salesChannel,
                    Price = productSalesChannel.Price,
                    RemoteProductId = productSalesChannel.RemoteProductId,
                    // ProductImport = productSalesChannel.ProductImport,
                    // ProductExport = productSalesChannel.ProductExport
                });
            }
        }

        await _productRepository.UpdateAsync(product);

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // PUT: api/Products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, [FromBody] ProductUpdateDto productUpdateDto)
    {
        if (await _productRepository.Exists(id) == true)
        {
            await _productRepository.UpdateAsync<ProductUpdateDto>(id, productUpdateDto);
        }
        else
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productRepository.DeleteAsync(id);

        return NoContent();
    }

    private async Task<bool> ProductExists(int id)
    {
        return await _productRepository.Exists(id);
    }
}