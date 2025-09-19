using PdfSharp.Fonts;
using System.IO;
using System.Reflection;

namespace maERP.Infrastructure.PDF;

public class CustomFontResolver : IFontResolver
{
    private const string DefaultFontFamily = "Open Sans";
    private const string RegularFontKey = "OpenSans#Regular";

    private static readonly Dictionary<string, string> FontMappings = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Courier New", DefaultFontFamily },
        { "Times New Roman", DefaultFontFamily },
        { "Helvetica", DefaultFontFamily },
        { "Times", DefaultFontFamily },
        { "Arial", DefaultFontFamily },
        { DefaultFontFamily, DefaultFontFamily }
    };

    private static readonly Lazy<byte[]> OpenSansRegular = new(() => LoadFontResource("maERP.Infrastructure.PDF.Fonts.OpenSans-Regular.ttf"));

    public byte[]? GetFont(string faceName)
    {
        return faceName switch
        {
            RegularFontKey => OpenSansRegular.Value,
            _ => null
        };
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        // Always resolve to our embedded Open Sans variant and simulate styling where required
        // Keep dictionary lookup for completeness; currently all map to Open Sans
        _ = FontMappings.TryGetValue(familyName, out _);

        return new FontResolverInfo(RegularFontKey);
    }

    private static byte[] LoadFontResource(string resourceName)
    {
        var assembly = typeof(CustomFontResolver).GetTypeInfo().Assembly;
        using var stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Embedded font resource '{resourceName}' not found.");

        using var ms = new MemoryStream();
        stream.CopyTo(ms);
        return ms.ToArray();
    }
}
