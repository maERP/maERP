using MediatR;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Infrastructure;
using maERP.Domain.Wrapper;
using Microsoft.Extensions.Logging;

namespace maERP.Application.Features.Invoice.Queries.InvoicePdf;

public class InvoicePdfQueryHandler : IRequestHandler<InvoicePdfQuery, Result<byte[]>>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ISettingRepository _settingRepository;
    private readonly IPdfService _pdfService;
    private readonly ILogger<InvoicePdfQueryHandler> _logger;

    public InvoicePdfQueryHandler(
        IInvoiceRepository invoiceRepository,
        ISettingRepository settingRepository,
        IPdfService pdfService,
        ILogger<InvoicePdfQueryHandler> logger)
    {
        _invoiceRepository = invoiceRepository;
        _settingRepository = settingRepository;
        _pdfService = pdfService;
        _logger = logger;
    }

    public async Task<Result<byte[]>> Handle(InvoicePdfQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Rechnung mit allen Details abrufen
            var invoice = await _invoiceRepository.GetInvoiceWithDetailsAsync(request.Id);
            if (invoice == null)
            {
                return await Result<byte[]>.FailAsync("Rechnung nicht gefunden");
            }

            // Unternehmenseinstellungen abrufen
            var settings = (await _settingRepository.GetAllAsync())
                .ToDictionary(s => s.Key, s => s.Value);

            // PDF generieren
            var pdfBytes = _pdfService.GenerateInvoice(invoice);
            if (pdfBytes == null)
            {
                return await Result<byte[]>.FailAsync("PDF konnte nicht generiert werden");
            }

            return await Result<byte[]>.SuccessAsync(pdfBytes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fehler beim Generieren der Rechnungs-PDF für ID {InvoiceId}", request.Id);
            return await Result<byte[]>.FailAsync("Fehler beim Generieren der PDF: " + ex.Message);
        }
    }

    private static string GetSetting(Dictionary<string, string> settings, string key, string defaultValue)
    {
        return settings.TryGetValue(key, out var value) ? value : defaultValue;
    }
} 