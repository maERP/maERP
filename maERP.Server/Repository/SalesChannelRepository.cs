#nullable disable

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Server.Models;
using maERP.Shared.Models;

namespace maERP.Server.Repository;

public interface ISalesChannelRepository : IGenericRepository<SalesChannel>
{

}

public class SalesChannelRepository : GenericRepository<SalesChannel>, ISalesChannelRepository
{
    private readonly ApplicationDbContext _context;

    public SalesChannelRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
    }
}