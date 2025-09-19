using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class AiPromptRepository : GenericRepository<AiPrompt>, IAiPromptRepository
{
    public AiPromptRepository(ApplicationDbContext context, ITenantContext tenantContext) : base(context, tenantContext)
    {

    }

    public async Task<AiPrompt?> GetByIdentifier(string identifier)
    {
        return await Entities.FirstOrDefaultAsync(p => p.Identifier == identifier);
    }

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }
}