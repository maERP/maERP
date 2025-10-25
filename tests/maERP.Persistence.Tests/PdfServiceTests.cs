using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Infrastructure.PDF;
using Microsoft.EntityFrameworkCore.Storage;

namespace maERP.Persistence.Tests;

public class PdfServiceTests
{
    private sealed class FakeSettingRepository : ISettingRepository
    {
        private readonly List<Setting> _settings;

        public FakeSettingRepository(IEnumerable<Setting> settings)
        {
            _settings = settings.ToList();
        }

        public IQueryable<Setting> Entities => _settings.AsQueryable();

        public IQueryable<TCt> GetContext<TCt>() where TCt : class => throw new NotSupportedException();

        public void Attach(Setting entity) => throw new NotSupportedException();

        public void AttachRange(IEnumerable<Setting> entities) => throw new NotSupportedException();

        public Task<Guid> CreateAsync(Setting entity) => throw new NotSupportedException();

        public Task<ICollection<Setting>> GetAllAsync() => Task.FromResult<ICollection<Setting>>(_settings);

        public Task<Setting?> GetByIdAsync(Guid id, bool asNoTracking = false) =>
            Task.FromResult(_settings.FirstOrDefault(s => s.Id == id));

        public Task UpdateAsync(Setting entity) => throw new NotSupportedException();

        public Task DeleteAsync(Setting entity) => throw new NotSupportedException();

        public Task<bool> ExistsAsync(Guid id) => Task.FromResult(_settings.Any(s => s.Id == id));

        public Task<bool> ExistsGloballyAsync(Guid id) => Task.FromResult(_settings.Any(s => s.Id == id));

        public Task<bool> IsUniqueAsync(Setting entity, Guid? id = null) =>
            Task.FromResult(!_settings.Any(s => s.Key == entity.Key && (!id.HasValue || s.Id != id.Value)));

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) =>
            throw new NotSupportedException();

        public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
            throw new NotSupportedException();

        public void Add(Setting entity) => throw new NotSupportedException();
    }

    [Fact]
    public void GenerateInvoice_WithOutputPath_ShouldReturnBytesAndPersistFile()
    {
        // Arrange
        var repository = new FakeSettingRepository(new[]
        {
            new Setting { Key = "Company.Name", Value = "Test Company" },
            new Setting { Key = "Company.Address", Value = "Test Street 1" },
            new Setting { Key = "Company.ZipCity", Value = "12345 Test City" },
            new Setting { Key = "Company.Country", Value = "Test Country" },
            new Setting { Key = "Company.Phone", Value = "+49 123 456" },
            new Setting { Key = "Company.Email", Value = "info@test.com" },
            new Setting { Key = "Company.Website", Value = "https://test.com" },
            new Setting { Key = "Company.TaxId", Value = "TAX-123" },
            new Setting { Key = "Company.VatId", Value = "VAT-456" },
            new Setting { Key = "Company.BankName", Value = "Test Bank" },
            new Setting { Key = "Company.Iban", Value = "DE00 0000 0000 0000" },
            new Setting { Key = "Company.Bic", Value = "TESTDEFF" }
        });

        var pdfService = new PdfService(repository);

        var invoice = new Invoice
        {
            TenantId = Guid.NewGuid(),
            InvoiceNumber = "INV-1000",
            InvoiceDate = DateTime.UtcNow,
            CustomerId = 42,
            Subtotal = 100m,
            ShippingCost = 5m,
            TotalTax = 19m,
            Total = 124m,
            PaymentStatus = PaymentStatus.Invoiced,
            InvoiceStatus = InvoiceStatus.Created,
            PaymentMethod = "Bank Transfer",
            PaymentTransactionId = "TXN-001",
            Notes = "Test invoice",
            InvoiceAddressFirstName = "John",
            InvoiceAddressLastName = "Doe",
            InvoiceAddressStreet = "Main Street 1",
            InvoiceAddressCity = "Test City",
            InvoiceAddressZip = "12345",
            InvoiceAddressCountry = "Test Country",
            DeliveryAddressFirstName = "John",
            DeliveryAddressLastName = "Doe",
            DeliveryAddressStreet = "Main Street 1",
            DeliveryAddressCity = "Test City",
            DeliveryAddressZip = "12345",
            DeliveryAddressCountry = "Test Country",
            InvoiceItems = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    Name = "Sample item",
                    Quantity = 2,
                    Unit = "pcs",
                    UnitPrice = 50m,
                    TotalPrice = 100m,
                    TaxRate = 19,
                    TaxAmount = 19m
                }
            }
        };

        var tempDirectory = Path.Combine(Path.GetTempPath(), "maerp-pdf-tests", Guid.NewGuid().ToString("N"));
        var outputPath = Path.Combine(tempDirectory, "invoice.pdf");

        try
        {
            // Act
            var pdfBytes = pdfService.GenerateInvoice(invoice, outputPath);

            // Assert
            Assert.NotNull(pdfBytes);
            Assert.True(pdfBytes!.Length > 0);
            Assert.True(File.Exists(outputPath));

            var persistedBytes = File.ReadAllBytes(outputPath);
            Assert.Equal(pdfBytes.Length, persistedBytes.Length);
        }
        finally
        {
            if (Directory.Exists(tempDirectory))
            {
                Directory.Delete(tempDirectory, true);
            }
        }
    }
}
