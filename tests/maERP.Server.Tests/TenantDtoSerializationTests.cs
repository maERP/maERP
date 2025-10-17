using System.Text.Json;
using maERP.Domain.Dtos.Tenant;
using Xunit;

namespace maERP.Server.Tests;

public class TenantDtoSerializationTests
{
    [Fact]
    public void TenantListDto_DeserializesExpectedProperties()
    {
        const string json = """
        {
            "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "name": "Main Tenant",
            "description": "Demomandant",
            "isActive": true,
            "contactEmail": "admin@example.com",
            "dateCreated": "2024-01-01T08:00:00Z",
            "dateModified": "2024-04-01T12:30:00Z",
            "domain": "main.maerp.local",
            "validUpto": "2026-05-01T10:15:00Z"
        }
        """;

        var dto = JsonSerializer.Deserialize<TenantListDto>(json);

        Assert.NotNull(dto);
        Assert.Equal(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), dto!.Id);
        Assert.Equal("Main Tenant", dto.Name);
        Assert.Equal("Demomandant", dto.Description);
        Assert.True(dto.IsActive);
        Assert.Equal("admin@example.com", dto.ContactEmail);
        Assert.Equal(DateTime.Parse("2024-01-01T08:00:00Z").ToUniversalTime(), dto.DateCreated.ToUniversalTime());
        Assert.Equal(DateTime.Parse("2024-04-01T12:30:00Z").ToUniversalTime(), dto.DateModified.ToUniversalTime());
        Assert.Equal("main.maerp.local", dto.Domain);
        Assert.Equal(DateTime.Parse("2026-05-01T10:15:00Z").ToUniversalTime(), dto.ValidUntil?.ToUniversalTime());
        Assert.Equal("Main Tenant", dto.DisplayName);
        Assert.Equal("Main Tenant", dto.Identifier);
    }

    [Fact]
    public void TenantDetailDto_DeserializesAdditionalProperties()
    {
        const string json = """
        {
            "id": "6f9619ff-8b86-d011-b42d-00cf4fc964ff",
            "name": "Secondary Tenant",
            "description": "VIP Tenant",
            "isActive": false,
            "contactEmail": "owner@secondary.com",
            "dateCreated": "2023-02-10T09:45:00Z",
            "dateModified": "2024-03-11T21:10:00Z",
            "userCount": 12,
            "connectionString": "Server=.;Database=Secondary;",
            "adminEmail": "owner@secondary.com",
            "domain": "secondary.maerp.local"
        }
        """;

        var dto = JsonSerializer.Deserialize<TenantDetailDto>(json);

        Assert.NotNull(dto);
        Assert.Equal(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc964ff"), dto!.Id);
        Assert.Equal("Secondary Tenant", dto.Name);
        Assert.Equal("VIP Tenant", dto.Description);
        Assert.False(dto.IsActive);
        Assert.Equal("owner@secondary.com", dto.ContactEmail);
        Assert.Equal("owner@secondary.com", dto.AdminEmail);
        Assert.Equal("Server=.;Database=Secondary;", dto.ConnectionString);
        Assert.Equal("secondary.maerp.local", dto.Domain);
        Assert.Equal(12, dto.UserCount);
    }
}
