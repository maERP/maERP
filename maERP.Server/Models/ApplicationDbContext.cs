#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using maERP.Server.Configurations.Seeds;
using maERP.Shared.Models;

namespace maERP.Server.Models;

public class ApplicationDbContext : IdentityDbContext<ApiUser>
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
        // modelBuilder.ApplyConfiguration(new ProductConfiguration());
        // modelBuilder.ApplyConfiguration(new SalesChannelConfiguration());
        // modelBuilder.ApplyConfiguration(new TaxClassConfiguration());

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

        // seed user data
        string DEFAULT_ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
        string DEFAULT_ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";

        var defaultAdminRole = new IdentityRole
        {
            Name = "Admin",
            NormalizedName = "ADMIN",
            Id = DEFAULT_ROLE_ID,
            ConcurrencyStamp = DEFAULT_ROLE_ID
        };

        var defaultUserRole = new IdentityRole
        {
            Name = "User",
            NormalizedName = "USER"
        };

        var defaultUser = new ApiUser
        {
            Id = DEFAULT_ADMIN_ID,
            FirstName = "Admin",
            LastName = "Admin",
            Email = "admin@localhost.com",
            UserName = "admin@localhost.com",
            NormalizedUserName = "ADMIN@LOCALHOST.COM",
            NormalizedEmail = "ADMIN@LOCALHOST.COM"
        };

        var password = new PasswordHasher<ApiUser>();
        var hashed = password.HashPassword(defaultUser, "maERP!12");
        defaultUser.PasswordHash = hashed;

        modelBuilder.Entity<IdentityRole>().HasData(defaultAdminRole);
        modelBuilder.Entity<IdentityRole>().HasData(defaultUserRole);
        modelBuilder.Entity<ApiUser>().HasData(defaultUser);

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = DEFAULT_ROLE_ID,
            UserId = DEFAULT_ADMIN_ID
        });
    }

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
    */
}