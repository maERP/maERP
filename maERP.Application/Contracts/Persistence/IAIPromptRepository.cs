using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface IAiPromptRepository : IGenericRepository<AiPrompt>
{
    Task<AiPrompt?> GetByIdentifier(string identifier);
}