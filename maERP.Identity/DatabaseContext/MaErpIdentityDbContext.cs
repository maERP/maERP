using maERP.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace maERP.Identity.DatabaseContext;

public class MaErpIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public MaErpIdentityDbContext(DbContextOptions<MaErpIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(typeof(MaErpIdentityDbContext).Assembly);
    }
}