using System.Net.Http.Headers;
using System.Net.Http.Json;
using maERP.Application.Features.ImportExport.Commands.CustomerCsvImport;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of import/export API client
/// </summary>
public class ImportExportApiClient : ApiClientBase, IImportExportApiClient
{
    private const string BaseEndpoint = "api/v1/ImportExport";

    public ImportExportApiClient(HttpClient httpClient, ILogger<ImportExportApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<Result<CustomerCsvImportResult>?> ImportCustomersCsvAsync(
        Stream csvFileStream,
        string fileName,
        bool updateExisting = false,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var content = new MultipartFormDataContent();

            var streamContent = new StreamContent(csvFileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            content.Add(streamContent, "csvFile", fileName);
            content.Add(new StringContent(updateExisting.ToString()), "updateExisting");

            Logger.LogDebug("POST {Endpoint}/customers/import", BaseEndpoint);
            var response = await HttpClient.PostAsync($"{BaseEndpoint}/customers/import", content, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Result<CustomerCsvImportResult>>(JsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "POST request failed: {Endpoint}/customers/import", BaseEndpoint);
            throw;
        }
    }

    public async Task<HttpResponseMessage> ExportCustomersCsvAsync(
        string searchString = "",
        bool includeAddresses = false,
        bool activeCustomersOnly = false,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string?>
        {
            { "searchString", searchString },
            { "includeAddresses", includeAddresses.ToString() },
            { "activeCustomersOnly", activeCustomersOnly.ToString() }
        };

        var url = BuildUrl($"{BaseEndpoint}/customers/export", queryParams);
        return await GetResponseAsync(url, cancellationToken);
    }
}
