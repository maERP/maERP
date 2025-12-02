namespace maERP.Client.Core.Services.NameGeneration;

/// <summary>
/// Configuration options for name generators.
/// </summary>
public class NameGeneratorOptions
{
    /// <summary>
    /// Language code for name generation ("de" or "en").
    /// If null, uses current UI culture.
    /// </summary>
    public string? LanguageCode { get; set; }

    /// <summary>
    /// Probability (0.0-1.0) of including optional adjective in product names.
    /// Default: 0.6
    /// </summary>
    public double AdjectiveProbability { get; set; } = 0.6;

    /// <summary>
    /// Probability (0.0-1.0) of including variant/model suffix in product names.
    /// Default: 0.5
    /// </summary>
    public double VariantProbability { get; set; } = 0.5;
}
