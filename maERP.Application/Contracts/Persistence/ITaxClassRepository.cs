using maERP.Domain.Models;

namespace maERP.Application.Contracts.Persistence;

public interface ICountryRepository : IGenericRepository<Country>
{
    Task<Country?> GetCountryByString(string country);
}