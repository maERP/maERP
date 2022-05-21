#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using maERP.Server.Data.Configurations;

namespace maERP.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApiUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<SalesChannel> SalesChannel { get; set; }
        public DbSet<TaxClass> TaxClass { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TaxClassConfiguration());
            modelBuilder.ApplyConfiguration(new SalesChannelConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            // modelBuilder.Entity<IdentityUser>().ToTable("user");
            modelBuilder.Entity<Customer>().ToTable("customer");
            modelBuilder.Entity<Product>().ToTable("product");
            modelBuilder.Entity<SalesChannel>().ToTable("sales_channel");
            modelBuilder.Entity<TaxClass>().ToTable("tax_class");    
        }
    }
}