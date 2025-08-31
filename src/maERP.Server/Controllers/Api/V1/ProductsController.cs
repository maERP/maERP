using Asp.Versioning;
using maERP.Application.Features.Product.Commands.ProductCreate;
using maERP.Application.Features.Product.Commands.ProductDelete;
using maERP.Application.Features.Product.Commands.ProductUpdate;
using maERP.Application.Features.Product.Queries.ProductDetail;
using maERP.Application.Features.Product.Queries.ProductList;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/<ProductsController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedResult<ProductListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var response = await mediator.Send(new ProductListQuery(pageNumber, pageSize, searchString, orderBy));
        return StatusCode((int)response.StatusCode, response);
    }

    // GET: api/v1/<ProductsController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDetailDto>> GetDetails(int id)
    {
        var response = await mediator.Send(new ProductDetailQuery { Id = id });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST: api/v1/<ProductsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDetailDto>> Create(ProductCreateCommand productCreateCommand)
    {
        var response = await mediator.Send(productCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // PUT: api/v1/<ProductsController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, ProductUpdateCommand productUpdateCommand)
    {
        // Validate ID consistency between URL and body if ID is provided in body and differs
        if (productUpdateCommand.Id != 0 && productUpdateCommand.Id != id)
        {
            return BadRequest($"ID in URL ({id}) must match ID in request body ({productUpdateCommand.Id})");
        }

        productUpdateCommand.Id = id;
        var response = await mediator.Send(productUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // DELETE: api/v1/<ProductController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new ProductDeleteCommand { Id = id };
        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}