using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiModel.Queries.AiModelList;

public class AiModelListQuery : IRequest<PaginatedResult<AiModelListDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public string[] OrderBy { get; set; }

    public AiModelListQuery(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchString = searchString;

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            OrderBy = orderBy.Split(',');
        }
        else OrderBy = new string[] { };
    }
}