using Asp.Versioning;
using maERP.Application.Features.AiModel.Queries.AiModelList;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class AiController(IMediator mediator) : ControllerBase
{
    // GET: api/<AiModelsController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<AiModelListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var aiModels = await mediator.Send(new AiModelListQuery(pageNumber, pageSize, searchString, orderBy));
        return Ok(aiModels);
    }
}
