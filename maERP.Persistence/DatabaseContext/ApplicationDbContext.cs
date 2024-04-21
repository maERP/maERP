using maERP.Domain.Models;
using maERP.Domain.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.DatabaseContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Country> Country { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<CustomerAddress> CustomerAddress { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductSalesChannel> ProductSalesChannel { get; set; }
    public DbSet<ProductStock> ProductStock { get; set; }
    public DbSet<SalesChannel> SalesChannel { get; set; }
    public DbSet<ShippingProvider> ShippingProvider { get; set; }
    public DbSet<ShippingProviderRate> ShippingProviderRate { get; set; }
    public DbSet<TaxClass> TaxClass { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }

    /* protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        // modelBuilder.ApplyConfiguration(new CountryConfiguration());
    } */

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