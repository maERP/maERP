using maERP.Client.Core.Constants;
using maERP.Client.Core.Models;
using maERP.Client.Features.Customers.Services;
using maERP.Domain.Dtos.Customer;

namespace maERP.Client.Features.Customers.Models;

/// <summary>
/// Model for customer list page using MVUX pattern.
/// Supports searching, sorting, and pagination.
/// </summary>
public partial record CustomerListModel
{
    private readonly ICustomerService _customerService;
    private readonly INavigator _navigator;

    public CustomerListModel(
        ICustomerService customerService,
        INavigator navigator)
    {
        _customerService = customerService;
        _navigator = navigator;
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
    /// Current sort order (e.g., "LastName Ascending").
    /// </summary>
    public IState<string> SortOrder => State<string>.Value(this, () => "DateEnrollment Descending");

    /// <summary>
    /// Pagination information from the last API response.
    /// </summary>
    public IState<PaginationInfo> Pagination => State<PaginationInfo>.Value(this, () => new PaginationInfo());

    /// <summary>
    /// Feed of customers from the API.
    /// Automatically refreshes when SearchQuery, CurrentPage, or SortOrder changes.
    /// </summary>
    public IListFeed<CustomerListDto> Customers => Feed
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

            var response = await _customerService.GetCustomersAsync(parameters, ct);

            // Update pagination info
            await Pagination.UpdateAsync(_ => new PaginationInfo
            {
                CurrentPage = response.CurrentPage,
                TotalPages = response.TotalPages,
                TotalCount = response.TotalCount,
                PageSize = response.PageSize,
                HasPreviousPage = response.HasPreviousPage,
                HasNextPage = response.HasNextPage
            }, ct);

            return response.Data.ToImmutableList();
        })
        .AsListFeed();

    /// <summary>
    /// Navigate to customer detail page.
    /// </summary>
    public async Task ViewCustomer(CustomerListDto customer)
    {
        await _navigator.NavigateRouteAsync(
            this,
            Routes.CustomerDetail,
            data: new Dictionary<string, object> { ["customerId"] = customer.Id });
    }

    /// <summary>
    /// Navigate to create new customer page.
    /// </summary>
    public async Task CreateCustomer()
    {
        await _navigator.NavigateRouteAsync(this, Routes.CustomerCreate);
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
/// Holds pagination state information.
/// </summary>
public record PaginationInfo
{
    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
    public int TotalCount { get; init; }
    public int PageSize { get; init; }
    public bool HasPreviousPage { get; init; }
    public bool HasNextPage { get; init; }

    /// <summary>
    /// Display text for current page info (e.g., "Page 1 of 5").
    /// </summary>
    public string PageInfo => TotalPages > 0 ? $"Page {CurrentPage + 1} of {TotalPages}" : "No results";

    /// <summary>
    /// Display text for total count info (e.g., "25 customers").
    /// </summary>
    public string CountInfo => TotalCount == 1 ? "1 customer" : $"{TotalCount} customers";
}
