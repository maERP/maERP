using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class AiPromptRepository : GenericRepository<AiPrompt>, IAiPromptRepository
{
    public AiPromptRepository(ApplicationDbContext context) : base(context)
    {

    }
    
    public async Task<AiPrompt?> GetByIdentifier(string identifier)
    {
        return await _context.AiPrompt.FirstOrDefaultAsync(p => p.Identifier == identifier);
    }
}