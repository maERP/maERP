using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class GoodsReceiptConfiguration : IEntityTypeConfiguration<GoodsReceipt>
{
    public void Configure(EntityTypeBuilder<GoodsReceipt> builder)
    {
        builder.HasKey(gr => gr.Id);

        builder.Property(gr => gr.ReceiptDate)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(gr => gr.Quantity)
            .IsRequired();

        builder.Property(gr => gr.Supplier)
            .HasMaxLength(255);

        builder.Property(gr => gr.Notes)
            .HasMaxLength(1000);

        builder.Property(gr => gr.CreatedBy)
            .HasMaxLength(100)
            .IsRequired();

        // Relationships
        builder.HasOne(gr => gr.Product)
            .WithMany()
            .HasForeignKey(gr => gr.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(gr => gr.Warehouse)
            .WithMany()
            .HasForeignKey(gr => gr.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(gr => gr.ReceiptDate);
        builder.HasIndex(gr => gr.ProductId);
        builder.HasIndex(gr => gr.WarehouseId);
        builder.HasIndex(gr => gr.DateCreated);
    }
}