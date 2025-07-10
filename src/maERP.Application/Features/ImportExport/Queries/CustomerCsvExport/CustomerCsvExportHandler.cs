using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.ImportExport.Queries.CustomerCsvExport;

/// <summary>
/// Handler for processing customer CSV export queries.
/// Implements IRequestHandler from MediatR to handle CustomerCsvExportQuery requests
/// and return CSV export data wrapped in a Result.
/// </summary>
public class CustomerCsvExportHandler : IRequestHandler<CustomerCsvExportQuery, Result<CustomerCsvExportResult>>
{
    private readonly IAppLogger<CustomerCsvExportHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public CustomerCsvExportHandler(
        IAppLogger<CustomerCsvExportHandler> logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result<CustomerCsvExportResult>> Handle(CustomerCsvExportQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting CSV export of customers with search: '{SearchString}'", request.SearchString);

        var result = new Result<CustomerCsvExportResult>();
        var exportResult = new CustomerCsvExportResult();

        try
        {
            // Create filter specification
            var customerFilterSpec = new CustomerFilterSpecification(request.SearchString);

            // Get customers from database
            var customers = await _customerRepository.Entities
                .Specify(customerFilterSpec)
                .Where(c => !request.ActiveCustomersOnly || c.CustomerStatus == Domain.Enums.CustomerStatus.Active)
                .OrderBy(c => c.Lastname)
                .ThenBy(c => c.Firstname)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Retrieved {Count} customers for CSV export", customers.Count);

            // Generate CSV content
            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream, Encoding.UTF8);
            
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                ShouldQuote = args => args.Field?.Contains(',') == true || args.Field?.Contains('"') == true || args.Field?.Contains('\n') == true
            };

            using var csv = new CsvWriter(writer, config);

            // Write header
            csv.WriteField("Firstname");
            csv.WriteField("Lastname");
            csv.WriteField("CompanyName");
            csv.WriteField("Email");
            csv.WriteField("Phone");
            csv.WriteField("Website");
            csv.WriteField("VatNumber");
            csv.WriteField("Note");
            csv.WriteField("CustomerStatus");
            csv.WriteField("DateEnrollment");

            if (request.IncludeAddresses)
            {
                csv.WriteField("AddressFirstname");
                csv.WriteField("AddressLastname");
                csv.WriteField("AddressCompanyName");
                csv.WriteField("Street");
                csv.WriteField("HouseNr");
                csv.WriteField("Zip");
                csv.WriteField("City");
                csv.WriteField("Country");
                csv.WriteField("DefaultDeliveryAddress");
                csv.WriteField("DefaultInvoiceAddress");
            }

            await csv.NextRecordAsync();

            // Write customer data
            foreach (var customer in customers)
            {
                if (request.IncludeAddresses && customer.CustomerAddresses != null && customer.CustomerAddresses.Any())
                {
                    // Export one row per address
                    foreach (var address in customer.CustomerAddresses)
                    {
                        WriteCustomerRow(csv, customer, address);
                        await csv.NextRecordAsync();
                    }
                }
                else
                {
                    // Export one row per customer
                    WriteCustomerRow(csv, customer, null);
                    await csv.NextRecordAsync();
                }
            }

            await writer.FlushAsync();
            exportResult.CsvData = memoryStream.ToArray();
            exportResult.CustomerCount = customers.Count;
            exportResult.FileName = $"customers_export_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = exportResult;

            _logger.LogInformation("CSV export completed successfully. Exported {Count} customers, file size: {Size} bytes", 
                customers.Count, exportResult.CsvData.Length);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred during CSV export: {ex.Message}");
            _logger.LogError("Error during CSV export: {Message}", ex.Message);
        }

        return result;
    }

    private static void WriteCustomerRow(CsvWriter csv, Domain.Entities.Customer customer, Domain.Entities.CustomerAddress? address)
    {
        csv.WriteField(customer.Firstname);
        csv.WriteField(customer.Lastname);
        csv.WriteField(customer.CompanyName);
        csv.WriteField(customer.Email);
        csv.WriteField(customer.Phone);
        csv.WriteField(customer.Website);
        csv.WriteField(customer.VatNumber);
        csv.WriteField(customer.Note);
        csv.WriteField(customer.CustomerStatus.ToString());
        csv.WriteField(customer.DateEnrollment.ToString("yyyy-MM-dd HH:mm:ss zzz"));

        if (address != null)
        {
            csv.WriteField(address.Firstname ?? string.Empty);
            csv.WriteField(address.Lastname ?? string.Empty);
            csv.WriteField(address.CompanyName ?? string.Empty);
            csv.WriteField(address.Street ?? string.Empty);
            csv.WriteField(address.HouseNr ?? string.Empty);
            csv.WriteField(address.Zip ?? string.Empty);
            csv.WriteField(address.City ?? string.Empty);
            csv.WriteField(address.Country?.Name ?? string.Empty);
            csv.WriteField(address.DefaultDeliveryAddress.ToString());
            csv.WriteField(address.DefaultInvoiceAddress.ToString());
        }
    }
}