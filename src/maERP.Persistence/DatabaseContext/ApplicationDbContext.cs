using maERP.Application.Services;
using maERP.Domain.Entities;
using maERP.Domain.Entities.Common;
using maERP.Identity.Configurations;
using maERP.Persistence.Configurations;
using maERP.Persistence.Seeders;
using maERP.Persistence.ValueConverters;
using maERP.Application.Contracts.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace maERP.Persistence.DatabaseContext;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly ITenantContext _tenantContext;
    private readonly ICredentialEncryptor _credentialEncryptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ITenantContext tenantContext,
        ICredentialEncryptor? credentialEncryptor = null)
        : base(options)
    {
        _tenantContext = tenantContext;
        _credentialEncryptor = credentialEncryptor ?? new NoOpCredentialEncryptor();
    }
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
        modelBuilder.Entity<Sales>().ToTable("sales");
        modelBuilder.Entity<SalesHistory>().ToTable("sales_history");
        modelBuilder.Entity<SalesItem>().ToTable("sales_item");
        modelBuilder.Entity<SalesItemSerialNumber>().ToTable("sales_item_serialnumber");
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
        modelBuilder.Entity<TenantEmailSettings>().ToTable("tenant_email_settings");
        modelBuilder.Entity<UserTenant>().ToTable("user_tenant");
        modelBuilder.Entity<Warehouse>().ToTable("warehouse");
        modelBuilder.Entity<ChannelSyncRun>().ToTable("channel_sync_run");
        modelBuilder.Entity<ChannelExportOutbox>().ToTable("channel_export_outbox");
        modelBuilder.Entity<TenantOAuthAppSettings>().ToTable("tenant_oauth_app_settings");
        modelBuilder.Entity<OAuthState>().ToTable("oauth_state");

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

        // Wire up at-rest encryption for SalesChannel credentials. Converter goes through
        // the injected ICredentialEncryptor (NoOp at design-time / in tests, DataProtection
        // in production).
        var encryptedConverter = new EncryptedStringConverter(_credentialEncryptor);
        modelBuilder.Entity<SalesChannel>().Property(e => e.Password).HasConversion(encryptedConverter);
        modelBuilder.Entity<SalesChannel>().Property(e => e.AccessToken).HasConversion(encryptedConverter!);
        modelBuilder.Entity<SalesChannel>().Property(e => e.RefreshToken).HasConversion(encryptedConverter!);
        // OAuth Developer-App ClientSecret encrypted at rest with the same key ring.
        modelBuilder.Entity<TenantOAuthAppSettings>().Property(e => e.ClientSecret).HasConversion(encryptedConverter!);
        modelBuilder.ApplyConfiguration(new TaxClassConfiguration());
        modelBuilder.ApplyConfiguration(new GoodsReceiptConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
        modelBuilder.ApplyConfiguration(new SalesConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceItemConfiguration());
        modelBuilder.ApplyConfiguration(new SalesItemConfiguration());
        modelBuilder.ApplyConfiguration(new ProductSalesChannelConfiguration());
        modelBuilder.ApplyConfiguration(new ShippingProviderRateConfiguration());
        modelBuilder.ApplyConfiguration(new TenantEmailSettingsConfiguration());
        modelBuilder.ApplyConfiguration(new ChannelSyncRunConfiguration());
        modelBuilder.ApplyConfiguration(new ChannelExportOutboxConfiguration());
        modelBuilder.ApplyConfiguration(new TenantOAuthAppSettingsConfiguration());
        modelBuilder.ApplyConfiguration(new OAuthStateConfiguration());

        modelBuilder.SeedSettings();

        // SQLite cannot ORDER BY / compare DateTimeOffset values when stored as TEXT.
        // Apply DateTimeOffsetToBinaryConverter on the SQLite provider only, so the
        // values are persisted as long (binary-encoded ticks + offset) and become
        // sortable. MSSQL/PostgreSQL keep their native DateTimeOffset column types.
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            var dtoConverter = new DateTimeOffsetToBinaryConverter();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.ClrType.GetProperties())
                {
                    if (property.PropertyType == typeof(DateTimeOffset)
                        || property.PropertyType == typeof(DateTimeOffset?))
                    {
                        modelBuilder.Entity(entityType.ClrType)
                            .Property(property.Name)
                            .HasConversion(dtoConverter);
                    }
                }
            }
        }

        // Configure global query filters for multi-tenancy
        // IMPORTANT: Always apply global filters to ensure tenant isolation, but not in tests
        // Tests use manual filtering in repositories for better control
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        if (environment != "Testing")
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
    public DbSet<Sales> Sales { get; set; } = null!;
    public DbSet<SalesHistory> SalesHistory { get; set; } = null!;
    public DbSet<SalesItem> SalesItem { get; set; } = null!;
    public DbSet<SalesItemSerialNumber> SalesItemSerialNumber { get; set; } = null!;
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
    public DbSet<TenantEmailSettings> TenantEmailSettings { get; set; } = null!;
    public DbSet<UserTenant> UserTenant { get; set; } = null!;
    public DbSet<ChannelSyncRun> ChannelSyncRun { get; set; } = null!;
    public DbSet<ChannelExportOutbox> ChannelExportOutbox { get; set; } = null!;
    public DbSet<TenantOAuthAppSettings> TenantOAuthAppSettings { get; set; } = null!;
    public DbSet<OAuthState> OAuthState { get; set; } = null!;

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

            // Normalize Customer.Email to a canonical lowercase form so the per-tenant unique index
            // matches regardless of case/whitespace at the call site.
            if (entry.Entity is Customer customer && !string.IsNullOrEmpty(customer.Email))
            {
                customer.Email = customer.Email.Trim().ToLowerInvariant();
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
