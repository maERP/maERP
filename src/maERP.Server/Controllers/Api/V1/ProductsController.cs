using Asp.Versioning;
using maERP.Application.Features.Product.Commands.ProductCreate;
using maERP.Application.Features.Product.Commands.ProductDelete;
using maERP.Application.Features.Product.Commands.ProductUpdate;
using maERP.Application.Features.Product.Queries.ProductDetail;
using maERP.Application.Features.Product.Queries.ProductList;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using maERP.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Retrieves a paginated list of products
    /// </summary>
    /// <param name="pageNumber">Page number (0-based)</param>
    /// <param name="pageSize">Number of items per page (max 100)</param>
    /// <param name="searchString">Search term to filter products by name or SKU</param>
    /// <param name="orderBy">Sort order (e.g., "Name Ascending", "DateCreated Descending")</param>
    /// <returns>Paginated list of products</returns>
    /// <response code="200">Returns the paginated list of products</response>
    /// <response code="400">Invalid pagination parameters or search criteria</response>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<ProductListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    public async Task<ActionResult<PaginatedResult<ProductListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var response = await mediator.Send(new ProductListQuery(pageNumber, pageSize, searchString, orderBy));
        return response.ToActionResult();
    }

    /// <summary>
    /// Retrieves detailed information about a specific product
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <returns>Detailed product information</returns>
    /// <response code="200">Returns the product details</response>
    /// <response code="404">Product not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<ProductDetailDto>> GetDetails(int id)
    {
        var response = await mediator.Send(new ProductDetailQuery { Id = id });
        return response.ToActionResult();
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="productCreateCommand">Product information for creation</param>
    /// <returns>The ID of the newly created product</returns>
    /// <response code="201">Product created successfully</response>
    /// <response code="400">Invalid product data or validation errors</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    public async Task<ActionResult<Result<int>>> Create(ProductCreateCommand productCreateCommand)
    {
        var response = await mediator.Send(productCreateCommand);
        return response.ToActionResult();
    }

    /// <summary>
    /// Updates an existing product
    /// </summary>
    /// <param name="id">The unique identifier of the product to update</param>
    /// <param name="productUpdateCommand">Updated product information</param>
    /// <returns>Success confirmation</returns>
    /// <response code="200">Product updated successfully</response>
    /// <response code="400">Invalid product data, validation errors, or ID mismatch</response>
    /// <response code="404">Product not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<Result<int>>> Update(int id, ProductUpdateCommand productUpdateCommand)
    {
        // Validate ID is not zero or negative
        if (id <= 0)
        {
            var invalidIdResponse = ProblemDetailsResult.BadRequest(
                "Invalid Request", 
                $"Product ID must be greater than zero",
                "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                $"/api/v1/products/{id}"
            );
            return invalidIdResponse.ToActionResult();
        }

        // Validate ID consistency between URL and body if ID is provided in body and differs
        if (productUpdateCommand.Id != 0 && productUpdateCommand.Id != id)
        {
            var errorResponse = ProblemDetailsResult.BadRequest(
                "Invalid Request", 
                $"ID in URL ({id}) must match ID in request body ({productUpdateCommand.Id})",
                "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                $"/api/v1/products/{id}"
            );
            return errorResponse.ToActionResult();
        }

        productUpdateCommand.Id = id;
        var response = await mediator.Send(productUpdateCommand);
        return response.ToActionResult();
    }

    /// <summary>
    /// Deletes a product
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <returns>Success confirmation</returns>
    /// <response code="204">Product deleted successfully</response>
    /// <response code="400">Invalid product ID</response>
    /// <response code="404">Product not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<Result<int>>> Delete(int id)
    {
        var command = new ProductDeleteCommand { Id = id };
        var response = await mediator.Send(command);
        return response.ToActionResult();
    }
}