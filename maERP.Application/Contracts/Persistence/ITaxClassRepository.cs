using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface ICountryRepository : IGenericRepository<Country>
{
    Task<Country?> GetCountryByString(string country);
}