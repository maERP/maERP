using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Invoice.Queries.InvoiceList;

/// <summary>
/// Query for retrieving a paginated list of invoices.
/// Implements IRequest to work with MediatR, returning a paginated list of invoice DTOs.
/// </summary>
public class InvoiceListQuery : IRequest<PaginatedResult<InvoiceListDto>>
{
    /// <summary>
    /// Page number for pagination (1-based)
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Optional search string to filter invoices
    /// </summary>
    public string SearchString { get; set; }

    /// <summary>
    /// Optional sorting parameters
    /// </summary>
    public string[] OrderBy { get; set; }

    /// <summary>
    /// Creates a new instance of InvoiceListQuery with the specified pagination and filtering parameters
    /// </summary>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Number of items per page (default: 10)</param>
    /// <param name="searchString">Search string for filtering (default: empty)</param>
    /// <param name="orderBy">Comma-separated list of sorting parameters (default: empty)</param>
    public InvoiceListQuery(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
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
