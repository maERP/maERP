using System.ComponentModel.DataAnnotations;
using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

public class Customer : BaseEntity, IBaseEntity
{
    public int CustomerId { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string VatNumber { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;

    [Required]
    public CustomerStatus CustomerStatus { get; set; }

    public ICollection<CustomerAddress>? CustomerAddresses { get; set; }
    public ICollection<CustomerSalesChannel>? CustomerSalesChannels { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public DateTimeOffset DateEnrollment { get; set; }
}