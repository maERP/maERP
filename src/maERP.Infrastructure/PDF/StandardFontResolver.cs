using System.IO;
using PdfSharp.Fonts;

namespace maERP.Infrastructure.PDF;

/// <summary>
/// Implements a font resolver for PDFsharp that uses the standard fonts.
/// </summary>
public class StandardFontResolver : IFontResolver
{
    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        // You can map any requested font to one of the standard PDF fonts
        // Here we're simplifying by always returning Helvetica
        string fontName = "Helvetica";

        if (isBold && isItalic)
            fontName = "Helvetica-BoldOblique";
        else if (isBold)
            fontName = "Helvetica-Bold";
        else if (isItalic)
            fontName = "Helvetica-Oblique";

        return new FontResolverInfo(fontName);
    }

    public byte[] GetFont(string faceName)
    {
        // PDFsharp includes the standard 14 PDF fonts
        // We just need to return a zero-length array as they don't need to be embedded
        return new byte[0];
    }
}
