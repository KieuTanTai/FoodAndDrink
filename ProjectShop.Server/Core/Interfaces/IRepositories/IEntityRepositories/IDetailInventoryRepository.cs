using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IDetailInventoryRepository : IRepository<DetailInventory>
    {
        Task<IEnumerable<DetailInventory>> GetByInventoryIdAsync(uint inventoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<DetailInventory>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default);
        Task<DetailInventory?> GetByInventoryIdAndProductBarcodeAsync(uint inventoryId, string productBarcode, CancellationToken cancellationToken = default);
        Task<IEnumerable<DetailInventory>> GetByAddedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<DetailInventory>> GetLowStockItemsAsync(int threshold, CancellationToken cancellationToken = default);
    }
}
