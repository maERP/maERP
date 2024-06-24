using maERP.Domain.Entities;
using maERP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class AIModelConfiguration : IEntityTypeConfiguration<AIModel>
{
    public void Configure(EntityTypeBuilder<AIModel> modelBuilder)
    {
        modelBuilder.HasData(
            new AIModel()
            {
                Id = 1,
                AiModelType = AIModelType.ChatGPT4o,
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
