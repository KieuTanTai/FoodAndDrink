using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IDetailInvoiceRepository : IRepository<DetailInvoice>
    {
        Task<IEnumerable<DetailInvoice>> GetByInvoiceIdAsync(uint invoiceId, CancellationToken cancellationToken = default);
        Task<IEnumerable<DetailInvoice>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default);
        Task<DetailInvoice?> GetByInvoiceIdAndProductBarcodeAsync(uint invoiceId, string productBarcode, CancellationToken cancellationToken = default);
        Task<IEnumerable<DetailInvoice>> GetBySaleEventIdAsync(uint? saleEventId, CancellationToken cancellationToken = default);
    }
}
