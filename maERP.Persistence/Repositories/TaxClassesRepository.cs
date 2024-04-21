using maERP.Application.Contracts.Persistence;
using maERP.Domain.Models;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class TaxClassRepository : GenericRepository<TaxClass>, ITaxClassRepository
{
    public TaxClassRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<TaxClass?> GetByTaxRateAsync(double taxRate)
    {
        return await _context.TaxClass.FirstOrDefaultAsync(p => p.TaxRate == taxRate);
    }
}