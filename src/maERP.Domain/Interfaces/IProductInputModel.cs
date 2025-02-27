namespace maERP.Domain.Interfaces;

public interface IProductInputModel
{
    string Sku { get; }
    string Name { get; }
    string NameOptimized { get; }
    string Ean { get; }
    string Asin { get; }
    string Description { get; }
    string DescriptionOptimized { get; }
    bool UseOptimized { get; }
    decimal Price { get; }
    decimal Msrp { get; }
    decimal Weight { get; }
    decimal Width { get; }
    decimal Height { get; }
    decimal Depth { get; }
    int TaxClassId { get; }
}
