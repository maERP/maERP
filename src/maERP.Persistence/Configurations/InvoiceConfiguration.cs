using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.Property(e => e.Subtotal)
            .HasPrecision(18, 2);

        builder.Property(e => e.ShippingCost)
            .HasPrecision(18, 2);

        builder.Property(e => e.TotalTax)
            .HasPrecision(18, 2);

        builder.Property(e => e.Total)
            .HasPrecision(18, 2);

        // Configure CustomerId as int
        builder.Property(e => e.CustomerId)
            .HasColumnType("int");

        // Configure the relationship with Customer entity
        builder.HasOne(e => e.Customer)
            .WithMany()
            .HasForeignKey(e => e.CustomerId)
            .HasPrincipalKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}