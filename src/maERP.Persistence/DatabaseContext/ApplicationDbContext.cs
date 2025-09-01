using maERP.Domain.Entities;
using maERP.Domain.Entities.Common;
using maERP.Identity.Configurations;
using maERP.Persistence.Configurations;
using maERP.Persistence.Seeders;
using maERP.Application.Contracts.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.DatabaseContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantContext tenantContext) : IdentityDbContext<ApplicationUser>(options)
{
    private readonly ITenantContext _tenantContext = tenantContext;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Map ASP.NET Identity tables to custom names
        modelBuilder.Entity<ApplicationUser>().ToTable("user");
        modelBuilder.Entity<IdentityRole>().ToTable("role");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("role_claim");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("user_claim");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("user_login");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("user_role");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("user_token");

        modelBuilder.Entity<AiModel>().ToTable("ai_model");
        modelBuilder.Entity<AiPrompt>().ToTable("ai_prompt");
        modelBuilder.Entity<Country>().ToTable("country");
        modelBuilder.Entity<Customer>().ToTable("customer");
        modelBuilder.Entity<CustomerAddress>().ToTable("customer_address");
        modelBuilder.Entity<CustomerSalesChannel>().ToTable("customer_saleschannel");
        modelBuilder.Entity<GoodsReceipt>().ToTable("goods_receipt");
        modelBuilder.Entity<Invoice>().ToTable("invoice");
        modelBuilder.Entity<InvoiceItem>().ToTable("invoice_item");
        modelBuilder.Entity<Manufacturer>().ToTable("manufacturer");
        modelBuilder.Entity<Order>().ToTable("order");
        modelBuilder.Entity<OrderHistory>().ToTable("order_history");
        modelBuilder.Entity<OrderItem>().ToTable("order_item");
        modelBuilder.Entity<OrderItemSerialNumber>().ToTable("order_item_serialnumber");
        modelBuilder.Entity<Product>().ToTable("product");
        modelBuilder.Entity<ProductSalesChannel>().ToTable("product_saleschannel");
        modelBuilder.Entity<ProductStock>().ToTable("product_stock");
        modelBuilder.Entity<SalesChannel>().ToTable("saleschannel");
        modelBuilder.Entity<Setting>().ToTable("setting");
        modelBuilder.Entity<Shipping>().ToTable("shipping");
        modelBuilder.Entity<ShippingProvider>().ToTable("shipping_provider");
        modelBuilder.Entity<ShippingProviderRate>().ToTable("shipping_provider_rate");
        modelBuilder.Entity<TaxClass>().ToTable("tax_class");
        modelBuilder.Entity<Tenant>().ToTable("tenant");
        modelBuilder.Entity<UserTenant>().ToTable("user_tenant");
        modelBuilder.Entity<Warehouse>().ToTable("warehouse");

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

        // Configure global query filters for multi-tenancy (disabled in testing environment)
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Testing")
        {
            ConfigureGlobalFilters(modelBuilder);
        }
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
                if (entry.Entity.TenantId == null)
                {
                    var currentTenantId = _tenantContext.GetCurrentTenantId();
                    if (currentTenantId.HasValue)
                    {
                        entry.Entity.TenantId = currentTenantId.Value;
                    }
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
        // SECURITY: Data is accessible ONLY if:
        // 1. The entity is tenant-agnostic (TenantId is null) OR
        // 2. The entity's tenant matches the current active tenant
        // NOTE: Removed access to all assigned tenants for security - only current tenant allowed
        return entity =>
            entity.TenantId == null ||
            entity.TenantId == _tenantContext.GetCurrentTenantId();
    }
}
