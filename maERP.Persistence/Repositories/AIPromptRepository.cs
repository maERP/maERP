using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class AIPromptRepository : GenericRepository<AIPrompt>, IAIPromptRepository
{
    public AIPromptRepository(ApplicationDbContext context) : base(context)
    {

    }
    
    public async Task<AIPrompt?> GetByIdentifier(string identifier)
    {
        return await _context.Prompt.FirstOrDefaultAsync(p => p.Identifier == identifier);
    }
}