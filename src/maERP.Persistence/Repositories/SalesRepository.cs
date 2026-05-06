using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class SalesRepository : GenericRepository<Sales>, ISalesRepository
{
    public SalesRepository(ApplicationDbContext context, ITenantContext tenantContext) : base(context, tenantContext)
    {
    }

    public async Task<Sales?> GetWithDetailsAsync(Guid id)
    {
        return await Entities
            .Where(o => o.Id == id)
            .Include(o => o.SalesItems)
            .Include(o => o.SalesHistories)
            .Include(o => o.Customer)
            .AsSplitQuery()
            .FirstOrDefaultAsync() ?? null;
    }

    public async Task<Sales?> GetByRemoteSalesIdAsync(Guid salesChannelId, string remoteSalesId)
    {
        return await Entities
            .Where(o => o.RemoteSalesId == remoteSalesId)
            .Where(o => o.SalesChannelId == salesChannelId)
            .FirstOrDefaultAsync() ?? null;
    }

    public async Task<List<SalesHistory>> GetSalesHistoryAsync(Guid salesId)
    {
        var currentTenantId = TenantContext.GetCurrentTenantId();
        var query = Context.SalesHistory.AsQueryable();
        
        if (currentTenantId.HasValue)
        {
            query = query.Where(oh => oh.TenantId == null || oh.TenantId == currentTenantId.Value);
        }
        else
        {
            query = query.Where(oh => oh.TenantId == null);
        }
        
        return await query
            .Where(oh => oh.SalesId == salesId)
            .OrderByDescending(oh => oh.DateCreated)
            .ToListAsync();
    }

    public async Task<int> GetNextSalesIdAsync()
    {
        var maxSalesId = await Entities
            .Select(o => (int?)o.SalesId)
            .MaxAsync() ?? 0;

        return maxSalesId + 1;
    }

    public async Task<bool> CanCreateInvoice(Guid salesId)
    {
        var sales = await Entities
            .Where(o => o.Id == salesId)
            .FirstOrDefaultAsync();

        if (sales == null)
        {
            return false;
        }

        // Check if the payment status is completely paid
        if (sales.PaymentStatus != PaymentStatus.CompletelyPaid)
        {
            return false;
        }

        // Check if an invoice already exists for this sales
        var currentTenantId = TenantContext.GetCurrentTenantId();
        var invoiceQuery = Context.Invoice.AsQueryable();
        
        if (currentTenantId.HasValue)
        {
            invoiceQuery = invoiceQuery.Where(i => i.TenantId == null || i.TenantId == currentTenantId.Value);
        }
        else
        {
            invoiceQuery = invoiceQuery.Where(i => i.TenantId == null);
        }
        
        var invoiceExists = await invoiceQuery.AnyAsync(i => i.SalesId == salesId);

        // Return false if invoice already exists
        return !invoiceExists;
    }
}