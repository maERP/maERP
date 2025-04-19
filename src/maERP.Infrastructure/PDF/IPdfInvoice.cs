using System.Threading.Tasks;
using maERP.Domain.Entities;

namespace maERP.Infrastructure.PDF;

public interface IPdfInvoice
{
    byte[] GenerateInvoice(Invoice invoice, string outputPath = null);
    Task<byte[]> GenerateInvoiceAsync(Invoice invoice, string outputPath = null);
}
