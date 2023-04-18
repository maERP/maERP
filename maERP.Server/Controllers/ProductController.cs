using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Server.Contracts;
using maERP.Shared.Dtos.Product;
using maERP.Server.Models;
using maERP.Server.Repository;
using maERP.Shared.Models;

namespace maERP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly IProductSalesChannelRepository _productSalesChannelRepository;
    private readonly ISalesChannelRepository _salesChannelRepository;
    private readonly ITaxClassRepository _taxClassRepository;

    public ProductController(
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

    // GET: api/Product
    [HttpGet("GetAll")]
    // GET: api/Product?$select=id,name&$filter=name eq 'Testprodukt'&$orderby=name
    [EnableQuery] 
    public async Task<ActionResult<IEnumerable<ProductListDto>>> GetProduct()
    {
        var result = await _productRepository.GetAllAsync<ProductListDto>();
        return Ok(result);
    }

    // GET: api/Product/?StartIndex=0&PageSize=25&PageNumber=1
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductListDto>>> GetPagedProduct([FromQuery] QueryParameters queryParameters)
    {
        var pagedResult = await _productRepository.GetAllAsync<ProductListDto>(queryParameters);
        return Ok(pagedResult);
    }

    // GET: api/Product/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDetailDto>> GetProduct(int id)
    {
        var result = await _productRepository.GetDetails(id);
        return Ok(result);
    }

    // POST: api/Product
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

        var taxClass = await _taxClassRepository.GetAsync(productCreateDto.TaxClass.Id);

        var product = await _productRepository.AddAsync<ProductCreateDto, Product>(productCreateDto);

        if (productCreateDto.ProductSalesChannel?.Count > 0)
        {
            product.ProductSalesChannel = new List<ProductSalesChannel>();

            foreach (var productSalesChannel in product.ProductSalesChannel)
            {
                var salesChannel = await _salesChannelRepository.GetAsync(productSalesChannel.Id);

                await _productSalesChannelRepository.AddAsync(new ProductSalesChannel
                {
                    Product = product,
                    SalesChannel = salesChannel,
                    Price = productSalesChannel.Price,
                    RemoteProductId = productSalesChannel.RemoteProductId,
                    ProductImport = productSalesChannel.ProductImport,
                    ProductExport = productSalesChannel.ProductExport
                });
            }
        }

        await _productRepository.UpdateAsync(product);

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // PUT: api/Product/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, ProductUpdateDto productUpdateDto)
    {
        if (id != productUpdateDto.Id)
        {
            return BadRequest("Invalid Record Id");
        }

        try
        {
            await _productRepository.UpdateAsync(id, productUpdateDto);
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

    // DELETE: api/Product/5
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