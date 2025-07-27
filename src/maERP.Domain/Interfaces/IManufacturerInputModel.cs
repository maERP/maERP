namespace maERP.Domain.Interfaces;

public interface IManufacturerInputModel
{
    string Name { get; }
    string Street { get; }
    string City { get; }
    string State { get; }
    string Country { get; }
    string ZipCode { get; }
    string Phone { get; }
    string Email { get; }
    string Website { get; }
    string Logo { get; }
}