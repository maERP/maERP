#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using maERP.Server.Configurations.Seeds;
using maERP.Shared.Models;
using maERP.Shared.Dtos.User;

namespace maERP.Server.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new SettingsConfiguration());
        // modelBuilder.ApplyConfiguration(new ProductConfiguration());
        // modelBuilder.ApplyConfiguration(new SalesChannelConfiguration());
        // modelBuilder.ApplyConfiguration(new TaxClassConfiguration());

        /*
        modelBuilder.Entity<Country>();
        modelBuilder.Entity<Customer>();
        modelBuilder.Entity<CustomerAddress>();
        modelBuilder.Entity<Order>();

        modelBuilder.Entity<Product>()
            .HasMany(e => e.ProductStock);

        modelBuilder.Entity<Product>()
            .HasMany(e => e.ProductSalesChannel);

        modelBuilder.Entity<Product>()
            .HasOne(e => e.TaxClass);

        modelBuilder.Entity<ProductSalesChannel>()
            .HasOne(e => e.Product)
            .WithMany(e => e.ProductSalesChannel)
            .HasForeignKey(e => e.ProductId)
            .IsRequired();

        modelBuilder.Entity<ProductStock>()
            .HasOne(e => e.Product)
            .WithMany(e => e.ProductStock)
            .HasForeignKey(e => e.ProductId)
            .IsRequired();

        modelBuilder.Entity<ProductStock>()
            .HasOne(e => e.Warehouse);

        modelBuilder.Entity<SalesChannel>();
        modelBuilder.Entity<ShippingProvider>();
        modelBuilder.Entity<ShippingProviderRate>();
        modelBuilder.Entity<TaxClass>();

        modelBuilder.Entity<Warehouse>();
        */
        
        // seed user data
        string DEFAULT_ADMIN_USER_ID = "05474ea3–7543-8aef-bcae-33e812c35fc3";
        string DEFAULT_ADMIN_ROLE_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
        string DEFAULT_USER_ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk21ab1";

        var defaultAdminRole = new IdentityRole
        {
            Id = DEFAULT_ADMIN_ROLE_ID,
            Name = "Admin",
            NormalizedName = "ADMIN"
        };

        var defaultUserRole = new IdentityRole
        {
            Id = DEFAULT_USER_ROLE_ID,
            Name = "User",
            NormalizedName = "USER"
        };

        var hasher = new PasswordHasher<ApplicationUser>();
        
        var defaultAdmin = new ApplicationUser
        {
            Id = DEFAULT_ADMIN_USER_ID,
            UserName = "admin@localhost.com",
            FirstName = "Admin",
            LastName = "Admin",
            Email = "admin@localhost.com",
            NormalizedUserName = "ADMIN@LOCALHOST.COM",
            NormalizedEmail = "ADMIN@LOCALHOST.COM",
            PasswordHash = hasher.HashPassword(null, "maERP!12")
        };
        
        modelBuilder.Entity<IdentityRole>().HasData(defaultAdminRole);
        modelBuilder.Entity<IdentityRole>().HasData(defaultUserRole);
        modelBuilder.Entity<ApplicationUser>().HasData(defaultAdmin);

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = DEFAULT_ADMIN_ROLE_ID,
            UserId = DEFAULT_ADMIN_USER_ID
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<ABaseModel>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.DateModified = DateTime.UtcNow;
            // entry.Entity.ModifiedBy = _userService.UserId;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.UtcNow;
                // entry.Entity.CreatedBy = _userService.UserId;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}