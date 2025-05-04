using System;
using System.IO;
using System.Collections.Generic;
using PdfSharp.Fonts;

namespace maERP.Infrastructure.PDF;

/// <summary>
/// Ein einfacher Font-Resolver für PDFsharp, der einige Standard-Schriftarten bereitstellt
/// </summary>
public class StandardFontResolver : IFontResolver
{
    // Dictionary zum Caching der Schriftdaten
    private static readonly Dictionary<string, byte[]> FontCache = new Dictionary<string, byte[]>();

    // Standard-Fallback-Schriftart
    private const string FallbackFontName = "Helvetica";

    /// <summary>
    /// Gibt Fontinfos für eine Schriftfamilie zurück
    /// </summary>
    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        // Sicherstellen, dass familyName nicht null ist
        string resolvedFamilyName = familyName ?? string.Empty;
        
        // Eindeutigen Schlüssel für den Font erstellen
        string faceName = GetFaceName(resolvedFamilyName, isBold, isItalic);
        
        // FontResolverInfo mit dem Face-Namen zurückgeben
        return new FontResolverInfo(faceName);
    }

    /// <summary>
    /// Gibt die Fontdaten für den angegebenen Schlüssel zurück
    /// </summary>
    public byte[] GetFont(string faceName)
    {
        // Prüfen, ob die Schriftart bereits im Cache ist
        if (FontCache.TryGetValue(faceName, out byte[]? fontData))
        {
            return fontData;
        }

        // Wenn nicht im Cache, verwende die Standard-14-Schriftarten von PDFsharp
        // Diese sind in PDFsharp eingebaut und müssen nicht als Ressource geladen werden
        // Das leere byte-Array teilt PDFsharp mit, dass es die eingebaute Schriftart verwenden soll
        fontData = Array.Empty<byte>();
        FontCache[faceName] = fontData;
        
        return fontData;
    }
    
    /// <summary>
    /// Hilfsmethode, um den eindeutigen Face-Namen für eine Schriftart zu generieren
    /// </summary>
    private string GetFaceName(string familyName, bool isBold, bool isItalic)
    {
        // Vereinfache den Font-Namen
        string fontName = familyName.ToLower().Trim();

        // Ersetze Windows-Schriftarten durch Äquivalente
        switch (fontName)
        {
            case "arial":
            case "helvetica":
                fontName = "Helvetica";
                break;
            case "times new roman":
            case "times":
                fontName = "Times";
                break;
            case "courier new":
            case "courier":
                fontName = "Courier";
                break;
            default:
                // Fallback auf Helvetica für nicht unterstützte Schriftarten
                fontName = FallbackFontName;
                break;
        }

        // Stil-Suffix hinzufügen
        string suffix = "";
        if (isBold && isItalic)
            suffix = "-BoldOblique";
        else if (isBold)
            suffix = "-Bold";
        else if (isItalic)
            suffix = "-Oblique";

        // Bei Courier und Times gibt es kein Oblique, sondern Italic
        if (fontName == "Times" && isItalic)
        {
            suffix = isBold ? "-BoldItalic" : "-Italic";
        }

        return $"{fontName}{suffix}";
    }
}

/// <summary>
/// Ein Dummy-FontResolver, der nur die eingebauten PDFsharp-Schriftarten unterstützt.
/// Vereinfachte Version, die immer auf Helvetica zurückfällt.
/// </summary>
public class SimpleFontResolver : IFontResolver
{
    // Grundlegende Implementation, um die Standard-14-PDF-Schriftarten zu unterstützen
    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        // Immer Fallback auf Helvetica, aber Stil berücksichtigen
        string faceName = "Helvetica";
        
        if (isBold && isItalic)
            faceName = "Helvetica-BoldOblique";
        else if (isBold)
            faceName = "Helvetica-Bold";
        else if (isItalic)
            faceName = "Helvetica-Oblique";
            
        return new FontResolverInfo(faceName);
    }

    public byte[] GetFont(string faceName)
    {
        // Leeres Array bedeutet: PDFsharp soll seine eingebauten Fonts verwenden
        return Array.Empty<byte>();
    }
} 