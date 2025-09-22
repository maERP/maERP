using System;
using System.Text.Json.Serialization;

namespace maERP.Domain.Dtos.Tenant;

public abstract class TenantDtoBase
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("tenantCode")]
    public string TenantCode { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }

    [JsonPropertyName("contactEmail")]
    public string? ContactEmail { get; set; }

    [JsonPropertyName("dateCreated")]
    public DateTime DateCreated { get; set; }

    [JsonPropertyName("dateModified")]
    public DateTime DateModified { get; set; }

    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    [JsonPropertyName("connectionString")]
    public string? ConnectionString { get; set; }

    [JsonPropertyName("adminEmail")]
    public string? AdminEmail { get; set; }

    [JsonPropertyName("validUpto")]
    public DateTime? ValidUntil { get; set; }

    [JsonIgnore]
    public string DisplayName => Name;

    [JsonIgnore]
    public string Identifier => string.IsNullOrWhiteSpace(TenantCode) ? Name : TenantCode;

    [JsonIgnore]
    public DateTime CreatedAt => DateCreated;

    [JsonIgnore]
    public DateTime LastUpdatedAt => DateModified;
}
