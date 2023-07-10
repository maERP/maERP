#nullable disable

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Server.Models;
using maERP.Shared.Models;

namespace maERP.Server.Repository;

public interface ISettingsRepository : IGenericRepository<Setting>
{
}

public class SettingsRepository : GenericRepository<Setting>, ISettingsRepository
{
    private readonly ApplicationDbContext _context;

    public SettingsRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
    }
}