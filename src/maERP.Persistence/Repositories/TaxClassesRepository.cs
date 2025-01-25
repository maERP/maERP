using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
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
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        return await Context.TaxClass.FirstOrDefaultAsync(p => p.TaxRate == taxRate);
    }
}