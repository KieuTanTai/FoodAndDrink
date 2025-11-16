using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IDetailCartRepository : IRepository<DetailCart>
    {
        Task<IEnumerable<DetailCart>> GetByCartIdAsync(uint cartId, CancellationToken cancellationToken = default);
        Task<IEnumerable<DetailCart>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default);
        Task<DetailCart?> GetByCartIdAndProductBarcodeAsync(uint cartId, string productBarcode, CancellationToken cancellationToken = default);
        Task<IEnumerable<DetailCart>> GetByAddedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
