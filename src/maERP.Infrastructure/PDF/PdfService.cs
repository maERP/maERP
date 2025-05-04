using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Contracts.Persistence;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using maERP.Domain.Entities;

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

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(50);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header().Element(container => ComposeHeader(container, invoice));
                page.Content().Element(container => ComposeContent(container, invoice));
                page.Footer().Element(container => ComposeFooter(container, invoice));
            });
        });

        if (string.IsNullOrEmpty(outputPath))
        {
            return document.GeneratePdf();
        }
        
        document.GeneratePdf(outputPath);
        return null;
    }

    private void ComposeHeader(IContainer container, Invoice invoice)
    {
        container.Row(row =>
        {
            // Logo and company info
            row.RelativeItem().Column(column =>
            {
                if (!string.IsNullOrEmpty(_logoPath) && File.Exists(_logoPath))
                {
                    column.Item().Height(50).Image(_logoPath);
                }
                else
                {
                    column.Item().Text(_companyName).FontSize(16).Bold();
                }

                column.Item().Text(_companyAddress);
                column.Item().Text(_companyZipCity);
                column.Item().Text(_companyCountry);
                column.Item().Text($"Tel: {_companyPhone}");
                column.Item().Text($"E-Mail: {_companyEmail}");
                column.Item().Text($"Web: {_companyWebsite}");
            });

            // Invoice information
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignRight().Text("RECHNUNG").FontSize(16).Bold();
                column.Item().AlignRight().Text($"Rechnungsnummer: {invoice.InvoiceNumber}");
                column.Item().AlignRight().Text($"Rechnungsdatum: {invoice.InvoiceDate:dd.MM.yyyy}");

                if (invoice.OrderId.HasValue)
                {
                    column.Item().AlignRight().Text($"Bestellnummer: {invoice.Order?.Id.ToString() ?? "N/A"}");
                }

                column.Item().AlignRight().Text($"Kundennummer: {invoice.Order?.CustomerId.ToString() ?? "N/A"}");
            });
        });
    }

    private void ComposeContent(IContainer container, Invoice invoice)
    {
        container.PaddingVertical(20).Column(column =>
        {
            // Billing address
            column.Item().PaddingBottom(10).Column(addressColumn =>
            {
                addressColumn.Item().Text("Rechnungsadresse:").Bold();
                
                var recipientName = !string.IsNullOrEmpty(invoice.InvoiceAddressCompanyName) 
                    ? invoice.InvoiceAddressCompanyName 
                    : $"{invoice.InvoiceAddressFirstName} {invoice.InvoiceAddressLastName}";

                addressColumn.Item().Text(recipientName);
                addressColumn.Item().Text(invoice.InvoiceAddressStreet);
                addressColumn.Item().Text($"{invoice.InvoiceAddressZip} {invoice.InvoiceAddressCity}");
                addressColumn.Item().Text(invoice.InvoiceAddressCountry);
                
                if (!string.IsNullOrEmpty(invoice.InvoiceAddressPhone))
                {
                    addressColumn.Item().Text($"Tel: {invoice.InvoiceAddressPhone}");
                }
            });

            // If delivery address is different, show it
            if (!string.IsNullOrEmpty(invoice.DeliveryAddressStreet) && 
                (invoice.DeliveryAddressStreet != invoice.InvoiceAddressStreet ||
                 invoice.DeliveryAddressZip != invoice.InvoiceAddressZip ||
                 invoice.DeliveryAddressCity != invoice.InvoiceAddressCity))
            {
                column.Item().PaddingBottom(10).Column(addressColumn =>
                {
                    addressColumn.Item().Text("Lieferadresse:").Bold();
                    
                    var recipientName = !string.IsNullOrEmpty(invoice.DeliveryAddressCompanyName) 
                        ? invoice.DeliveryAddressCompanyName 
                        : $"{invoice.DeliveryAddressFirstName} {invoice.DeliveryAddressLastName}";

                    addressColumn.Item().Text(recipientName);
                    addressColumn.Item().Text(invoice.DeliveryAddressStreet);
                    addressColumn.Item().Text($"{invoice.DeliveryAddressZip} {invoice.DeliveryAddressCity}");
                    addressColumn.Item().Text(invoice.DeliveryAddressCountry);
                    
                    if (!string.IsNullOrEmpty(invoice.DeliveryAddressPhone))
                    {
                        addressColumn.Item().Text($"Tel: {invoice.DeliveryAddressPhone}");
                    }
                });
            }

            // Items table
            column.Item().Table(table =>
            {
                // Define columns
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3); // Description
                    columns.RelativeColumn(1); // Quantity
                    columns.RelativeColumn(1); // Unit
                    columns.RelativeColumn(1); // Price
                    columns.RelativeColumn(1); // Tax Rate
                    columns.RelativeColumn(1); // Total
                });

                // Add header row
                table.Header(header =>
                {
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Artikel").Bold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Menge").Bold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Einheit").Bold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Einzelpreis").Bold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("MwSt.").Bold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Gesamt").Bold();
                });

                // Add items
                foreach (var item in invoice.InvoiceItems)
                {
                    table.Cell().Padding(5).Column(column =>
                    {
                        column.Item().Text(item.Name).Bold();
                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            column.Item().Text(item.Description);
                        }
                        if (!string.IsNullOrEmpty(item.SKU))
                        {
                            column.Item().Text($"Art.-Nr.: {item.SKU}");
                        }
                    });
                    
                    table.Cell().Padding(5).Text(item.Quantity.ToString("0.##"));
                    table.Cell().Padding(5).Text(item.Unit);
                    table.Cell().Padding(5).Text($"{item.UnitPrice:0.00} €");
                    table.Cell().Padding(5).Text($"{item.TaxRate:0.#}%");
                    table.Cell().Padding(5).Text($"{item.TotalPrice:0.00} €");
                }
            });

            // Summary
            column.Item().PaddingTop(10).AlignRight().Column(summary =>
            {
                summary.Item().Text($"Zwischensumme (netto): {invoice.Subtotal:0.00} €");
                
                // Group items by tax rate and display tax amounts separately
                var taxGroups = invoice.InvoiceItems
                    .GroupBy(item => item.TaxRate)
                    .Select(group => new
                    {
                        TaxRate = group.Key,
                        TaxAmount = group.Sum(item => item.TaxAmount)
                    });

                foreach (var taxGroup in taxGroups.OrderBy(g => g.TaxRate))
                {
                    summary.Item().Text($"MwSt. ({taxGroup.TaxRate:0.#}%): {taxGroup.TaxAmount:0.00} €");
                }

                if (invoice.ShippingCost > 0)
                {
                    summary.Item().Text($"Versandkosten: {invoice.ShippingCost:0.00} €");
                }
                
                summary.Item().BorderTop(1).BorderColor(Colors.Black).PaddingTop(5)
                    .Text($"Gesamtbetrag: {invoice.Total:0.00} €").FontSize(12).Bold();
            });

            // Payment information
            column.Item().PaddingTop(20).Column(payment =>
            {
                payment.Item().Text("Zahlungsinformationen:").Bold();
                payment.Item().Text($"Zahlungsmethode: {invoice.PaymentMethod}");
                
                if (!string.IsNullOrEmpty(invoice.PaymentTransactionId))
                {
                    payment.Item().Text($"Transaktions-ID: {invoice.PaymentTransactionId}");
                }
                
                payment.Item().Text($"Zahlungsstatus: {invoice.PaymentStatus}");
                
                // Bank details
                payment.Item().PaddingTop(5).Column(bank =>
                {
                    bank.Item().Text("Bankverbindung:").Bold();
                    bank.Item().Text($"Bank: {_companyBankName}");
                    bank.Item().Text($"IBAN: {_companyIban}");
                    bank.Item().Text($"BIC: {_companyBic}");
                });
            });

            // Notes
            if (!string.IsNullOrEmpty(invoice.Notes))
            {
                column.Item().PaddingTop(10).Column(notes =>
                {
                    notes.Item().Text("Anmerkungen:").Bold();
                    notes.Item().Text(invoice.Notes);
                });
            }
        });
    }

    private void ComposeFooter(IContainer container, Invoice invoice)
    {
        container.Column(column =>
        {
            // Company legal information
            column.Item().BorderTop(1).BorderColor(Colors.Grey.Medium).PaddingTop(5)
                .Row(row =>
                {
                    row.RelativeItem().Column(c =>
                    {
                        c.Item().Text(_companyName);
                        c.Item().Text($"USt-IdNr.: {_companyVatId}");
                        c.Item().Text($"Steuernummer: {_companyTaxId}");
                    });

                    row.RelativeItem().Column(c =>
                    {
                        c.Item().Text(_companyBankName);
                        c.Item().Text($"IBAN: {_companyIban}");
                        c.Item().Text($"BIC: {_companyBic}");
                    });

                    row.RelativeItem().Column(c =>
                    {
                        c.Item().Text(_companyPhone);
                        c.Item().Text(_companyEmail);
                        c.Item().Text(_companyWebsite);
                    });
                });

            // Page number
            column.Item().AlignCenter().Text(text =>
            {
                text.Span("Seite ");
                text.CurrentPageNumber();
                text.Span(" von ");
                text.TotalPages();
            });
        });
    }
}