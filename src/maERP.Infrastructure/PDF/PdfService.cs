using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Globalization;
using System.IO;
using PdfSharp.Fonts;
using System;
using System.Linq;
using System.Collections.Generic;

namespace maERP.Infrastructure.PDF;

public class PdfService : IPdfService
{
    private readonly ISettingRepository _settingRepository;
    private string _companyName = string.Empty;
    private string _companyAddress = string.Empty;
    private string _companyZipCity = string.Empty;
    private string _companyCountry = string.Empty;
    private string _companyPhone = string.Empty;
    private string _companyEmail = string.Empty;
    private string _companyWebsite = string.Empty;
    private string _companyTaxId = string.Empty;
    private string _companyVatId = string.Empty;
    private string _companyBankName = string.Empty;
    private string _companyIban = string.Empty;
    private string _companyBic = string.Empty;
    private string _logoPath = string.Empty;

    public PdfService(ISettingRepository settingRepository)
    {
        // Den PDFsharp FontResolver initialisieren, falls er noch nicht gesetzt ist
        if (GlobalFontSettings.FontResolver == null)
        {
            GlobalFontSettings.FontResolver = new StandardFontResolver();
        }

        // Explizit den FontCache aktivieren um Null-Referenzen zu verhindern
        PdfSharp.Fonts.GlobalFontSettings.FontResolver = new StandardFontResolver();

        _settingRepository = settingRepository;
        LoadCompanySettings();
    }

    private void LoadCompanySettings()
    {
        var settings = _settingRepository.GetAllAsync().Result;

        if (settings != null)
        {
            _companyName = GetSettingValue(settings, "Company.Name");
            _companyAddress = GetSettingValue(settings, "Company.Address");
            _companyZipCity = GetSettingValue(settings, "Company.ZipCity");
            _companyCountry = GetSettingValue(settings, "Company.Country");
            _companyPhone = GetSettingValue(settings, "Company.Phone");
            _companyEmail = GetSettingValue(settings, "Company.Email");
            _companyWebsite = GetSettingValue(settings, "Company.Website");
            _companyTaxId = GetSettingValue(settings, "Company.TaxId");
            _companyVatId = GetSettingValue(settings, "Company.VatId");
            _companyBankName = GetSettingValue(settings, "Company.BankName");
            _companyIban = GetSettingValue(settings, "Company.Iban");
            _companyBic = GetSettingValue(settings, "Company.Bic");
            _logoPath = GetSettingValue(settings, "Company.LogoPath");
        }
    }

    private string GetSettingValue(IEnumerable<Setting> settings, string key)
    {
        return settings.FirstOrDefault(s => s.Key == key)?.Value ?? string.Empty;
    }

    /// <summary>
    /// Generates a PDF invoice from the given Invoice entity
    /// </summary>
    /// <param name="invoice">The invoice entity to generate PDF for</param>
    /// <param name="outputPath">Optional path to save the PDF file. If null, returns the PDF as a byte array</param>
    /// <returns>Byte array containing the PDF if outputPath is null, otherwise returns null after saving to file</returns>
    public byte[]? GenerateInvoice(Invoice invoice, string? outputPath = null)
    {
        if (invoice == null)
            throw new ArgumentNullException(nameof(invoice));

        try
        {
            // Überprüfen, ob der FontResolver ordnungsgemäß initialisiert ist
            if (GlobalFontSettings.FontResolver == null)
            {
                GlobalFontSettings.FontResolver = new StandardFontResolver();
            }

            // MigraDoc Dokumentobjekt erstellen
            var document = CreateInvoiceDocument(invoice);

            // PdfDocument vor der Übergabe explizit initialisieren
            var pdfDoc = new PdfDocument();

            // Rendern des Dokuments zu PDF
            var pdfRenderer = new PdfDocumentRenderer
            {
                Document = document,
                PdfDocument = pdfDoc
            };

            // Dokument rendern
            pdfRenderer.RenderDocument();

            // PDF speichern oder als Byte-Array zurückgeben
            if (string.IsNullOrEmpty(outputPath))
            {
                using var stream = new MemoryStream();
                pdfRenderer.PdfDocument.Save(stream, false);
                return stream.ToArray();
            }

            pdfRenderer.PdfDocument.Save(outputPath);
            return null;
        }
        catch (Exception ex)
        {
            // Im Fehlerfall detaillierte Informationen loggen
            System.Diagnostics.Debug.WriteLine($"PDF-Generierung fehlgeschlagen: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
            throw; // Weitergeben der Exception für höhere Handler
        }
    }

    private Document CreateInvoiceDocument(Invoice invoice)
    {
        // Sicherstellen, dass die richtige Kodierung für deutsche Zeichen verwendet wird
        Document document = new Document();

        // Stil für den gesamten Text definieren
        var style = document.Styles["Normal"];
        style!.Font.Name = "Helvetica"; // Standard-PDF-Schriftart verwenden
        style.Font.Size = 10;

        // Titel-Stil definieren
        var titleStyle = document.Styles.AddStyle("Title", "Normal");
        titleStyle.Font.Bold = true;
        titleStyle.Font.Size = 16;

        // Überschrift-Stil definieren
        var headingStyle = document.Styles.AddStyle("Heading", "Normal");
        headingStyle.Font.Bold = true;
        headingStyle.Font.Size = 12;

        // Unterüberschrift-Stil definieren
        var subheadingStyle = document.Styles.AddStyle("Subheading", "Normal");
        subheadingStyle.Font.Bold = true;

        // Tabellen-Header-Stil definieren
        var tableHeaderStyle = document.Styles.AddStyle("TableHeader", "Normal");
        tableHeaderStyle.Font.Bold = true;
        tableHeaderStyle.ParagraphFormat.Alignment = ParagraphAlignment.Center;

        // Abschnitt erstellen
        var section = document.AddSection();

        // PageSetup für den Abschnitt setzen
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.TopMargin = Unit.FromCentimeter(2);
        section.PageSetup.LeftMargin = Unit.FromCentimeter(2);
        section.PageSetup.RightMargin = Unit.FromCentimeter(2);
        section.PageSetup.BottomMargin = Unit.FromCentimeter(2);

        // Header mit Logo und Firmeninfos
        CreateHeader(section, invoice);

        // Rechnungsadresse und ggf. Lieferadresse
        CreateAddresses(section, invoice);

        // Artikeltabelle
        CreateItemsTable(section, invoice);

        // Zusammenfassung
        CreateSummary(section, invoice);

        // Zahlungsinfos
        CreatePaymentInfo(section, invoice);

        // Anmerkungen
        if (!string.IsNullOrEmpty(invoice.Notes))
        {
            CreateNotes(section, invoice);
        }

        // Footer
        CreateFooter(section);

        return document;
    }

    private void CreateHeader(Section section, Invoice invoice)
    {
        // Sicherstellen, dass section nicht null ist
        if (section == null)
            throw new ArgumentNullException(nameof(section));

        // Tabelle erstellen und konfigurieren
        var table = section.AddTable();
        if (table == null)
            throw new InvalidOperationException("Tabelle konnte nicht erstellt werden");

        table.Borders = table.Borders ?? new Borders();
        table.Borders.Visible = false;

        // Spalten hinzufügen
        var column1 = table.AddColumn(Unit.FromCentimeter(10));
        var column2 = table.AddColumn(Unit.FromCentimeter(7));

        // Zeile erstellen
        var row = table.AddRow();
        if (row == null || row.Cells.Count < 2)
            throw new InvalidOperationException("Tabellenzeile konnte nicht korrekt erstellt werden");

        // Linke Spalte: Logo und Firmeninfos
        var cell = row.Cells[0];
        var paragraph = cell.AddParagraph();

        if (!string.IsNullOrEmpty(_logoPath) && File.Exists(_logoPath))
        {
            var logo = paragraph.AddImage(_logoPath);
            logo.Height = Unit.FromCentimeter(2);
        }
        else
        {
            paragraph.AddFormattedText(_companyName, TextFormat.Bold);
        }

        paragraph = cell.AddParagraph(_companyAddress);
        paragraph = cell.AddParagraph(_companyZipCity);
        paragraph = cell.AddParagraph(_companyCountry);
        paragraph = cell.AddParagraph($"Tel: {_companyPhone}");
        paragraph = cell.AddParagraph($"E-Mail: {_companyEmail}");
        paragraph = cell.AddParagraph($"Web: {_companyWebsite}");

        // Rechte Spalte: Rechnungsinformationen
        cell = row.Cells[1];
        paragraph = cell.AddParagraph("RECHNUNG");
        paragraph.Format.Alignment = ParagraphAlignment.Right;
        paragraph.Format.Font.Bold = true;
        paragraph.Format.Font.Size = 16;

        paragraph = cell.AddParagraph($"Rechnungsnummer: {invoice.InvoiceNumber}");
        paragraph.Format.Alignment = ParagraphAlignment.Right;

        paragraph = cell.AddParagraph($"Rechnungsdatum: {invoice.InvoiceDate:dd.MM.yyyy}");
        paragraph.Format.Alignment = ParagraphAlignment.Right;

        if (invoice.OrderId.HasValue)
        {
            paragraph = cell.AddParagraph($"Bestellnummer: {invoice.Order?.Id.ToString() ?? "N/A"}");
            paragraph.Format.Alignment = ParagraphAlignment.Right;
        }

        paragraph = cell.AddParagraph($"Kundennummer: {invoice.Order?.CustomerId.ToString() ?? "N/A"}");
        paragraph.Format.Alignment = ParagraphAlignment.Right;

        // Abstand nach Header
        section.AddParagraph().Format.SpaceAfter = Unit.FromCentimeter(0.5);
    }

    private void CreateAddresses(Section section, Invoice invoice)
    {
        if (section == null)
            throw new ArgumentNullException(nameof(section));

        var table = section.AddTable();
        if (table == null)
            throw new InvalidOperationException("Tabelle konnte nicht erstellt werden");

        table.Borders = table.Borders ?? new Borders();
        table.Borders.Visible = false;

        table.AddColumn(Unit.FromCentimeter(8.5));
        table.AddColumn(Unit.FromCentimeter(8.5));

        var row = table.AddRow();
        if (row == null || row.Cells.Count < 2)
            throw new InvalidOperationException("Tabellenzeile konnte nicht korrekt erstellt werden");

        // Rechnungsadresse
        var cell = row.Cells[0];
        var paragraph = cell.AddParagraph("Rechnungsadresse:");
        paragraph.Format.Font.Bold = true;

        var recipientName = !string.IsNullOrEmpty(invoice.InvoiceAddressCompanyName)
            ? invoice.InvoiceAddressCompanyName
            : $"{invoice.InvoiceAddressFirstName} {invoice.InvoiceAddressLastName}";

        cell.AddParagraph(recipientName);
        cell.AddParagraph(invoice.InvoiceAddressStreet);
        cell.AddParagraph($"{invoice.InvoiceAddressZip} {invoice.InvoiceAddressCity}");
        cell.AddParagraph(invoice.InvoiceAddressCountry);

        if (!string.IsNullOrEmpty(invoice.InvoiceAddressPhone))
        {
            cell.AddParagraph($"Tel: {invoice.InvoiceAddressPhone}");
        }

        // Lieferadresse (wenn unterschiedlich)
        if (!string.IsNullOrEmpty(invoice.DeliveryAddressStreet) &&
            (invoice.DeliveryAddressStreet != invoice.InvoiceAddressStreet ||
             invoice.DeliveryAddressZip != invoice.InvoiceAddressZip ||
             invoice.DeliveryAddressCity != invoice.InvoiceAddressCity))
        {
            cell = row.Cells[1];
            paragraph = cell.AddParagraph("Lieferadresse:");
            paragraph.Format.Font.Bold = true;

            recipientName = !string.IsNullOrEmpty(invoice.DeliveryAddressCompanyName)
                ? invoice.DeliveryAddressCompanyName
                : $"{invoice.DeliveryAddressFirstName} {invoice.DeliveryAddressLastName}";

            cell.AddParagraph(recipientName);
            cell.AddParagraph(invoice.DeliveryAddressStreet);
            cell.AddParagraph($"{invoice.DeliveryAddressZip} {invoice.DeliveryAddressCity}");
            cell.AddParagraph(invoice.DeliveryAddressCountry);

            if (!string.IsNullOrEmpty(invoice.DeliveryAddressPhone))
            {
                cell.AddParagraph($"Tel: {invoice.DeliveryAddressPhone}");
            }
        }

        // Abstand nach Adressen
        section.AddParagraph().Format.SpaceAfter = Unit.FromCentimeter(0.5);
    }

    private void CreateItemsTable(Section section, Invoice invoice)
    {
        if (section == null)
            throw new ArgumentNullException(nameof(section));

        var table = section.AddTable();
        if (table == null)
            throw new InvalidOperationException("Tabelle konnte nicht erstellt werden");

        table.Borders = table.Borders ?? new Borders();
        table.Borders.Width = 0.5;

        // Spalten definieren
        table.AddColumn(Unit.FromCentimeter(7)); // Beschreibung
        table.AddColumn(Unit.FromCentimeter(2)); // Menge
        table.AddColumn(Unit.FromCentimeter(2)); // Einheit
        table.AddColumn(Unit.FromCentimeter(2)); // Preis
        table.AddColumn(Unit.FromCentimeter(2)); // MwSt
        table.AddColumn(Unit.FromCentimeter(2)); // Gesamt

        // Header-Zeile
        var headerRow = table.AddRow();
        if (headerRow == null || headerRow.Cells.Count < 6)
            throw new InvalidOperationException("Header-Zeile konnte nicht korrekt erstellt werden");
        headerRow.HeadingFormat = true;
        headerRow.Format.Font.Bold = true;
        headerRow.Shading.Color = new Color(230, 230, 230);

        headerRow.Cells[0].AddParagraph("Artikel");
        headerRow.Cells[1].AddParagraph("Menge");
        headerRow.Cells[2].AddParagraph("Einheit");
        headerRow.Cells[3].AddParagraph("Einzelpreis");
        headerRow.Cells[4].AddParagraph("MwSt.");
        headerRow.Cells[5].AddParagraph("Gesamt");

        // Artikel-Zeilen
        foreach (var item in invoice.InvoiceItems)
        {
            var row = table.AddRow();

            // Artikel-Beschreibung
            var cell = row.Cells[0];
            var paragraph = cell.AddParagraph(item.Name);
            paragraph.Format.Font.Bold = true;

            if (!string.IsNullOrEmpty(item.Description))
            {
                cell.AddParagraph(item.Description);
            }

            if (!string.IsNullOrEmpty(item.SKU))
            {
                cell.AddParagraph($"Art.-Nr.: {item.SKU}");
            }

            // Menge
            row.Cells[1].AddParagraph(item.Quantity.ToString("0.##", CultureInfo.InvariantCulture));

            // Einheit
            row.Cells[2].AddParagraph(item.Unit);

            // Einzelpreis
            row.Cells[3].AddParagraph($"{item.UnitPrice:0.00} €");

            // MwSt.
            row.Cells[4].AddParagraph($"{item.TaxRate:0.#}%");

            // Gesamtpreis
            row.Cells[5].AddParagraph($"{item.TotalPrice:0.00} €");
        }

        // Abstand nach Tabelle
        section.AddParagraph().Format.SpaceAfter = Unit.FromCentimeter(0.5);
    }

    private void CreateSummary(Section section, Invoice invoice)
    {
        if (section == null)
            throw new ArgumentNullException(nameof(section));

        var table = section.AddTable();
        if (table == null)
            throw new InvalidOperationException("Tabelle konnte nicht erstellt werden");

        table.Borders = table.Borders ?? new Borders();
        table.Borders.Visible = false;

        table.AddColumn(Unit.FromCentimeter(12));
        table.AddColumn(Unit.FromCentimeter(5));

        var row = table.AddRow();
        if (row == null || row.Cells.Count < 2)
            throw new InvalidOperationException("Tabellenzeile konnte nicht korrekt erstellt werden");

        // Leere linke Zelle
        row.Cells[0].AddParagraph();

        // Rechte Zelle: Zusammenfassung
        var cell = row.Cells[1];
        var paragraph = cell.AddParagraph($"Zwischensumme (netto): {invoice.Subtotal:0.00} €");

        // MwSt. nach Sätzen gruppieren
        var taxGroups = invoice.InvoiceItems
            .GroupBy(item => item.TaxRate)
            .Select(group => new
            {
                TaxRate = group.Key,
                TaxAmount = group.Sum(item => item.TaxAmount)
            });

        foreach (var taxGroup in taxGroups.OrderBy(g => g.TaxRate))
        {
            cell.AddParagraph($"MwSt. ({taxGroup.TaxRate:0.#}%): {taxGroup.TaxAmount:0.00} €");
        }

        // Versandkosten
        if (invoice.ShippingCost > 0)
        {
            cell.AddParagraph($"Versandkosten: {invoice.ShippingCost:0.00} €");
        }

        // Gesamtbetrag
        paragraph = cell.AddParagraph($"Gesamtbetrag: {invoice.Total:0.00} €");
        paragraph.Format.Font.Bold = true;
        paragraph.Format.Font.Size = 12;
        paragraph.Format.Borders.Top.Width = 1;
        paragraph.Format.Borders.Top.Color = Colors.Black;
        paragraph.Format.SpaceBefore = 5;

        // Abstand nach Zusammenfassung
        section.AddParagraph().Format.SpaceAfter = Unit.FromCentimeter(0.5);
    }

    private void CreatePaymentInfo(Section section, Invoice invoice)
    {
        var paragraph = section.AddParagraph("Zahlungsinformationen:");
        paragraph.Format.Font.Bold = true;
        paragraph.Format.SpaceBefore = Unit.FromCentimeter(0.5);

        section.AddParagraph($"Zahlungsmethode: {invoice.PaymentMethod}");

        if (!string.IsNullOrEmpty(invoice.PaymentTransactionId))
        {
            section.AddParagraph($"Transaktions-ID: {invoice.PaymentTransactionId}");
        }

        section.AddParagraph($"Zahlungsstatus: {invoice.PaymentStatus}");

        // Bankverbindung
        paragraph = section.AddParagraph("Bankverbindung:");
        paragraph.Format.Font.Bold = true;
        paragraph.Format.SpaceBefore = Unit.FromCentimeter(0.3);

        section.AddParagraph($"Bank: {_companyBankName}");
        section.AddParagraph($"IBAN: {_companyIban}");
        section.AddParagraph($"BIC: {_companyBic}");
    }

    private void CreateNotes(Section section, Invoice invoice)
    {
        var paragraph = section.AddParagraph("Anmerkungen:");
        paragraph.Format.Font.Bold = true;
        paragraph.Format.SpaceBefore = Unit.FromCentimeter(0.5);

        section.AddParagraph(invoice.Notes);
    }

    private void CreateFooter(Section section)
    {
        if (section == null)
            throw new ArgumentNullException(nameof(section));

        var paragraph = section.AddParagraph();
        paragraph.Format.SpaceBefore = Unit.FromCentimeter(1);
        paragraph.Format.Borders = paragraph.Format.Borders ?? new Borders();
        paragraph.Format.Borders.Top = paragraph.Format.Borders.Top ?? new Border();
        paragraph.Format.Borders.Top.Width = 1;
        paragraph.Format.Borders.Top.Color = new Color(180, 180, 180);

        var table = section.AddTable();
        if (table == null)
            throw new InvalidOperationException("Tabelle konnte nicht erstellt werden");

        table.Borders = table.Borders ?? new Borders();
        table.Borders.Visible = false;

        table.AddColumn(Unit.FromCentimeter(5.5));
        table.AddColumn(Unit.FromCentimeter(5.5));
        table.AddColumn(Unit.FromCentimeter(6));

        var row = table.AddRow();
        if (row == null || row.Cells.Count < 3)
            throw new InvalidOperationException("Tabellenzeile konnte nicht korrekt erstellt werden");

        // Firmendaten
        var cell = row.Cells[0];
        cell.AddParagraph(_companyName);
        cell.AddParagraph($"USt-IdNr.: {_companyVatId}");
        cell.AddParagraph($"Steuernummer: {_companyTaxId}");

        // Bankdaten
        cell = row.Cells[1];
        cell.AddParagraph(_companyBankName);
        cell.AddParagraph($"IBAN: {_companyIban}");
        cell.AddParagraph($"BIC: {_companyBic}");

        // Kontaktdaten
        cell = row.Cells[2];
        cell.AddParagraph(_companyPhone);
        cell.AddParagraph(_companyEmail);
        cell.AddParagraph(_companyWebsite);

        // Seitenzahl
        paragraph = section.AddParagraph("Seite ");
        paragraph.Format.Alignment = ParagraphAlignment.Center;
        paragraph.Format.SpaceBefore = 5;
        paragraph.AddPageField();
        paragraph.AddText(" von ");
        paragraph.AddNumPagesField();
    }
}