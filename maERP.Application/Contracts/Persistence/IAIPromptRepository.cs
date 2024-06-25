using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface IAIPromptRepository : IGenericRepository<AIPrompt>
{
    Task<AIPrompt?> GetByIdentifier(string identifier);
}