using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;

namespace maERP.Persistence.Repositories;

public class AIModelRepository : GenericRepository<AIModel>, IAIModelRepository
{
    public AIModelRepository(ApplicationDbContext context) : base(context)
    {

    }
}