using maERP.Domain.Entities;
using maERP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class AiModelConfiguration : IEntityTypeConfiguration<AiModel>
{
    public void Configure(EntityTypeBuilder<AiModel> builder)
    {
        builder.HasData(
            new AiModel
            {
                Id = new Guid("44444444-4444-4444-4444-444444444444"),
                TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // Demo tenant ID
                AiModelType = AiModelType.ChatGpt4O,
                Name = "ChatGPT 4o Demo",
                ApiUsername = "demo",
                ApiKey = "demo",
                ApiPassword = "demo"
            }
        );

        builder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
