using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class PromptRepository : GenericRepository<Prompt>, IPromptRepository
{
    public PromptRepository(ApplicationDbContext context) : base(context)
    {

    }
    
    public async Task<Prompt?> GetByIdentifier(string identifier)
    {
        return await _context.Prompt.FirstOrDefaultAsync(p => p.Identifier == identifier);
    }
}