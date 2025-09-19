using FluentValidation;

namespace maERP.Application.Features.ImportExport.Commands.CustomerCsvImport;

/// <summary>
/// Validator for CustomerCsvImportCommand to ensure the CSV file is valid for processing.
/// </summary>
public class CustomerCsvImportValidator : AbstractValidator<CustomerCsvImportCommand>
{
    public CustomerCsvImportValidator()
    {
        RuleFor(x => x.CsvFile)
            .NotNull()
            .WithMessage("CSV file is required");

        RuleFor(x => x.CsvFile.Length)
            .GreaterThan(0)
            .WithMessage("CSV file cannot be empty")
            .When(x => x.CsvFile != null);

        RuleFor(x => x.CsvFile.ContentType)
            .Must(contentType => contentType == "text/csv" ||
                                contentType == "application/vnd.ms-excel" ||
                                contentType == "text/plain")
            .WithMessage("File must be a CSV file")
            .When(x => x.CsvFile != null);

        RuleFor(x => x.CsvFile.Length)
            .LessThan(10 * 1024 * 1024) // 10 MB limit
            .WithMessage("CSV file size cannot exceed 10 MB")
            .When(x => x.CsvFile != null);

        RuleFor(x => x.CsvFile.FileName)
            .Must(fileName => fileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            .WithMessage("File must have a .csv extension")
            .When(x => x.CsvFile != null && !string.IsNullOrEmpty(x.CsvFile.FileName));
    }
}