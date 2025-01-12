using maERP.Domain.Entities;
using maERP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class AiModelConfiguration : IEntityTypeConfiguration<AiModel>
{
    public void Configure(EntityTypeBuilder<AiModel> modelBuilder)
    {
        modelBuilder.HasData(
            new AiModel()
            {
                Id = 1,
                AiModelType = AiModelType.ChatGPT4o,
                Name = "ChatGPT 4o Demo",
                ApiUsername = "demo",
                ApiKey = "demo",
                ApiPassword = "demo"
            }
        );

        modelBuilder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
