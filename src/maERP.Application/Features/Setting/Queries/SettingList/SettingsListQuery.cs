using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Setting.Queries.SettingList;

public class SettingListQuery : IRequest<PaginatedResult<SettingListDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public string[] OrderBy { get; set; }

    public SettingListQuery(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
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