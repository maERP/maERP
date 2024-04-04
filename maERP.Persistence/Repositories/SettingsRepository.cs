using AutoMapper;
using maERP.Server.Contracts;
using maERP.Server.Services;
using maERP.Shared.Models.Database;

namespace maERP.Persistence.Repositories;

public class SettingsRepository : GenericRepository<Setting>, ISettingsRepository
{
    public SettingsRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}