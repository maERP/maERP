using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;

namespace maERP.Persistence.Repositories;

public class AiModelRepository : GenericRepository<AiModel>, IAiModelRepository
{
    public AiModelRepository(ApplicationDbContext context) : base(context)
    {

    }
}