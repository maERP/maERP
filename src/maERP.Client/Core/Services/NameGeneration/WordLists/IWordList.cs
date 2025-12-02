namespace maERP.Client.Core.Services.NameGeneration.WordLists;

/// <summary>
/// Interface for language-specific word lists used in name generation.
/// </summary>
public interface IWordList
{
    /// <summary>
    /// Gets the supported language code (e.g., "de", "en").
    /// </summary>
    string LanguageCode { get; }

    /// <summary>
    /// Gets adjectives for product names (e.g., "Premium", "Ergonomic").
    /// </summary>
    IReadOnlyList<string> ProductAdjectives { get; }

    /// <summary>
    /// Gets product category names (e.g., "Laptop", "Office Chair").
    /// </summary>
    IReadOnlyList<string> ProductCategories { get; }

    /// <summary>
    /// Gets product variant/model suffixes (e.g., "Pro", "4K", "2024").
    /// </summary>
    IReadOnlyList<string> ProductVariants { get; }

    /// <summary>
    /// Gets first names for customer generation.
    /// </summary>
    IReadOnlyList<string> FirstNames { get; }

    /// <summary>
    /// Gets last names for customer generation.
    /// </summary>
    IReadOnlyList<string> LastNames { get; }

    /// <summary>
    /// Gets street names for address generation.
    /// </summary>
    IReadOnlyList<string> Streets { get; }

    /// <summary>
    /// Gets city names for address generation.
    /// </summary>
    IReadOnlyList<string> Cities { get; }

    /// <summary>
    /// Gets zip/postal codes for address generation.
    /// </summary>
    IReadOnlyList<string> ZipCodes { get; }
}
