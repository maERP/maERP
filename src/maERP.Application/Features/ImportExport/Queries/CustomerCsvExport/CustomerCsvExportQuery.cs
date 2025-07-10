using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.ImportExport.Queries.CustomerCsvExport;

/// <summary>
/// Query for exporting customers to a CSV file.
/// Implements IRequest to work with MediatR, returning a Result with CSV file data.
/// </summary>
public class CustomerCsvExportQuery : IRequest<Result<CustomerCsvExportResult>>
{
    /// <summary>
    /// Optional search string to filter customers
    /// </summary>
    public string SearchString { get; set; } = string.Empty;

    /// <summary>
    /// Whether to include customer addresses in the export
    /// </summary>
    public bool IncludeAddresses { get; set; } = false;

    /// <summary>
    /// Whether to include only active customers
    /// </summary>
    public bool ActiveCustomersOnly { get; set; } = false;
}

/// <summary>
/// Result class containing the CSV export data
/// </summary>
public class CustomerCsvExportResult
{
    /// <summary>
    /// The CSV file content as byte array, ready for download
    /// </summary>
    public byte[] CsvData { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// The suggested filename for the CSV export
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// The MIME content type for the CSV file
    /// </summary>
    public string ContentType { get; set; } = "text/csv";

    /// <summary>
    /// Number of customers exported
    /// </summary>
    public int CustomerCount { get; set; }

    /// <summary>
    /// Export timestamp
    /// </summary>
    public DateTimeOffset ExportDate { get; set; } = DateTimeOffset.Now;
}