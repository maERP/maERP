using Asp.Versioning;
using maERP.Application.Features.Product.Commands.ProductCreate;
using maERP.Application.Features.Product.Commands.ProductDelete;
using maERP.Application.Features.Product.Commands.ProductUpdate;
using maERP.Application.Features.Product.Queries.ProductDetail;
using maERP.Application.Features.Product.Queries.ProductList;
using maERP.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    // GET: api/<ProductsController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<ProductListResponse>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var products = await mediator.Send(new ProductListQuery(pageNumber, pageSize, searchString, orderBy));
        return Ok(products);
    }

    // GET api/<ProductsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDetailResponse>> GetDetails(int id)
    {
        var product = await mediator.Send(new ProductDetailQuery { Id = id });
        return Ok(product);
    }

    // POST api/<ProductsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(ProductCreateCommand productCreateCommand)
    {
        var response = await mediator.Send(productCreateCommand);
        return CreatedAtAction(nameof(GetAll), new { id = response });
    }

    // PUT: api/<ProductsController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, ProductUpdateCommand productUpdateCommand)
    {
        productUpdateCommand.Id = id;
        await mediator.Send(productUpdateCommand);
        return NoContent();
    }

    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new ProductDeleteCommand { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}