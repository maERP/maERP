using maERP.Client.Core.Constants;
using maERP.Client.Core.Models;
using maERP.Client.Features.Orders.Services;
using maERP.Domain.Dtos.Order;

namespace maERP.Client.Features.Orders.Models;

/// <summary>
/// Model for order list page using MVUX pattern.
/// Supports searching, sorting, and pagination.
/// </summary>
public partial record OrderListModel
{
    private readonly IOrderService _orderService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;

    public OrderListModel(
        IOrderService orderService,
        INavigator navigator,
        IStringLocalizer localizer)
    {
        _orderService = orderService;
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
    /// Current sort order (e.g., "DateOrdered Descending").
    /// </summary>
    public IState<string> SortOrder => State<string>.Value(this, () => "DateOrdered Descending");

    /// <summary>
    /// Pagination information from the last API response.
    /// </summary>
    public IState<OrderPaginationInfo> Pagination => State<OrderPaginationInfo>.Value(this, () => new OrderPaginationInfo());

    /// <summary>
    /// Feed of orders from the API.
    /// Automatically refreshes when SearchQuery, CurrentPage, or SortOrder changes.
    /// </summary>
    public IListFeed<OrderListDto> Orders => Feed
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

            var response = await _orderService.GetOrdersAsync(parameters, ct);

            // Update pagination info
            await Pagination.UpdateAsync(_ => new OrderPaginationInfo(
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
    /// Navigate to order detail page.
    /// </summary>
    public async Task ViewOrder(OrderListDto order)
    {
        await _navigator.NavigateDataAsync(this, new OrderDetailData(order.Id));
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
/// Holds pagination state information for orders.
/// </summary>
public record OrderPaginationInfo
{
    private readonly IStringLocalizer? _localizer;

    public OrderPaginationInfo()
    {
    }

    public OrderPaginationInfo(
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
    /// Display text for total count info (e.g., "25 orders").
    /// </summary>
    public string CountInfo
    {
        get
        {
            if (TotalCount == 1)
            {
                return _localizer?["Pagination.OrdersSingular"] ?? "1 order";
            }

            var format = _localizer?["Pagination.OrdersPlural"] ?? "{0} orders";
            return string.Format(format, TotalCount);
        }
    }
}
