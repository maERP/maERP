using System.Globalization;
using maERP.Client.Core.Services.NameGeneration.WordLists;

namespace maERP.Client.Core.Services.NameGeneration;

/// <summary>
/// Generates realistic product names following the pattern:
/// [Optional Adjective] + [Category] + [Optional Variant/Model]
/// </summary>
public class ProductNameGenerator : INameGenerator
{
    private readonly IWordList _wordList;
    private readonly NameGeneratorOptions _options;
    private readonly Random _random;

    public ProductNameGenerator(NameGeneratorOptions? options = null)
    {
        _options = options ?? new NameGeneratorOptions();
        _wordList = ResolveWordList(_options.LanguageCode);
        _random = new Random();
    }

    private static IWordList ResolveWordList(string? languageCode)
    {
        var code = languageCode ?? CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        return code.ToLowerInvariant() switch
        {
            "de" => new GermanWordList(),
            _ => new EnglishWordList()
        };
    }

    public string Generate(ISet<string>? excludeNames = null)
    {
        const int maxAttempts = 100;
        for (var i = 0; i < maxAttempts; i++)
        {
            var name = GenerateInternal();
            if (excludeNames == null || !excludeNames.Contains(name))
            {
                return name;
            }
        }

        // Fallback: append random number
        return $"{GenerateInternal()} {_random.Next(1000, 9999)}";
    }

    public IReadOnlyList<string> GenerateMany(int count)
    {
        var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        while (names.Count < count)
        {
            names.Add(Generate(names));
        }
        return names.ToList();
    }

    private string GenerateInternal()
    {
        var parts = new List<string>();

        // Optional adjective
        if (_random.NextDouble() < _options.AdjectiveProbability)
        {
            parts.Add(GetRandomElement(_wordList.ProductAdjectives));
        }

        // Required category
        parts.Add(GetRandomElement(_wordList.ProductCategories));

        // Optional variant
        if (_random.NextDouble() < _options.VariantProbability)
        {
            parts.Add(GetRandomElement(_wordList.ProductVariants));
        }

        return string.Join(" ", parts);
    }

    private T GetRandomElement<T>(IReadOnlyList<T> list)
    {
        return list[_random.Next(list.Count)];
    }
}
