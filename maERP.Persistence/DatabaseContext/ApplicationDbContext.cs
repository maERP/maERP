using maERP.Domain.Entities;
using maERP.Domain.Entities.Common;
using maERP.Identity.Configurations;
using maERP.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.DatabaseContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(MaErpIdentityDbContext).Assembly);
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
        modelBuilder.ApplyConfiguration(new SalesChannelConfiguration());
        modelBuilder.ApplyConfiguration(new TaxClassConfiguration());
    }

    public DbSet<AIModel> AIModel { get; set; }
    public DbSet<AIPrompt> Prompt { get; set; }
    public DbSet<Country> Country { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<CustomerAddress> CustomerAddress { get; set; }
    public DbSet<CustomerSalesChannel> CustomerSalesChannel { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<OrderItemSerialNumber> OrderItemSerialNumber { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductSalesChannel> ProductSalesChannel { get; set; }
    public DbSet<ProductStock> ProductStock { get; set; }
    public DbSet<SalesChannel> SalesChannel { get; set; }
    public DbSet<Setting> Setting { get; set; }
    public DbSet<Shipping> Shipping { get; set; }
    public DbSet<ShippingProvider> ShippingProvider { get; set; }
    public DbSet<ShippingProviderRate> ShippingProviderRate { get; set; }
    public DbSet<TaxClass> TaxClass { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>().Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        { 
            entry.Entity.DateModified = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}