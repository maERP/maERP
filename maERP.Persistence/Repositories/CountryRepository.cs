using maERP.Application.Contracts.Persistence;
using maERP.Domain.Models;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<Country?> GetCountryByString(string country)
    {
        return await _context.Country.FirstOrDefaultAsync(c => c.Name == country || c.CountryCode == country);
    }
}