using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace maERP.Application.Features.ImportExport.Commands.CustomerCsvImport;

/// <summary>
/// Command for importing customers from a CSV file.
/// Implements IRequest to work with MediatR, returning a Result with import statistics.
/// </summary>
public class CustomerCsvImportCommand : IRequest<Result<CustomerCsvImportResult>>
{
    /// <summary>
    /// The CSV file containing customer data to import
    /// </summary>
    public IFormFile CsvFile { get; set; } = null!;

    /// <summary>
    /// Whether to update existing customers if they already exist (based on email)
    /// </summary>
    public bool UpdateExisting { get; set; } = false;
}

/// <summary>
/// Result class containing statistics about the CSV import operation
/// </summary>
public class CustomerCsvImportResult
{
    /// <summary>
    /// Number of customers successfully imported
    /// </summary>
    public int ImportedCount { get; set; }

    /// <summary>
    /// Number of customers updated (if UpdateExisting is true)
    /// </summary>
    public int UpdatedCount { get; set; }

    /// <summary>
    /// Number of customers skipped due to errors or duplicates
    /// </summary>
    public int SkippedCount { get; set; }

    /// <summary>
    /// List of error messages for rows that couldn't be processed
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Total number of rows processed (excluding header)
    /// </summary>
    public int TotalRows { get; set; }
}