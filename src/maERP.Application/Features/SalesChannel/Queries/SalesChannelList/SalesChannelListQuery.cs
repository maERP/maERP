﻿using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.SalesChannel.Queries.SalesChannelList;

public class SalesChannelListQuery : IRequest<PaginatedResult<SalesChannelListDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public string[] OrderBy { get; set; }

    public SalesChannelListQuery(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
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