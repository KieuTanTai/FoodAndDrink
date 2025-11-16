using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IProductLotRepository : IRepository<ProductLot>
    {
        Task<IEnumerable<ProductLot>> GetByInventoryIdAsync(uint inventoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductLot>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<ProductLot?> GetByIdWithDetailsAsync(uint productLotId, CancellationToken cancellationToken = default);
    }
}
