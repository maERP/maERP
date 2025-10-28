using maERP.Application.Features.ImportExport.Commands.CustomerCsvImport;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for import/export operations
/// </summary>
public interface IImportExportApiClient
{
    /// <summary>
    /// Import customers from CSV file
    /// </summary>
    Task<Result<CustomerCsvImportResult>?> ImportCustomersCsvAsync(
        Stream csvFileStream,
        string fileName,
        bool updateExisting = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Export customers to CSV file
    /// </summary>
    Task<HttpResponseMessage> ExportCustomersCsvAsync(
        string searchString = "",
        bool includeAddresses = false,
        bool activeCustomersOnly = false,
        CancellationToken cancellationToken = default);
}
