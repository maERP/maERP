using Asp.Versioning;
using maERP.Application.Features.GoodsReceipt.Commands.GoodsReceiptCreate;
using maERP.Application.Features.GoodsReceipt.Queries.GoodsReceiptDetail;
using maERP.Application.Features.GoodsReceipt.Queries.GoodsReceiptList;
using maERP.Domain.Dtos.GoodsReceipt;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class GoodsReceiptsController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/goodsreceipts
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<GoodsReceiptListDto>>> GetAll(
        int pageNumber = 0,
        int pageSize = 50,
        string searchTerm = "",
        string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "ReceiptDate Descending";
        }

        var response = await mediator.Send(new GoodsReceiptListQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchTerm = searchTerm,
            OrderBy = orderBy
        });

        return StatusCode((int)response.StatusCode, response);
    }

    // GET: api/v1/goodsreceipts/5
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GoodsReceiptDetailDto>> GetDetails(Guid id)
    {
        var response = await mediator.Send(new GoodsReceiptDetailQuery { Id = id });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST: api/v1/goodsreceipts
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(GoodsReceiptCreateCommand goodsReceiptCreateCommand)
    {
        var response = await mediator.Send(goodsReceiptCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }
}