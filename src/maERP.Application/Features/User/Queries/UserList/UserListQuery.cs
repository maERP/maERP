using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.User.Queries.UserList;

public class UserListQuery : IRequest<PaginatedResult<UserListDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public string[] OrderBy { get; set; }

    public UserListQuery(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
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