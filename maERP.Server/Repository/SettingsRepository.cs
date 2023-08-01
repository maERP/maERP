#nullable disable

using AutoMapper;
using maERP.Server.Models;

namespace maERP.Server.Repository;

public interface ISettingsRepository : IGenericRepository<Setting>
{
}

public class SettingsRepository : GenericRepository<Setting>, ISettingsRepository
{
    public SettingsRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}