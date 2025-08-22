using maERP.Domain.Entities;
using maERP.Domain.Entities.Common;
using maERP.Identity.Configurations;
using maERP.Persistence.Configurations;
using maERP.Persistence.Seeders;
using maERP.Application.Contracts.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.DatabaseContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantContext tenantContext) : IdentityDbContext<ApplicationUser>(options)
{
    private readonly ITenantContext _tenantContext = tenantContext;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(MaErpIdentityDbContext).Assembly);
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new TenantConfiguration());
        modelBuilder.ApplyConfiguration(new UserTenantConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
        modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
        modelBuilder.ApplyConfiguration(new SalesChannelConfiguration());
        modelBuilder.ApplyConfiguration(new TaxClassConfiguration());
        modelBuilder.ApplyConfiguration(new GoodsReceiptConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceItemConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new ProductSalesChannelConfiguration());
        modelBuilder.ApplyConfiguration(new ShippingProviderRateConfiguration());

        modelBuilder.SeedSettings();

        // Configure global query filters for multi-tenancy
        ConfigureGlobalFilters(modelBuilder);
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
    public DbSet<Manufacturer> Manufacturer { get; set; } = null!;
    public DbSet<GoodsReceipt> GoodsReceipt { get; set; } = null!;
    public DbSet<Tenant> Tenant { get; set; } = null!;
    public DbSet<UserTenant> UserTenant { get; set; } = null!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>().Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.DateModified = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.UtcNow;

                // Set TenantId for new entities if not already set
                if (entry.Entity.TenantId == null && _tenantContext.HasTenant())
                {
                    entry.Entity.TenantId = _tenantContext.GetCurrentTenantId();
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    private void ConfigureGlobalFilters(ModelBuilder modelBuilder)
    {
        // Apply global query filter for all entities that inherit from BaseEntity
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(ApplicationDbContext)
                    .GetMethod(nameof(GetQueryFilterExpression), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.MakeGenericMethod(entityType.ClrType);

                var filter = method?.Invoke(this, Array.Empty<object>());
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter((System.Linq.Expressions.LambdaExpression)filter!);
            }
        }

        // Special filter for UserTenant entity since it doesn't inherit from BaseEntity
        modelBuilder.Entity<UserTenant>().HasQueryFilter(ut =>
            _tenantContext.GetAssignedTenantIds().Contains(ut.TenantId) ||
            ut.TenantId == _tenantContext.GetCurrentTenantId());
    }

    private System.Linq.Expressions.Expression<System.Func<T, bool>> GetQueryFilterExpression<T>()
        where T : BaseEntity
    {
        // Data is accessible if:
        // 1. The entity is tenant-agnostic (TenantId is null) OR
        // 2. The entity's tenant matches the current active tenant OR
        // 3. The entity's tenant is one of the assigned tenants for the current user
        return entity =>
            entity.TenantId == null ||
            entity.TenantId == _tenantContext.GetCurrentTenantId() ||
            (_tenantContext.GetAssignedTenantIds().Count > 0 && _tenantContext.GetAssignedTenantIds().Contains(entity.TenantId.Value));
    }
}
