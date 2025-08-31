using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Exceptions;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class SalesChannelRepository : GenericRepository<SalesChannel>, ISalesChannelRepository
{
    public SalesChannelRepository(ApplicationDbContext context, ITenantContext tenantContext) : base(context, tenantContext)
    {

    }

    public async Task<SalesChannel> GetDetails(int id)
    {
        var salesChannel = await Context.SalesChannel
            .Include(s => s.Warehouses)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (salesChannel == null)
        {
            throw new NotFoundException("SalesChannel not found", id);
        }

        return salesChannel;
    }

    public async Task<bool> SalesChannelIsUniqueAsync(SalesChannel salesChannel, int? id = null)
    {
        if (id == null)
        {
            return await Context.SalesChannel
                .AnyAsync(s => s.Name == salesChannel.Name) ? false : true;
        }

        return await Context.SalesChannel
            .AnyAsync(s => s.Name == salesChannel.Name && s.Id != id) ? false : true;
    }
}