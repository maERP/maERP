using Asp.Versioning;
using maERP.Application.Dtos.Product;
using maERP.Application.Features.Product.Commands.CreateProductCommand;
using maERP.Application.Features.Product.Commands.DeleteProductCommand;
using maERP.Application.Features.Product.Commands.UpdateProductCommand;
using maERP.Application.Features.Product.Queries.GetProductDetailQuery;
using maERP.Application.Features.Product.Queries.GetProductsQuery;
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
    public async Task<List<ProductListDto>> Get()
    {
        var products = await mediator.Send(new GetProductsQuery());
        return products;
    }

    // GET api/<ProductsController>/5
    [HttpGet("{id}")]
    public async Task<ProductDetailDto> GetDetails(int id)
    {
        return await mediator.Send(new GetProductDetailQuery { Id = id });
    }

    // POST api/<ProductsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(CreateProductCommand createProductCommand)
    {
        var response = await mediator.Send(createProductCommand);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT: api/<ProductsController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, UpdateProductCommand updateProductCommand)
    {
        updateProductCommand.Id = id;
        await mediator.Send(updateProductCommand);
        return NoContent();
    }

    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteProductCommand { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}