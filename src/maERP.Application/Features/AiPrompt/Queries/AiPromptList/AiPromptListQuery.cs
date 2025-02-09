using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Queries.AiPromptList;

public class AiPromptListQuery : IRequest<PaginatedResult<AiPromptListDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public string[] OrderBy { get; set; }

    public AiPromptListQuery(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
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