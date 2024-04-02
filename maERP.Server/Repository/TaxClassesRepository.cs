using AutoMapper;
using maERP.Server.Contracts;
using maERP.Server.Services;
using maERP.Shared.Models.Database;

namespace maERP.Server.Repository;

public class TaxClassRepository : GenericRepository<TaxClass>, ITaxClassRepository
{
    public TaxClassRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}