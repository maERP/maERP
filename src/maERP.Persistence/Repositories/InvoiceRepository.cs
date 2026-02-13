using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace maERP.Persistence.Repositories;

/// <summary>
/// Repository for Invoice entity operations
/// </summary>
public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
{
    private readonly ILogger<InvoiceRepository> _logger;
    private readonly IPdfService _pdfService;

    public InvoiceRepository(ApplicationDbContext context, ITenantContext tenantContext, ILogger<InvoiceRepository> logger, IPdfService pdfService) : base(context, tenantContext)
    {
        _logger = logger;
        _pdfService = pdfService;
    }

    // You can add invoice-specific repository methods here if needed in the future
    // For example:

    /// <summary>
    /// Gets an invoice with all related details including customer and invoice items
    /// </summary>
    /// <param name="id">The invoice ID</param>
    /// <returns>The invoice with all related entities or null if not found</returns>
    public async Task<Invoice?> GetInvoiceWithDetailsAsync(Guid id)
    {
        // Start with ignoring query filters to ensure fresh database reads
        var query = Context.Set<Invoice>().IgnoreQueryFilters().AsQueryable();

        // Apply manual tenant filtering - crucial for multi-tenant scenarios
        var currentTenantId = base.TenantContext.GetCurrentTenantId();
        if (currentTenantId.HasValue)
        {
            // Manual tenant filtering for both production and test environments
            query = query.Where(x => x.TenantId == null || x.TenantId == currentTenantId.Value);
        }
        else
        {
            // If no tenant context, only return tenant-agnostic entities
            query = query.Where(x => x.TenantId == null);
        }

        return await query
            .Where(x => x.Id == id)
            .Include(x => x.Customer)
            .Include(x => x.Order)
            .Include(x => x.InvoiceItems)
            .AsSplitQuery()
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Gets invoices for a specific customer
    /// </summary>
    /// <param name="customerId">The customer ID</param>
    /// <returns>Collection of invoices for the customer</returns>
    public async Task<ICollection<Invoice>> GetInvoicesByCustomerIdAsync(int customerId)
    {
        return await Context.Set<Invoice>()
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();
    }

    /// <summary>
    /// Gets invoices by their status
    /// </summary>
    /// <param name="status">The invoice status</param>
    /// <returns>Collection of invoices with the specified status</returns>
    public async Task<ICollection<Invoice>> GetInvoicesByStatusAsync(InvoiceStatus status)
    {
        return await Context.Set<Invoice>()
            .Where(x => x.InvoiceStatus == status)
            .ToListAsync();
    }

    /// <summary>
    /// Creates an invoice from an order, including all invoice items from order items
    /// </summary>
    /// <param name="order">The order with details to create invoice from</param>
    /// <returns>The created invoice</returns>
    public async Task<Invoice> CreateInvoiceFromOrderAsync(Order order)
    {
        _logger.LogInformation("Creating invoice for order with ID: {Id}", order.Id);

        try
        {
            // Rechnung erstellen
            var invoice = new Invoice
            {
                InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{order.Id}",
                InvoiceDate = DateTime.UtcNow,
                CustomerId = order.CustomerId,
                OrderId = order.Id,
                Subtotal = order.Subtotal,
                ShippingCost = order.ShippingCost,
                TotalTax = order.TotalTax,
                Total = order.Total,
                PaymentStatus = order.PaymentStatus,
                InvoiceStatus = InvoiceStatus.Created,
                PaymentMethod = order.PaymentMethod,
                PaymentTransactionId = order.PaymentTransactionId,
                Notes = $"Automatisch erstellte Rechnung für Bestellung {order.Id}",

                // Rechnungsadresse
                InvoiceAddressFirstName = order.InvoiceAddressFirstName,
                InvoiceAddressLastName = order.InvoiceAddressLastName,
                InvoiceAddressCompanyName = order.InvoiceAddressCompanyName,
                InvoiceAddressPhone = order.InvoiceAddressPhone,
                InvoiceAddressStreet = order.InvoiceAddressStreet,
                InvoiceAddressCity = order.InvoiceAddressCity,
                InvoiceAddressZip = order.InvoiceAddressZip,
                InvoiceAddressCountry = order.InvoiceAddressCountry,

                // Lieferadresse
                DeliveryAddressFirstName = order.DeliveryAddressFirstName,
                DeliveryAddressLastName = order.DeliveryAddressLastName,
                DeliveryAddressCompanyName = order.DeliveryAddressCompanyName,
                DeliveryAddressPhone = order.DeliveryAddressPhone,
                DeliveryAddressStreet = order.DeliveryAddressStreet,
                DeliveryAddressCity = order.DeliveryAddressCity,
                DeliveryAddressZip = order.DeliveryAddressZip,
                DeliveryAddressCountry = order.DeliveryAddressCountry
            };

            // Rechnungspositionen aus OrderItems erstellen
            if (order.OrderItems != null)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    var invoiceItem = new InvoiceItem
                    {
                        Name = orderItem.Name,
                        //SKU = orderItem.SKU,
                        Quantity = orderItem.Quantity,
                        // UnitPrice = orderItem.UnitPrice,
                        TaxRate = orderItem.TaxRate,
                        //TaxAmount = orderItem.TaxAmount,
                        //Total = orderItem.Total,
                        //Notes = orderItem.Notes
                    };

                    invoice.InvoiceItems.Add(invoiceItem);
                }
            }

            // Rechnung in der Datenbank speichern
            var createdInvoice = await this.CreateAsync(invoice);

            // PDF-Rechnung erstellen
            string outputPath = $"Invoices/INV-{DateTime.UtcNow:yyyyMMdd}-{order.Id}.pdf";
            _pdfService.GenerateInvoice(invoice, outputPath);

            _logger.LogInformation("Successfully created invoice for order ID: {Id}", order.Id);
            return invoice;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error creating invoice for order ID {Id}: {Message}", order.Id, ex.Message);
            throw;
        }
    }

    public override async Task DeleteAsync(Invoice entity)
    {
        var invoice = await Context.Invoice
            .IgnoreQueryFilters()
            .Include(i => i.InvoiceItems)
            .FirstOrDefaultAsync(i => i.Id == entity.Id);

        if (invoice == null)
        {
            throw new InvalidOperationException($"Invoice with ID {entity.Id} not found for deletion");
        }

        var currentTenantId = TenantContext.GetCurrentTenantId();
        if (currentTenantId.HasValue && invoice.TenantId != null && invoice.TenantId != currentTenantId)
        {
            throw new UnauthorizedAccessException("Cannot delete invoice belonging to a different tenant");
        }

        if (invoice.InvoiceItems?.Any() == true)
        {
            Context.InvoiceItem.RemoveRange(invoice.InvoiceItems);
        }

        Context.Remove(invoice);
        await Context.SaveChangesAsync();

        if (Context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
        {
            Context.ChangeTracker.Clear();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            await Context.Invoice.IgnoreQueryFilters().Where(x => x.Id == Guid.Empty).FirstOrDefaultAsync();
        }
    }
}
