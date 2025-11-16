using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IDetailSaleEventRepository : IRepository<DetailSaleEvent>
    {
        Task<IEnumerable<DetailSaleEvent>> GetBySaleEventIdAsync(uint saleEventId, CancellationToken cancellationToken = default);
        Task<IEnumerable<DetailSaleEvent>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default);
        Task<DetailSaleEvent?> GetBySaleEventIdAndProductBarcodeAsync(uint saleEventId, string productBarcode, CancellationToken cancellationToken = default);
    }
}
