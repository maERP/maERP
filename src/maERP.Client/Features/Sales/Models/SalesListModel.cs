using maERP.Client.Core.Constants;
using maERP.Client.Core.Models;
using maERP.Client.Features.Saless.Services;
using maERP.Domain.Dtos.Sales;

namespace maERP.Client.Features.Saless.Models;

/// <summary>
/// Model for sales list page using MVUX pattern.
/// Supports searching, sorting, and pagination.
/// </summary>
public partial record SalesListModel
{
    private readonly ISalesService _salesService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;

    public SalesListModel(
        ISalesService salesService,
        INavigator navigator,
        IStringLocalizer localizer)
    {
        _salesService = salesService;
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
    /// Current sort sales (e.g., "DateSalesed Descending").
    /// </summary>
    public IState<string> SortSales => State<string>.Value(this, () => "DateSalesed Descending");

    /// <summary>
    /// Pagination information from the last API response.
    /// </summary>
    public IState<SalesPaginationInfo> Pagination => State<SalesPaginationInfo>.Value(this, () => new SalesPaginationInfo());

    /// <summary>
    /// Feed of saless from the API.
    /// Automatically refreshes when SearchQuery, CurrentPage, or SortSales changes.
    /// </summary>
    public IListFeed<SalesListDto> Saless => Feed
        .Combine(SearchQuery, CurrentPage, PageSize, SortSales)
        .SelectAsync(async (combined, ct) =>
        {
            var (query, page, size, salesBy) = combined;

            var parameters = new QueryParameters
            {
                PageNumber = page,
                PageSize = size,
                SearchString = string.IsNullOrWhiteSpace(query) ? null : query,
                SalesBy = salesBy
            };

            var response = await _salesService.GetSalessAsync(parameters, ct);

            // Update pagination info
            await Pagination.UpdateAsync(_ => new SalesPaginationInfo(
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
    /// Navigate to sales detail page.
    /// </summary>
    public async Task ViewSales(SalesListDto sales)
    {
        await _navigator.NavigateDataAsync(this, new SalesDetailData(sales.Id));
    }

    /// <summary>
    /// Navigate to create new sales page.
    /// </summary>
    public async Task CreateSales()
    {
        await _navigator.NavigateRouteAsync(this, Routes.SalesCreate);
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
    /// Change the sort sales.
    /// </summary>
    public async ValueTask SetSortSales(string salesBy, CancellationToken ct = default)
    {
        await SortSales.UpdateAsync(_ => salesBy, ct);
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
/// Holds pagination state information for saless.
/// </summary>
public record SalesPaginationInfo
{
    private readonly IStringLocalizer? _localizer;

    public SalesPaginationInfo()
    {
    }

    public SalesPaginationInfo(
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
    /// Display text for total count info (e.g., "25 saless").
    /// </summary>
    public string CountInfo
    {
        get
        {
            if (TotalCount == 1)
            {
                return _localizer?["Pagination.SalessSingular"] ?? "1 sales";
            }

            var format = _localizer?["Pagination.SalessPlural"] ?? "{0} saless";
            return string.Format(format, TotalCount);
        }
    }
}
