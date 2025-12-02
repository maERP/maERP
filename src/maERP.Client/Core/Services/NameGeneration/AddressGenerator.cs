using System.Globalization;
using maERP.Client.Core.Services.NameGeneration.WordLists;

namespace maERP.Client.Core.Services.NameGeneration;

/// <summary>
/// Generates realistic addresses with all components (name, street, zip, city).
/// </summary>
public class AddressGenerator : IAddressGenerator
{
    private readonly IWordList _wordList;
    private readonly Random _random;

    public AddressGenerator(NameGeneratorOptions? options = null)
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

    public GeneratedAddress Generate()
    {
        var firstName = GetRandomElement(_wordList.FirstNames);
        var lastName = GetRandomElement(_wordList.LastNames);
        var street = GetRandomElement(_wordList.Streets);
        var houseNr = GenerateHouseNumber();
        var zip = GetRandomElement(_wordList.ZipCodes);
        var city = GetRandomElement(_wordList.Cities);

        return new GeneratedAddress(firstName, lastName, street, houseNr, zip, city);
    }

    public IReadOnlyList<GeneratedAddress> GenerateMany(int count)
    {
        var addresses = new List<GeneratedAddress>(count);
        var usedCombinations = new HashSet<string>();

        while (addresses.Count < count)
        {
            var address = Generate();
            var key = $"{address.Street}|{address.HouseNr}|{address.Zip}";

            if (!usedCombinations.Contains(key))
            {
                usedCombinations.Add(key);
                addresses.Add(address);
            }
        }

        return addresses;
    }

    private string GenerateHouseNumber()
    {
        var number = _random.Next(1, 200);

        // 20% chance of having a letter suffix (e.g., 12a, 12b)
        if (_random.NextDouble() < 0.2)
        {
            var suffix = (char)('a' + _random.Next(3)); // a, b, or c
            return $"{number}{suffix}";
        }

        return number.ToString();
    }

    private T GetRandomElement<T>(IReadOnlyList<T> list)
    {
        return list[_random.Next(list.Count)];
    }
}
