using maERP.Application.Contracts.Persistence;
using maERP.Domain;
using maERP.Persistence.DatabaseContext;

namespace maERP.Persistence.Repositories;

public class SettingsRepository : GenericRepository<Setting>, ISettingsRepository
{
    public SettingsRepository(ApplicationDbContext context) : base(context)
    {
    }
}