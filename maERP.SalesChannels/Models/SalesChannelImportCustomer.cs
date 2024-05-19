using maERP.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace maERP.SalesChannels.Models;

public class SalesChannelImportCustomer
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string VatNumber { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public CustomerStatus CustomerStatus { get; set; }
    public DateTime DateEnrollment { get; set; }
}