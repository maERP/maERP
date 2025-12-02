using System.Globalization;
using maERP.Client.Core.Services.NameGeneration.WordLists;

namespace maERP.Client.Core.Services.NameGeneration;

/// <summary>
/// Generates realistic customer names following the pattern:
/// [FirstName] + [LastName]
/// </summary>
public class CustomerNameGenerator : INameGenerator
{
    private readonly IWordList _wordList;
    private readonly Random _random;

    public CustomerNameGenerator(NameGeneratorOptions? options = null)
    {
        var languageCode = options?.LanguageCode ?? CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        _wordList = ResolveWordList(languageCode);
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

        // Fallback: append initial
        var fallback = GenerateInternal();
        return $"{fallback} {(char)('A' + _random.Next(26))}.";
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
        var firstName = GetRandomElement(_wordList.FirstNames);
        var lastName = GetRandomElement(_wordList.LastNames);
        return $"{firstName} {lastName}";
    }

    private T GetRandomElement<T>(IReadOnlyList<T> list)
    {
        return list[_random.Next(list.Count)];
    }
}
