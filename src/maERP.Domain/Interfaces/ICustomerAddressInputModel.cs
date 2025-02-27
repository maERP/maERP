namespace maERP.Domain.Interfaces;

public interface ICustomerAddressInputModel
{
    string Firstname { get; }
    string Lastname { get; }
    string CompanyName { get; }
    string Street { get; }
    string HouseNr { get; }
    string Zip { get; }
    string City { get; }
    bool DefaultDeliveryAddress { get; }
    bool DefaultInvoiceAddress { get; }
    int CountryId { get; }
    int CustomerId { get; }
}
