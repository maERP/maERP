namespace maERP.Domain.Dtos.Customer;

/// <summary>
/// Result of a single customer-dedupe run (dry or live). One <see cref="CustomerDedupeGroupDto"/>
/// per (TenantId, normalized-email) collision detected.
/// </summary>
public class CustomerDedupeResultDto
{
    public bool DryRun { get; set; }
    public int TenantsAffected { get; set; }
    public int GroupsFound { get; set; }
    public int CustomersToMerge { get; set; }
    public int SalessReassigned { get; set; }
    public int AddressesReassigned { get; set; }
    public int SalesChannelLinksReassigned { get; set; }
    public List<CustomerDedupeGroupDto> Groups { get; set; } = new();
}

public class CustomerDedupeGroupDto
{
    public Guid TenantId { get; set; }
    public string EmailKey { get; set; } = string.Empty;
    public Guid SurvivorCustomerId { get; set; }
    public List<Guid> SiblingCustomerIds { get; set; } = new();
    public int SalessReassigned { get; set; }
    public int AddressesReassigned { get; set; }
    public int SalesChannelLinksReassigned { get; set; }
}
