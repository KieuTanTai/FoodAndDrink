using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Inventory repository interface with specific query methods
    /// </summary>
    public interface IInventoryRepository : IRepository<Inventory>
    {
        // Query by InventoryName
        Task<Inventory?> GetByNameAsync(string inventoryName, CancellationToken cancellationToken = default);
        Task<IEnumerable<Inventory>> GetByNamesAsync(IEnumerable<string> inventoryNames, CancellationToken cancellationToken = default);
        Task<IEnumerable<Inventory>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);

        // Query by LastUpdatedDate
        Task<IEnumerable<Inventory>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<Inventory>> GetRecentlyUpdatedAsync(int days, CancellationToken cancellationToken = default);

        // Query with navigation properties
        Task<Inventory?> GetByIdWithNavigationAsync(uint inventoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Inventory>> GetAllWithNavigationAsync(CancellationToken cancellationToken = default);

        // Business queries
        Task<IEnumerable<Inventory>> GetLowStockInventoriesAsync(CancellationToken cancellationToken = default);
    }
}
