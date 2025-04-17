using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

/// <summary>
/// Repository interface for Invoice entity operations
/// </summary>
public interface IInvoiceRepository : IGenericRepository<Invoice>
{
    // Add any invoice-specific repository methods here
}