using AutoMapper;
using maERP.Server.Models;

namespace maERP.Server.Repository;

public class TaxClassRepository : GenericRepository<TaxClass>, ITaxClassRepository
{
    public TaxClassRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}