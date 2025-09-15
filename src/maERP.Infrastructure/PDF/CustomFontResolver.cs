using PdfSharp.Fonts;

namespace maERP.Infrastructure.PDF;

public class CustomFontResolver : IFontResolver
{
    private static readonly Dictionary<string, string> FontMappings = new()
    {
        { "Courier New", "Arial" },
        { "Times New Roman", "Arial" },
        { "Helvetica", "Arial" },
        { "Times", "Arial" }
    };

    public byte[]? GetFont(string faceName)
    {
        // Keine embedded Fonts verwenden - lass PdfSharp System-Fonts verwenden
        return null;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        // Verwende Arial als Fallback für alle problematischen Fonts
        var resolvedName = FontMappings.ContainsKey(familyName) ? FontMappings[familyName] : familyName;

        // Erstelle einen eindeutigen Key für diese Font-Kombination
        var key = $"{resolvedName}_{isBold}_{isItalic}";

        return new FontResolverInfo(key);
    }
}