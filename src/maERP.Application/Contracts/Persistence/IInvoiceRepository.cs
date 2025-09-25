using maERP.Domain.Entities;
using maERP.Domain.Enums;

namespace maERP.Application.Contracts.Persistence;

/// <summary>
/// Repository interface for Invoice entity operations
/// </summary>
public interface IInvoiceRepository : IGenericRepository<Invoice>
{
    /// <summary>
    /// Gets an invoice with all related details including customer and invoice items
    /// </summary>
    /// <param name="id">The invoice ID</param>
    /// <returns>The invoice with all related entities or null if not found</returns>
    Task<Invoice?> GetInvoiceWithDetailsAsync(Guid id);

    /// <summary>
    /// Gets invoices for a specific customer
    /// </summary>
    /// <param name="customerId">The customer ID</param>
    /// <returns>Collection of invoices for the customer</returns>
    Task<ICollection<Invoice>> GetInvoicesByCustomerIdAsync(int customerId);

    /// <summary>
    /// Gets invoices by their status
    /// </summary>
    /// <param name="status">The invoice status</param>
    /// <returns>Collection of invoices with the specified status</returns>
    Task<ICollection<Invoice>> GetInvoicesByStatusAsync(InvoiceStatus status);

    /// <summary>
    /// Creates an invoice from an order, including all invoice items from order items
    /// </summary>
    /// <param name="order">The order with details to create invoice from</param>
    /// <returns>The created invoice</returns>
    Task<Invoice> CreateInvoiceFromOrderAsync(Order order);
}