namespace maERP.Client.Core.Services.NameGeneration;

/// <summary>
/// Strategy interface for generating names.
/// Implementations provide different name generation strategies (products, customers, etc.).
/// </summary>
public interface INameGenerator
{
    /// <summary>
    /// Generates a unique name.
    /// </summary>
    /// <param name="excludeNames">Names to exclude from generation to avoid duplicates.</param>
    /// <returns>A generated name string.</returns>
    string Generate(ISet<string>? excludeNames = null);

    /// <summary>
    /// Generates multiple unique names.
    /// </summary>
    /// <param name="count">Number of names to generate.</param>
    /// <returns>Collection of unique generated names.</returns>
    IReadOnlyList<string> GenerateMany(int count);
}
