using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using maERP.Domain.Entities;

namespace maERP.Infrastructure.PDF;

public class PdfInvoice : IPdfInvoice
{
    private readonly string _companyName;
    private readonly string _companyAddress;
    private readonly string _companyZipCity;
    private readonly string _companyCountry;
    private readonly string _companyPhone;
    private readonly string _companyEmail;
    private readonly string _companyWebsite;
    private readonly string _companyTaxId;
    private readonly string _companyVatId;
    private readonly string _companyBankName;
    private readonly string _companyIban;
    private readonly string _companyBic;
    private readonly string _logoPath;

    public PdfInvoice(
        string companyName, 
        string companyAddress, 
        string companyZipCity, 
        string companyCountry, 
        string companyPhone, 
        string companyEmail, 
        string companyWebsite, 
        string companyTaxId, 
        string companyVatId, 
        string companyBankName, 
        string companyIban, 
        string companyBic,
        string logoPath = null)
    {
        _companyName = companyName;
        _companyAddress = companyAddress;
        _companyZipCity = companyZipCity;
        _companyCountry = companyCountry;
        _companyPhone = companyPhone;
        _companyEmail = companyEmail;
        _companyWebsite = companyWebsite;
        _companyTaxId = companyTaxId;
        _companyVatId = companyVatId;
        _companyBankName = companyBankName;
        _companyIban = companyIban;
        _companyBic = companyBic;
        _logoPath = logoPath;
    }

    /// <summary>
    /// Generates a PDF invoice from the given Invoice entity
    /// </summary>
    /// <param name="invoice">The invoice entity to generate PDF for</param>
    /// <param name="outputPath">Optional path to save the PDF file. If null, returns the PDF as a byte array</param>
    /// <returns>Byte array containing the PDF if outputPath is null, otherwise returns null after saving to file</returns>
    public byte[] GenerateInvoice(Invoice invoice, string outputPath = null)
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

    /// <summary>
    /// Generates a PDF invoice asynchronously from the given Invoice entity
    /// </summary>
    /// <param name="invoice">The invoice entity to generate PDF for</param>
    /// <param name="outputPath">Optional path to save the PDF file. If null, returns the PDF as a byte array</param>
    /// <returns>Byte array containing the PDF if outputPath is null, otherwise returns null after saving to file</returns>
    public async Task<byte[]> GenerateInvoiceAsync(Invoice invoice, string outputPath = null)
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
            return await document.GeneratePdfAsync();
        }
        
        await document.GeneratePdfAsync(outputPath);
        return null;
    }

    private static void ComposeHeader(IContainer container, Invoice invoice)
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
                    column.Item().AlignRight().Text($"Bestellnummer: {invoice.Order?.OrderNumber ?? "N/A"}");
                }

                column.Item().AlignRight().Text($"Kundennummer: {invoice.Customer?.CustomerNumber ?? "N/A"}");
            });
        });
    }

    private static void ComposeContent(IContainer container, Invoice invoice)
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

    private static void ComposeFooter(IContainer container, Invoice invoice)
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