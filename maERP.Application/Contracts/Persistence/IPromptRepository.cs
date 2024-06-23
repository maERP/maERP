using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface IPromptRepository : IGenericRepository<Prompt>
{
    Task<Prompt?> GetByIdentifier(string identifier);
}
