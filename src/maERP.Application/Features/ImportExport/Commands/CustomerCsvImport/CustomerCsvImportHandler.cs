using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.ImportExport.Commands.CustomerCsvImport;

/// <summary>
/// Handler for processing customer CSV import commands.
/// Implements IRequestHandler from MediatR to handle CustomerCsvImportCommand requests
/// and return import statistics wrapped in a Result.
/// </summary>
public class CustomerCsvImportHandler : IRequestHandler<CustomerCsvImportCommand, Result<CustomerCsvImportResult>>
{
    private readonly IAppLogger<CustomerCsvImportHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public CustomerCsvImportHandler(
        IAppLogger<CustomerCsvImportHandler> logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result<CustomerCsvImportResult>> Handle(CustomerCsvImportCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting CSV import of customers, file size: {FileSize} bytes", request.CsvFile.Length);

        var result = new Result<CustomerCsvImportResult>();
        var importResult = new CustomerCsvImportResult();

        try
        {
            // Validate the CSV file
            var validator = new CustomerCsvImportValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
                return result;
            }

            // Read and process CSV file
            using var stream = request.CsvFile.OpenReadStream();
            using var reader = new StreamReader(stream, Encoding.UTF8);
            
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                HeaderValidated = null,
                TrimOptions = TrimOptions.Trim
            };

            using var csv = new CsvReader(reader, config);

            var customers = new List<CustomerCsvRecord>();
            var rowNumber = 1; // Start at 1 (header is row 0)

            try
            {
                await csv.ReadAsync();
                csv.ReadHeader();
                
                while (await csv.ReadAsync())
                {
                    rowNumber++;
                    try
                    {
                        var record = csv.GetRecord<CustomerCsvRecord>();
                        if (record != null)
                        {
                            record.RowNumber = rowNumber;
                            customers.Add(record);
                        }
                    }
                    catch (Exception ex)
                    {
                        importResult.Errors.Add($"Row {rowNumber}: Error parsing CSV data - {ex.Message}");
                        importResult.SkippedCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.Add($"Error reading CSV file: {ex.Message}");
                return result;
            }

            importResult.TotalRows = customers.Count;
            _logger.LogInformation("Parsed {Count} customer records from CSV", customers.Count);

            // Process each customer record
            foreach (var csvRecord in customers)
            {
                try
                {
                    // Validate required fields
                    if (string.IsNullOrWhiteSpace(csvRecord.Firstname) || string.IsNullOrWhiteSpace(csvRecord.Lastname))
                    {
                        importResult.Errors.Add($"Row {csvRecord.RowNumber}: Firstname and Lastname are required");
                        importResult.SkippedCount++;
                        continue;
                    }

                    // Check if customer already exists by email
                    Domain.Entities.Customer? existingCustomer = null;
                    if (!string.IsNullOrWhiteSpace(csvRecord.Email))
                    {
                        existingCustomer = await _customerRepository.GetCustomerByEmailAsync(csvRecord.Email);
                    }

                    if (existingCustomer != null && !request.UpdateExisting)
                    {
                        importResult.Errors.Add($"Row {csvRecord.RowNumber}: Customer with email '{csvRecord.Email}' already exists");
                        importResult.SkippedCount++;
                        continue;
                    }

                    // Map CSV record to Customer entity
                    var customer = MapCsvRecordToCustomer(csvRecord, existingCustomer);

                    if (existingCustomer != null)
                    {
                        // Update existing customer
                        await _customerRepository.UpdateAsync(customer);
                        importResult.UpdatedCount++;
                        _logger.LogInformation("Updated existing customer: {Email}", customer.Email);
                    }
                    else
                    {
                        // Create new customer
                        await _customerRepository.CreateAsync(customer);
                        importResult.ImportedCount++;
                        _logger.LogInformation("Created new customer: {Email}", customer.Email);
                    }
                }
                catch (Exception ex)
                {
                    importResult.Errors.Add($"Row {csvRecord.RowNumber}: Error processing customer - {ex.Message}");
                    importResult.SkippedCount++;
                    _logger.LogError("Error processing customer row {RowNumber}: {Message}", csvRecord.RowNumber, ex.Message);
                }
            }

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = importResult;

            _logger.LogInformation("CSV import completed. Imported: {Imported}, Updated: {Updated}, Skipped: {Skipped}", 
                importResult.ImportedCount, importResult.UpdatedCount, importResult.SkippedCount);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred during CSV import: {ex.Message}");
            _logger.LogError("Error during CSV import: {Message}", ex.Message);
        }

        return result;
    }

    private static Domain.Entities.Customer MapCsvRecordToCustomer(CustomerCsvRecord csvRecord, Domain.Entities.Customer? existingCustomer)
    {
        var customer = existingCustomer ?? new Domain.Entities.Customer();

        customer.Firstname = csvRecord.Firstname;
        customer.Lastname = csvRecord.Lastname;
        customer.CompanyName = csvRecord.CompanyName ?? string.Empty;
        customer.Email = csvRecord.Email ?? string.Empty;
        customer.Phone = csvRecord.Phone ?? string.Empty;
        customer.Website = csvRecord.Website ?? string.Empty;
        customer.VatNumber = csvRecord.VatNumber ?? string.Empty;
        customer.Note = csvRecord.Note ?? string.Empty;

        // Parse CustomerStatus from string
        if (!string.IsNullOrWhiteSpace(csvRecord.CustomerStatus))
        {
            if (Enum.TryParse<CustomerStatus>(csvRecord.CustomerStatus, true, out var status))
            {
                customer.CustomerStatus = status;
            }
            else
            {
                customer.CustomerStatus = CustomerStatus.Active; // Default value
            }
        }
        else
        {
            customer.CustomerStatus = CustomerStatus.Active; // Default value
        }

        // Parse DateEnrollment from string
        if (!string.IsNullOrWhiteSpace(csvRecord.DateEnrollment))
        {
            if (DateTimeOffset.TryParse(csvRecord.DateEnrollment, out var enrollmentDate))
            {
                customer.DateEnrollment = enrollmentDate;
            }
            else
            {
                customer.DateEnrollment = DateTimeOffset.Now; // Default to now
            }
        }
        else
        {
            customer.DateEnrollment = DateTimeOffset.Now; // Default to now
        }

        return customer;
    }
}

/// <summary>
/// CSV record class representing a customer row in the CSV file
/// </summary>
public class CustomerCsvRecord
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string? CompanyName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Website { get; set; }
    public string? VatNumber { get; set; }
    public string? Note { get; set; }
    public string? CustomerStatus { get; set; }
    public string? DateEnrollment { get; set; }

    /// <summary>
    /// Internal property to track which row this record came from (for error reporting)
    /// </summary>
    public int RowNumber { get; set; }
}