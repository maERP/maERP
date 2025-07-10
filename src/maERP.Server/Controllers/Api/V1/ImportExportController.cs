using Asp.Versioning;
using maERP.Application.Features.ImportExport.Commands.CustomerCsvImport;
using maERP.Application.Features.ImportExport.Queries.CustomerCsvExport;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class ImportExportController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Import customers from a CSV file
    /// </summary>
    /// <param name="csvFile">The CSV file containing customer data</param>
    /// <param name="updateExisting">Whether to update existing customers if they already exist</param>
    /// <returns>Import statistics</returns>
    [HttpPost("customers/import")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<CustomerCsvImportResult>>> ImportCustomersCsv(
        IFormFile csvFile, 
        bool updateExisting = false)
    {
        var command = new CustomerCsvImportCommand
        {
            CsvFile = csvFile,
            UpdateExisting = updateExisting
        };

        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }

    /// <summary>
    /// Export customers to a CSV file
    /// </summary>
    /// <param name="searchString">Optional search string to filter customers</param>
    /// <param name="includeAddresses">Whether to include customer addresses in the export</param>
    /// <param name="activeCustomersOnly">Whether to include only active customers</param>
    /// <returns>CSV file with customer data</returns>
    [HttpGet("customers/export")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExportCustomersCsv(
        string searchString = "",
        bool includeAddresses = false,
        bool activeCustomersOnly = false)
    {
        var query = new CustomerCsvExportQuery
        {
            SearchString = searchString,
            IncludeAddresses = includeAddresses,
            ActiveCustomersOnly = activeCustomersOnly
        };

        var response = await mediator.Send(query);

        if (!response.Succeeded || response.Data == null)
        {
            return StatusCode((int)response.StatusCode, response);
        }

        return File(
            response.Data.CsvData,
            response.Data.ContentType,
            response.Data.FileName);
    }
}