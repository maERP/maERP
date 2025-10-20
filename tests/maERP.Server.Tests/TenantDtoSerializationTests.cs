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
            "companyName": "ACME Corporation",
            "contactEmail": "admin@example.com",
            "phone": "+49 123 456789",
            "website": "https://www.example.com",
            "street": "Main Street 123",
            "street2": "Building A",
            "postalCode": "12345",
            "city": "Berlin",
            "state": "Berlin",
            "country": "Germany",
            "iban": "DE89370400440532013000",
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
        Assert.Equal("ACME Corporation", dto.CompanyName);
        Assert.Equal("admin@example.com", dto.ContactEmail);
        Assert.Equal("+49 123 456789", dto.Phone);
        Assert.Equal("https://www.example.com", dto.Website);
        Assert.Equal("Main Street 123", dto.Street);
        Assert.Equal("Building A", dto.Street2);
        Assert.Equal("12345", dto.PostalCode);
        Assert.Equal("Berlin", dto.City);
        Assert.Equal("Berlin", dto.State);
        Assert.Equal("Germany", dto.Country);
        Assert.Equal("DE89370400440532013000", dto.Iban);
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
            "companyName": "Secondary Ltd.",
            "contactEmail": "owner@secondary.com",
            "phone": "+44 20 1234 5678",
            "website": "https://www.secondary.com",
            "street": "Oxford Street 456",
            "street2": null,
            "postalCode": "W1D 1BS",
            "city": "London",
            "state": "England",
            "country": "United Kingdom",
            "iban": "GB82WEST12345698765432",
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
        Assert.Equal("Secondary Ltd.", dto.CompanyName);
        Assert.Equal("owner@secondary.com", dto.ContactEmail);
        Assert.Equal("+44 20 1234 5678", dto.Phone);
        Assert.Equal("https://www.secondary.com", dto.Website);
        Assert.Equal("Oxford Street 456", dto.Street);
        Assert.Null(dto.Street2);
        Assert.Equal("W1D 1BS", dto.PostalCode);
        Assert.Equal("London", dto.City);
        Assert.Equal("England", dto.State);
        Assert.Equal("United Kingdom", dto.Country);
        Assert.Equal("GB82WEST12345698765432", dto.Iban);
        Assert.Equal("owner@secondary.com", dto.AdminEmail);
        Assert.Equal("Server=.;Database=Secondary;", dto.ConnectionString);
        Assert.Equal("secondary.maerp.local", dto.Domain);
        Assert.Equal(12, dto.UserCount);
    }
}
