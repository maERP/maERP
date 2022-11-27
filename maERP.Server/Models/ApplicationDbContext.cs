#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using maERP.Shared.Models;
using maERP.Server.Configurations.Seeds;
using Microsoft.AspNetCore.Identity;

namespace maERP.Server.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApiUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerAddress> CustomerAddresse { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductStock> ProductStock { get; set; }
        public DbSet<ProductSalesChannel> ProductSalesChannel { get; set; }
        public DbSet<SalesChannel> SalesChannel { get; set; }
        public DbSet<TaxClass> TaxClass { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new RoleConfiguration());
            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TaxClassConfiguration());
            modelBuilder.ApplyConfiguration(new SalesChannelConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            // modelBuilder.Entity<IdentityUser>().ToTable("user");
            modelBuilder.Entity<Customer>().ToTable("customer");
            modelBuilder.Entity<CustomerAddress>().ToTable("customer_address");
            modelBuilder.Entity<Product>().ToTable("product");
            modelBuilder.Entity<ProductStock>().ToTable("product_stock");
            modelBuilder.Entity<ProductSalesChannel>().ToTable("product_sales_channel");
            modelBuilder.Entity<SalesChannel>().ToTable("sales_channel");
            modelBuilder.Entity<TaxClass>().ToTable("tax_class");
            modelBuilder.Entity<Warehouse>().ToTable("warehouse");


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
    }
}