using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface ITaxClassRepository : IGenericRepository<TaxClass>
{
    Task<TaxClass?> GetByTaxRateAsync(double taxRate);
}