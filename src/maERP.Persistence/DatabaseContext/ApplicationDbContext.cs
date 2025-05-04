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

    public DbSet<AiModel> AiModel { get; set; } = null!;
    public DbSet<AiPrompt> AiPrompt { get; set; } = null!;
    public DbSet<Country> Country { get; set; } = null!;
    public DbSet<Customer> Customer { get; set; } = null!;
    public DbSet<CustomerAddress> CustomerAddress { get; set; } = null!;
    public DbSet<CustomerSalesChannel> CustomerSalesChannel { get; set; } = null!;
    public DbSet<Invoice> Invoice { get; set; } = null!;
    public DbSet<InvoiceItem> InvoiceItem { get; set; } = null!;
    public DbSet<Order> Order { get; set; } = null!;
    public DbSet<OrderHistory> OrderHistory { get; set; } = null!;
    public DbSet<OrderItem> OrderItem { get; set; } = null!;
    public DbSet<OrderItemSerialNumber> OrderItemSerialNumber { get; set; } = null!;
    public DbSet<Product> Product { get; set; } = null!;
    public DbSet<ProductSalesChannel> ProductSalesChannel { get; set; } = null!;
    public DbSet<ProductStock> ProductStock { get; set; } = null!;
    public DbSet<SalesChannel> SalesChannel { get; set; } = null!;
    public DbSet<Setting> Setting { get; set; } = null!;
    public DbSet<Shipping> Shipping { get; set; } = null!;
    public DbSet<ShippingProvider> ShippingProvider { get; set; } = null!;
    public DbSet<ShippingProviderRate> ShippingProviderRate { get; set; } = null!;
    public DbSet<TaxClass> TaxClass { get; set; } = null!;
    public DbSet<Warehouse> Warehouse { get; set; } = null!;

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
