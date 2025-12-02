namespace maERP.Client.Core.Services.NameGeneration;

/// <summary>
/// Factory interface for creating name generators.
/// Provides a centralized way to create properly configured generators.
/// </summary>
public interface INameGeneratorFactory
{
    /// <summary>
    /// Creates a product name generator.
    /// </summary>
    /// <param name="options">Optional configuration options.</param>
    /// <returns>A configured product name generator.</returns>
    INameGenerator CreateProductGenerator(NameGeneratorOptions? options = null);

    /// <summary>
    /// Creates a customer name generator.
    /// </summary>
    /// <param name="options">Optional configuration options.</param>
    /// <returns>A configured customer name generator.</returns>
    INameGenerator CreateCustomerGenerator(NameGeneratorOptions? options = null);

    /// <summary>
    /// Creates an address generator.
    /// </summary>
    /// <param name="options">Optional configuration options.</param>
    /// <returns>A configured address generator.</returns>
    IAddressGenerator CreateAddressGenerator(NameGeneratorOptions? options = null);
}

/// <summary>
/// Default implementation of the name generator factory.
/// </summary>
public class NameGeneratorFactory : INameGeneratorFactory
{
    public INameGenerator CreateProductGenerator(NameGeneratorOptions? options = null)
    {
        return new ProductNameGenerator(options);
    }

    public INameGenerator CreateCustomerGenerator(NameGeneratorOptions? options = null)
    {
        return new CustomerNameGenerator(options);
    }

    public IAddressGenerator CreateAddressGenerator(NameGeneratorOptions? options = null)
    {
        return new AddressGenerator(options);
    }
}
