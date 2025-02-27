using maERP.Domain.Enums;

namespace maERP.Domain.Interfaces;

public interface ICustomerInputModel
{
    string Firstname { get; }
    string Lastname { get; }
    string CompanyName { get; }
    string Email { get; }
    string Phone { get; }
    string Website { get; }
    string VatNumber { get; }
    string Note { get; }
    CustomerStatus CustomerStatus { get; }
    DateTime DateEnrollment { get; }
}
