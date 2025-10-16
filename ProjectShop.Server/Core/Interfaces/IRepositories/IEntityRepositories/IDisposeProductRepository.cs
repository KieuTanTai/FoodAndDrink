using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IDisposeProductRepository : IRepository<DisposeProduct>
    {
        Task<IEnumerable<DisposeProduct>> GetByDisposeReasonIdAsync(uint disposeReasonId, CancellationToken cancellationToken = default);
        Task<IEnumerable<DisposeProduct>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default);
        Task<IEnumerable<DisposeProduct>> GetByDisposeDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<DisposeProduct>> GetByQuantityRangeAsync(int minQuantity, int maxQuantity, CancellationToken cancellationToken = default);
    }
}
