using maERP.Domain.Dtos.Country;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Country.Queries.CountryList;

public class CountryListQuery : IRequest<PaginatedResult<CountryListDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public string[] OrderBy { get; set; }

    public CountryListQuery(int pageNumber = 0, int pageSize = 300, string searchString = "", string orderBy = "")
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchString = searchString;

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            OrderBy = orderBy.Split(',');
        }
        else OrderBy = Array.Empty<string>();
    }
}
