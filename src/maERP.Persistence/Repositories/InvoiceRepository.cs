using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

/// <summary>
/// Repository for Invoice entity operations
/// </summary>
public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
{
    public InvoiceRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // You can add invoice-specific repository methods here if needed in the future
    // For example:
    
    /// <summary>
    /// Gets an invoice with all related details including customer and invoice items
    /// </summary>
    /// <param name="id">The invoice ID</param>
    /// <returns>The invoice with all related entities or null if not found</returns>
    public async Task<Invoice?> GetInvoiceWithDetailsAsync(int id)
    {
        return await Context.Set<Invoice>()
            .Where(x => x.Id == id)
            .Include(x => x.Customer)
            .Include(x => x.Order)
            .Include(x => x.InvoiceItems)
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
}