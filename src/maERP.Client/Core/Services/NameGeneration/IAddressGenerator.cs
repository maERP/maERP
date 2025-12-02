namespace maERP.Client.Core.Services.NameGeneration;

/// <summary>
/// Strategy interface for generating addresses.
/// Provides full address generation including name, street, zip, and city.
/// </summary>
public interface IAddressGenerator
{
    /// <summary>
    /// Generates a unique address.
    /// </summary>
    /// <returns>A generated address with all components.</returns>
    GeneratedAddress Generate();

    /// <summary>
    /// Generates multiple unique addresses.
    /// </summary>
    /// <param name="count">Number of addresses to generate.</param>
    /// <returns>Collection of unique generated addresses.</returns>
    IReadOnlyList<GeneratedAddress> GenerateMany(int count);
}
