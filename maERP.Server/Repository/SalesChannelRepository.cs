#nullable disable

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Server.Contracts;
using maERP.Server.Models;
using maERP.Shared.Models;

namespace maERP.Server.Repository;

public class SalesChannelRepository : GenericRepository<SalesChannel>, ISalesChannelRepository
{
    private readonly ApplicationDbContext _context;

    public SalesChannelRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
    }

    public async Task<SalesChannel> getDetails(int id)
    {
        return await _context.SalesChannel.FirstOrDefaultAsync(q => q.Id == id);
    }
}