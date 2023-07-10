using System;
using maERP.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Server.Configurations.Seeds;

public class SettingsConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.HasData(
            new Setting { Id = 1, Key = "JwtSettings:Key", Value = "CHANGE_TO_YOUR_VERY_SECRET_JWT_SIGNING_KEY" },
            new Setting { Id = 2, Key = "JwtSettings:Issuer", Value = "maERP.Server" },
            new Setting { Id = 3, Key = "JwtSettings:Audience", Value = "maERP.Client" },
            new Setting { Id = 4, Key = "JwtSettings:DurationInMinutes", Value = "60" },

            new Setting { Id = 5, Key = "RemoteLog:Enabled", Value = "false" },
            new Setting { Id = 6, Key = "RemoteLog:Host", Value = "graylog.martin-andrich.de" },
            new Setting { Id = 7, Key = "RemoteLog:Port", Value = "12301" },
            new Setting { Id = 8, Key = "RemoteLog:TransportType", Value = "Tcp" },
            new Setting { Id = 9, Key = "RemoteLog:Facility", Value = "maERP.Server" }
        );
    }
}