using maERP.Client.Core.Constants;
using maERP.Client.Core.Models;
using maERP.Client.Features.SalesChannels.Services;
using maERP.Domain.Dtos.SalesChannel;

namespace maERP.Client.Features.SalesChannels.Models;

/// <summary>
/// Model for sales channel list page using MVUX pattern.
/// Supports searching, sorting, and pagination.
/// </summary>
public partial record SalesChannelListModel
{
    private readonly ISalesChannelService _salesChannelService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;

    public SalesChannelListModel(
        ISalesChannelService salesChannelService,
        INavigator navigator,
        IStringLocalizer localizer)
    {
        _salesChannelService = salesChannelService;
        _navigator = navigator;
        _localizer = localizer;
    }

    /// <summary>
    /// The search query entered by the user.
    /// </summary>
    public IState<string> SearchQuery => State<string>.Value(this, () => string.Empty);

    /// <summary>
    /// Current page number (0-based).
    /// </summary>
    public IState<int> CurrentPage => State<int>.Value(this, () => 0);

    /// <summary>
    /// Number of items per page.
    /// </summary>
    public IState<int> PageSize => State<int>.Value(this, () => 25);

    /// <summary>
    /// Current sort order (e.g., "Name Ascending").
    /// </summary>
    public IState<string> SortOrder => State<string>.Value(this, () => "DateCreated Descending");

    /// <summary>
    /// Pagination information from the last API response.
    /// </summary>
    public IState<SalesChannelPaginationInfo> Pagination => State<SalesChannelPaginationInfo>.Value(this, () => new SalesChannelPaginationInfo());

    /// <summary>
    /// Feed of sales channels from the API.
    /// Automatically refreshes when SearchQuery, CurrentPage, or SortOrder changes.
    /// </summary>
    public IListFeed<SalesChannelListDto> SalesChannels => Feed
        .Combine(SearchQuery, CurrentPage, PageSize, SortOrder)
        .SelectAsync(async (combined, ct) =>
        {
            var (query, page, size, orderBy) = combined;

            var parameters = new QueryParameters
            {
                PageNumber = page,
                PageSize = size,
                SearchString = string.IsNullOrWhiteSpace(query) ? null : query,
                OrderBy = orderBy
            };

            var response = await _salesChannelService.GetSalesChannelsAsync(parameters, ct);

            // Update pagination info
            await Pagination.UpdateAsync(_ => new SalesChannelPaginationInfo(
                response.CurrentPage,
                response.TotalPages,
                response.TotalCount,
                response.PageSize,
                response.HasPreviousPage,
                response.HasNextPage,
                _localizer), ct);

            return response.Data.ToImmutableList();
        })
        .AsListFeed();

    /// <summary>
    /// Navigate to sales channel detail page.
    /// </summary>
    public async Task ViewSalesChannel(SalesChannelListDto salesChannel)
    {
        await _navigator.NavigateDataAsync(this, new SalesChannelDetailData(salesChannel.Id));
    }

    /// <summary>
    /// Navigate to create new sales channel page.
    /// </summary>
    public async Task CreateSalesChannel()
    {
        await _navigator.NavigateDataAsync(this, new SalesChannelEditData());
    }

    /// <summary>
    /// Go to the next page.
    /// </summary>
    public async ValueTask GoToNextPage(CancellationToken ct = default)
    {
        var pagination = await Pagination.Value(ct);
        if (pagination?.HasNextPage == true)
        {
            await CurrentPage.UpdateAsync(p => p + 1, ct);
        }
    }

    /// <summary>
    /// Go to the previous page.
    /// </summary>
    public async ValueTask GoToPreviousPage(CancellationToken ct = default)
    {
        var pagination = await Pagination.Value(ct);
        if (pagination?.HasPreviousPage == true)
        {
            await CurrentPage.UpdateAsync(p => Math.Max(0, p - 1), ct);
        }
    }

    /// <summary>
    /// Go to a specific page.
    /// </summary>
    public async ValueTask GoToPage(int page, CancellationToken ct = default)
    {
        var pagination = await Pagination.Value(ct);
        if (pagination != null && page >= 0 && page < pagination.TotalPages)
        {
            await CurrentPage.UpdateAsync(_ => page, ct);
        }
    }

    /// <summary>
    /// Change the sort order.
    /// </summary>
    public async ValueTask SetSortOrder(string orderBy, CancellationToken ct = default)
    {
        await SortOrder.UpdateAsync(_ => orderBy, ct);
        await CurrentPage.UpdateAsync(_ => 0, ct); // Reset to first page when sorting changes
    }

    /// <summary>
    /// Change the page size.
    /// </summary>
    public async ValueTask SetPageSize(int pageSize, CancellationToken ct = default)
    {
        await PageSize.UpdateAsync(_ => pageSize, ct);
        await CurrentPage.UpdateAsync(_ => 0, ct); // Reset to first page when page size changes
    }
}

/// <summary>
/// Holds pagination state information for sales channels.
/// </summary>
public record SalesChannelPaginationInfo
{
    private readonly IStringLocalizer? _localizer;

    public SalesChannelPaginationInfo()
    {
    }

    public SalesChannelPaginationInfo(
        int currentPage,
        int totalPages,
        int totalCount,
        int pageSize,
        bool hasPreviousPage,
        bool hasNextPage,
        IStringLocalizer localizer)
    {
        CurrentPage = currentPage;
        TotalPages = totalPages;
        TotalCount = totalCount;
        PageSize = pageSize;
        HasPreviousPage = hasPreviousPage;
        HasNextPage = hasNextPage;
        _localizer = localizer;
    }

    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
    public int TotalCount { get; init; }
    public int PageSize { get; init; }
    public bool HasPreviousPage { get; init; }
    public bool HasNextPage { get; init; }

    /// <summary>
    /// Display text for current page info (e.g., "Page 1 of 5").
    /// </summary>
    public string PageInfo
    {
        get
        {
            if (TotalPages <= 0)
            {
                return _localizer?["Pagination.NoResults"] ?? "No results";
            }

            var format = _localizer?["Pagination.PageInfo"] ?? "Page {0} of {1}";
            return string.Format(format, CurrentPage + 1, TotalPages);
        }
    }

    /// <summary>
    /// Display text for total count info (e.g., "25 sales channels").
    /// </summary>
    public string CountInfo
    {
        get
        {
            if (TotalCount == 1)
            {
                return _localizer?["Pagination.SalesChannelsSingular"] ?? "1 sales channel";
            }

            var format = _localizer?["Pagination.SalesChannelsPlural"] ?? "{0} sales channels";
            return string.Format(format, TotalCount);
        }
    }
}

